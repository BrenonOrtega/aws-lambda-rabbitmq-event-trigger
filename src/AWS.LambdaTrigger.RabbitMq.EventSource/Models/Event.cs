// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Models
{

public class Header1
    {
        [JsonPropertyName("bytes")]
        public List<int> Bytes { get; set; }
    }

    public class Header2
    {
        [JsonPropertyName("bytes")]
        public List<int> Bytes { get; set; }
    }

    public class Headers
    {
        [JsonPropertyName("header1")]
        public Header1 Header1 { get; set; }

        [JsonPropertyName("header2")]
        public Header2 Header2 { get; set; }

        [JsonPropertyName("numberInHeader")]
        public int NumberInHeader { get; set; }
    }

    public class BasicProperties
    {
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyName("contentEncoding")]
        public object ContentEncoding { get; set; }

        [JsonPropertyName("headers")]
        public Headers Headers { get; set; }

        [JsonPropertyName("deliveryMode")]
        public int DeliveryMode { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("correlationId")]
        public object CorrelationId { get; set; }

        [JsonPropertyName("replyTo")]
        public object ReplyTo { get; set; }

        [JsonPropertyName("expiration")]
        public string Expiration { get; set; }

        [JsonPropertyName("messageId")]
        public object MessageId { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("type")]
        public object Type { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("appId")]
        public object AppId { get; set; }

        [JsonPropertyName("clusterId")]
        public object ClusterId { get; set; }

        [JsonPropertyName("bodySize")]
        public int BodySize { get; set; }
    }

    public class Queue
    {
        [JsonPropertyName("basicProperties")]
        public BasicProperties BasicProperties { get; set; }

        [JsonPropertyName("redelivered")]
        public bool Redelivered { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }

    public class RmqMessagesByQueue
    {
        [JsonPropertyName("Queue::VHost")]
        public List<Queue> Queue { get; set; } = new List<Queue>();
    }

    public class Event
    {
        [JsonPropertyName("eventSource")]
        public string EventSource { get; set; }

        [JsonPropertyName("eventSourceArn")]
        public string EventSourceArn { get; set; }

        [JsonPropertyName("rmqMessagesByQueue")]
        public RmqMessagesByQueue RmqMessagesByQueue { get; set; }
    }

}