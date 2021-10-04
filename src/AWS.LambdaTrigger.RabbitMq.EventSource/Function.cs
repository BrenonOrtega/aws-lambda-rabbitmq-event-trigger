using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Amazon.Lambda.Core;
using System.Threading;
using System.IO;
using System.Text.Json;
using AWS.LambdaTrigger.RabbitMq.EventSource.Policies;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(Stream data, ILambdaContext context)
        {
            var bytes = new byte[data.Length + 15];
            var bytesToRead = (int)data.Length;
            var readBytes = 0;

            do
            {
                var n = await data.ReadAsync(bytes, readBytes, 15);
                readBytes += n;
                bytesToRead -= n;
            } while (bytesToRead > 0);

            context.Logger.Log($"Starting application at { DateTime.Now }.");
            context.Logger.Log(JsonSerializer.Serialize(bytes, new JsonSerializerOptions { WriteIndented = true }));
            
            var host = new HostBuilder()
                .ConfigureAppConfiguration(b => b.AddLambdaConfiguration())
                   .ConfigureServices((hostContext, services) => services.ConfigureLambdaServices(hostContext.Configuration, context)
                    .AddSingleton<TaskCircuitBreaker>())
                .Build()
                ;

            var csc = host.Services.GetRequiredService<CancellationTokenSource>();
            var bus = host.Services.GetRequiredService<IBusControl>();
            
            try
            {
                await host.RunAsync(csc.Token);
            }
            finally
            {
                await host.StopAsync();
            }

            context.Logger.Log("Ending Lambda Function Invocation.");
        }
    }
}
