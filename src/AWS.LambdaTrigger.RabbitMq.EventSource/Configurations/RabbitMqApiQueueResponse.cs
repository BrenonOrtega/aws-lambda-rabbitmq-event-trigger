using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Configurations
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Arguments
    {
        [JsonPropertyName("x-queue-type")]
        public string XQueueType { get; set; }
    }

    public class BackingQueueStatus
    {
        [JsonPropertyName("avg_ack_egress_rate")]
        public int AvgAckEgressRate { get; set; }

        [JsonPropertyName("avg_ack_ingress_rate")]
        public int AvgAckIngressRate { get; set; }

        [JsonPropertyName("avg_egress_rate")]
        public int AvgEgressRate { get; set; }

        [JsonPropertyName("avg_ingress_rate")]
        public int AvgIngressRate { get; set; }

        [JsonPropertyName("delta")]
        public List<object> Delta { get; set; }

        [JsonPropertyName("len")]
        public int Len { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("next_seq_id")]
        public int NextSeqId { get; set; }

        [JsonPropertyName("q1")]
        public int Q1 { get; set; }

        [JsonPropertyName("q2")]
        public int Q2 { get; set; }

        [JsonPropertyName("q3")]
        public int Q3 { get; set; }

        [JsonPropertyName("q4")]
        public int Q4 { get; set; }

        [JsonPropertyName("target_ram_count")]
        public string TargetRamCount { get; set; }
    }

    public class EffectivePolicyDefinition
    {
        [JsonPropertyName("queue-mode")]
        public string QueueMode { get; set; }
    }

    public class GarbageCollection
    {
        [JsonPropertyName("fullsweep_after")]
        public int FullsweepAfter { get; set; }

        [JsonPropertyName("max_heap_size")]
        public int MaxHeapSize { get; set; }

        [JsonPropertyName("min_bin_vheap_size")]
        public int MinBinVheapSize { get; set; }

        [JsonPropertyName("min_heap_size")]
        public int MinHeapSize { get; set; }

        [JsonPropertyName("minor_gcs")]
        public int MinorGcs { get; set; }
    }

    public class AckDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class DeliverDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class DeliverGetDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class DeliverNoAckDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class GetDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class GetEmptyDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class GetNoAckDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class PublishDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class RedeliverDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class MessageStats
    {
        [JsonPropertyName("ack")]
        public int Ack { get; set; }

        [JsonPropertyName("ack_details")]
        public AckDetails AckDetails { get; set; }

        [JsonPropertyName("deliver")]
        public int Deliver { get; set; }

        [JsonPropertyName("deliver_details")]
        public DeliverDetails DeliverDetails { get; set; }

        [JsonPropertyName("deliver_get")]
        public int DeliverGet { get; set; }

        [JsonPropertyName("deliver_get_details")]
        public DeliverGetDetails DeliverGetDetails { get; set; }

        [JsonPropertyName("deliver_no_ack")]
        public int DeliverNoAck { get; set; }

        [JsonPropertyName("deliver_no_ack_details")]
        public DeliverNoAckDetails DeliverNoAckDetails { get; set; }

        [JsonPropertyName("get")]
        public int Get { get; set; }

        [JsonPropertyName("get_details")]
        public GetDetails GetDetails { get; set; }

        [JsonPropertyName("get_empty")]
        public int GetEmpty { get; set; }

        [JsonPropertyName("get_empty_details")]
        public GetEmptyDetails GetEmptyDetails { get; set; }

        [JsonPropertyName("get_no_ack")]
        public int GetNoAck { get; set; }

        [JsonPropertyName("get_no_ack_details")]
        public GetNoAckDetails GetNoAckDetails { get; set; }

        [JsonPropertyName("publish")]
        public int Publish { get; set; }

        [JsonPropertyName("publish_details")]
        public PublishDetails PublishDetails { get; set; }

        [JsonPropertyName("redeliver")]
        public int Redeliver { get; set; }

        [JsonPropertyName("redeliver_details")]
        public RedeliverDetails RedeliverDetails { get; set; }
    }

    public class MessagesDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class MessagesReadyDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class MessagesUnacknowledgedDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class ReductionsDetails
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }

    public class QueueStatus
    {
        [JsonPropertyName("consumer_details")]
        public List<object> ConsumerDetails { get; set; }

        [JsonPropertyName("arguments")]
        public Arguments Arguments { get; set; }

        [JsonPropertyName("auto_delete")]
        public bool AutoDelete { get; set; }

        [JsonPropertyName("backing_queue_status")]
        public BackingQueueStatus BackingQueueStatus { get; set; }

        [JsonPropertyName("consumer_capacity")]
        public int ConsumerCapacity { get; set; }

        [JsonPropertyName("consumer_utilisation")]
        public int ConsumerUtilisation { get; set; }

        [JsonPropertyName("consumers")]
        public int Consumers { get; set; }

        [JsonPropertyName("deliveries")]
        public List<object> Deliveries { get; set; }

        [JsonPropertyName("durable")]
        public bool Durable { get; set; }

        [JsonPropertyName("effective_policy_definition")]
        public EffectivePolicyDefinition EffectivePolicyDefinition { get; set; }

        [JsonPropertyName("exclusive")]
        public bool Exclusive { get; set; }

        [JsonPropertyName("exclusive_consumer_tag")]
        public object ExclusiveConsumerTag { get; set; }

        [JsonPropertyName("garbage_collection")]
        public GarbageCollection GarbageCollection { get; set; }

        [JsonPropertyName("head_message_timestamp")]
        public object HeadMessageTimestamp { get; set; }

        [JsonPropertyName("idle_since")]
        public string IdleSince { get; set; }

        [JsonPropertyName("incoming")]
        public List<object> Incoming { get; set; }

        [JsonPropertyName("memory")]
        public int Memory { get; set; }

        [JsonPropertyName("message_bytes")]
        public int MessageBytes { get; set; }

        [JsonPropertyName("message_bytes_paged_out")]
        public int MessageBytesPagedOut { get; set; }

        [JsonPropertyName("message_bytes_persistent")]
        public int MessageBytesPersistent { get; set; }

        [JsonPropertyName("message_bytes_ram")]
        public int MessageBytesRam { get; set; }

        [JsonPropertyName("message_bytes_ready")]
        public int MessageBytesReady { get; set; }

        [JsonPropertyName("message_bytes_unacknowledged")]
        public int MessageBytesUnacknowledged { get; set; }

        [JsonPropertyName("message_stats")]
        public MessageStats MessageStats { get; set; }

        [JsonPropertyName("messages")]
        public int Messages { get; set; }

        [JsonPropertyName("messages_details")]
        public MessagesDetails MessagesDetails { get; set; }

        [JsonPropertyName("messages_paged_out")]
        public int MessagesPagedOut { get; set; }

        [JsonPropertyName("messages_persistent")]
        public int MessagesPersistent { get; set; }

        [JsonPropertyName("messages_ram")]
        public int MessagesRam { get; set; }

        [JsonPropertyName("messages_ready")]
        public int MessagesReady { get; set; }

        [JsonPropertyName("messages_ready_details")]
        public MessagesReadyDetails MessagesReadyDetails { get; set; }

        [JsonPropertyName("messages_ready_ram")]
        public int MessagesReadyRam { get; set; }

        [JsonPropertyName("messages_unacknowledged")]
        public int MessagesUnacknowledged { get; set; }

        [JsonPropertyName("messages_unacknowledged_details")]
        public MessagesUnacknowledgedDetails MessagesUnacknowledgedDetails { get; set; }

        [JsonPropertyName("messages_unacknowledged_ram")]
        public int MessagesUnacknowledgedRam { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("node")]
        public string Node { get; set; }

        [JsonPropertyName("operator_policy")]
        public object OperatorPolicy { get; set; }

        [JsonPropertyName("policy")]
        public string Policy { get; set; }

        [JsonPropertyName("recoverable_slaves")]
        public object RecoverableSlaves { get; set; }

        [JsonPropertyName("reductions")]
        public int Reductions { get; set; }

        [JsonPropertyName("reductions_details")]
        public ReductionsDetails ReductionsDetails { get; set; }

        [JsonPropertyName("single_active_consumer_tag")]
        public object SingleActiveConsumerTag { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("vhost")]
        public string Vhost { get; set; }
    }


}