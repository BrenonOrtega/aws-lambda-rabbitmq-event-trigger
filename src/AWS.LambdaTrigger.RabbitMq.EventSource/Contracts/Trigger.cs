using System;

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public class Trigger
    {
        public Guid CorrelationID { get; set; }
        public string Description => "Hey I'm Triggering an event";
    }
}