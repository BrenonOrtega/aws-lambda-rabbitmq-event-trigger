using System;
using System.Threading.Tasks;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Contracts
{
    public interface IEventRaiser
    {
        Task Raise();

        event Func<Task> MessageConsumed;
    }
}