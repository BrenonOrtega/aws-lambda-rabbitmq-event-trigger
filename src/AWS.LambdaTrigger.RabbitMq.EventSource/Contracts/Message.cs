using System;

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public class Message
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Description => $"Hey I've been created at { CreatedAt } with ID: { CorrelationId }";
    }
}