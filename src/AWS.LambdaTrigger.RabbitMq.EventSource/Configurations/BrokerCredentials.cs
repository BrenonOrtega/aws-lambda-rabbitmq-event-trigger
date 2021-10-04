using System;
using Microsoft.Extensions.Configuration;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Configurations
{
    public class BrokerCredentials
    {
        private const string SectionName = "RabbitMqCredentials";

        public const string DefaultVhostEncoding = "%2f";

        public string Username { get; set; }
        public string Password { get; set; }

        public string ApiUrl {get;set;}

        public BrokerCredentials() { }
        public BrokerCredentials(IConfiguration config)
        {
            _ = config ?? throw new ArgumentNullException(nameof(config));
            Username = config.GetValue<string>(SectionName +":"+ nameof(Username));
            Password =  config.GetValue<string>(SectionName +":"+ nameof(Password));
            ApiUrl =config.GetValue<string>(SectionName +":"+ nameof(ApiUrl));
        }

    }
}