using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Publisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureAppConfiguration(b => {
                    b.AddEnvironmentVariables();
                    b.AddJsonFile("appsettings.json", optional:false, reloadOnChange: true);
                    b.AddJsonFile("appsettings.Development.json", optional:false, reloadOnChange: true);
                    b.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;
                    services.AddMassTransit(configure => 
                    {
                        configure.UsingRabbitMq((ctx, cfg) => 
                        {
                            var connectionString = config.GetConnectionString("RabbitMq");
                            var username = config["RabbitMq:Username"];
                            var password = config["RabbitMq:Password"];

                            cfg.Host(connectionString, hostCfg => 
                            {
                                hostCfg.Username(username);
                                hostCfg.Password(password);
                            });
                            cfg.ConfigureEndpoints(ctx);
                        });
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
