using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILambdaContext _context;
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILambdaContext context, ILogger<MessageConsumer> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<Message> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message);
            var message = $"Consuming message :{ serializedMessage }";

            _logger.LogInformation(message);
            _context.Logger.Log(message);
            return Task.CompletedTask;
        }
    }
}