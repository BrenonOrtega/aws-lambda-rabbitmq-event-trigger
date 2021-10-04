using System.Collections.Generic;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Models
{
    public class Event
    {
        public string EventSource { get; set; }
        public string EventSourceArn { get; set; }
        List<EventData> Messages { get; set; } = new List<EventData>();
    }

    public class EventData
    {
        public string MessageID { get; set; }
        public string MessageType { get; set; }
        public ulong Timestamp {get; set;}
        public int DeliveryMode { get; set; }
        public string CorrelationID { get; set; }
        public string ReplyTo { get; set; }
        public object Destination { get; set; }
        public bool Redelivered { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public string Data { get; set; }
        public ulong BrokerInTime { get; set; }
        public ulong BrokerOutTime { get; set; }
    }
}