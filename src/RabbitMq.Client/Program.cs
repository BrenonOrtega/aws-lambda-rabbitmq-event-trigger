using System;
using System.Text.Json;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace RabbitMq.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .Build();

            var rabbitConnectionString = config.GetConnectionString("RabbitMq") ?? throw new ArgumentNullException("connectionString");
            var username = config["RabbitMqCredentials:Username"] ?? throw new ArgumentNullException("username");
            var Password = config["RabbitMqCredentials:Password"] ?? throw new ArgumentNullException("password");
            
            //in the URL this means the default vhost "/" .
            var defaultVhostUrlEncoded = "%2f";

            var client = new FlurlClient(rabbitConnectionString)
                .AllowAnyHttpStatus()
                ;

            var response = await rabbitConnectionString
                .AppendPathSegment($"/queues/{ defaultVhostUrlEncoded }/received.messages")
                .WithBasicAuth("username", "Password")
                .GetAsync()
                .ReceiveJson<QueueStatus>()
                ;

            Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
