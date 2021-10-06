using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Amazon.Lambda.Core;
using AWS.LambdaTrigger.RabbitMq.EventSource.Helpers;
using AWS.LambdaTrigger.RabbitMq.EventSource.Policies;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public static class Extensions
    {
        public static IConfigurationBuilder AddLambdaConfiguration(this IConfigurationBuilder builder) =>
            builder.AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                ;

        public static IServiceCollection ConfigureLambdaServices(this IServiceCollection services, IConfiguration config, ILambdaContext context)
        {
            var rabbitmqUri = config.GetConnectionString("RabbitMq");
            var messageQueue = config["RabbitMq:MessageQueueName"];
            var rabbitMqSecret = config["SecretsManager:RabbitMqCredentials"];

            var credentials = AwsSecretsManager.GetSecret(rabbitMqSecret);
            var username = config["RabbitMqCredentials:Username"];
            var password = config["RabbitMqCredentials:Password"];

            var allCredentials = new HashSet<string>() { rabbitmqUri, messageQueue, rabbitMqSecret, username, password };
            allCredentials.ThrowIfNullOrEmpty("Some required broker access credentials were not provided");

            var csc = new CancellationTokenSource();

            services.AddSingleton<ILambdaContext>(context)
                .AddSingleton<CancellationTokenSource>(csc)
                .AddHostedService<TaskCircuitBreaker>()
                .AddMassTransitHostedService()
                .AddMassTransit(configure =>
                {
                    configure.AddConsumer<MessageConsumer>();
                    configure.UsingRabbitMq((ctx, configureBus) =>
                    {
                        configureBus.Host(new Uri(rabbitmqUri), rabbitHost =>
                        {
                            rabbitHost.Username(username);
                            rabbitHost.Password(password);
                        });

                        configureBus.ReceiveEndpoint(messageQueue, e =>
                        {
                            e.ConfigureConsumer<MessageConsumer>(ctx, f => f.Message<Message>(q => q.UseInMemoryOutbox()));
                        });

                        configureBus.ConfigureEndpoints(ctx);
                    });
                });

            return services;
        }

        public static void ThrowIfNullOrEmpty(this IEnumerable<string> strings, string errorMessage) => strings.Any(@string =>
        {
            var isNullOrEmpty = string.IsNullOrEmpty(@string);

            if (isNullOrEmpty)
                throw new ArgumentException(errorMessage);

            return isNullOrEmpty;
        });

    }
}