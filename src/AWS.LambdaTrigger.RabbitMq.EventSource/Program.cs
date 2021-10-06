using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AWS.LambdaTrigger.RabbitMq.EventSource.Models;
using AWS.LambdaTrigger.RabbitMq.EventSource.Policies;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public class Program
    {
        private readonly Microsoft.Extensions.Hosting.IHost Host;
        static Task Main(string[] args)
        {
            return Task.CompletedTask;
        }

        public static async Task HostStart(Stream data, ILambdaContext context)
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

            var messages = JsonSerializer.Deserialize<Event>(bytes);

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
        }
    }
}