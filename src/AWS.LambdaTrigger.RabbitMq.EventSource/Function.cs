using System.Threading.Tasks;
using Amazon.Lambda.Core;
using System.IO;
using System.Text.Json;
using System;
using AWS.LambdaTrigger.RabbitMq.EventSource.Models;
using System.Linq;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWS.LambdaTrigger.RabbitMq.EventSource
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(Stream data, ILambdaContext context)
        {
            var useHost = false;
            var log = new Action<string>(context.Logger.LogLine);
            var @event = await JsonSerializer.DeserializeAsync<Event>(data);

            var encodedMessages = @event.RmqMessagesByQueue.Queue.Select(message => Convert.FromBase64String(message.Data));
            var messages = encodedMessages.Select(message => Encoding.UTF8.GetString(message));

            foreach(var message in messages)
                log(message);

            if (useHost)
                await Program.HostStart(data, context);

            context.Logger.Log("Ending Lambda Function Invocation.");
        }
    }
}
