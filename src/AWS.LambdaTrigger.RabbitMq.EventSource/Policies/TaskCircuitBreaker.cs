using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AWS.LambdaTrigger.RabbitMq.EventSource.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Flurl;
using Flurl.Http;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Policies
{
    public class TaskCircuitBreaker : BackgroundService
    {
        private readonly BrokerCredentials _credentials;
        private readonly ILambdaContext _context;
        private readonly CancellationTokenSource _tokenSource;

        private readonly string _monitoredQueueName;

        public TaskCircuitBreaker(ILambdaContext context, CancellationTokenSource tokenSource, IConfiguration config)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenSource = tokenSource ?? throw new ArgumentNullException(nameof(tokenSource));
            _credentials = new BrokerCredentials(config);
            _monitoredQueueName = config["RabbitMq:MessageQueueName"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested is false)
            {

                var queueStatus = await _credentials.ApiUrl
                    .AppendPathSegment($"queues/{ BrokerCredentials.DefaultVhostEncoding }/{ _monitoredQueueName }")
                    .WithBasicAuth(_credentials.Username, _credentials.Password)
                    .GetJsonAsync<QueueStatus>();

                if (0.Equals(queueStatus?.Messages) && 0.Equals(queueStatus?.MessagesUnacknowledged))
                {
                    _context.Logger.Log("All Messages were consumed, shutting down the Host");
                    _tokenSource.Cancel();
                }

                await Task.Delay(850);
            }
        }

    }
}