using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MassTransit;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Publisher
{
    public class Worker : BackgroundService
    {
        private readonly int _messageCount;
        private readonly int _waitTime;
        private readonly IPublishEndpoint _endpoint;
        private readonly ILogger<Worker> _logger;
        private readonly IHostedService service;

        public Worker(ILogger<Worker> logger, IPublishEndpoint endpoint, IConfiguration config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));

            int.TryParse(config["MessageCount"], out _messageCount);
            int.TryParse(config["WaitTime"], out _waitTime);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Application starte succesfully - Starting message publish.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var taskQueue = new Queue<Task>();

                    for (var i = 0; i <= _messageCount; i++)
                        taskQueue.Enqueue(_endpoint.Publish<Message>(new()));

                    await Task.WhenAll(taskQueue);

                    await _endpoint.Publish<Trigger>(new Trigger());
                    _logger.LogInformation("Process finished publishing {messageCount} messages", _messageCount);

                    await Task.Delay(_waitTime);

                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.ToString());
                }
            }
        }
    }
}
