using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using AWS.LambdaTrigger.RabbitMq.EventSource;
using System.Text;
using System.Text.Json;
using AWS.LambdaTrigger.RabbitMq.EventSource.Models;

namespace AWS.LambdaTrigger.RabbitMq.EventSource.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

          /*   // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = function.FunctionHandler("hello world", context);
            Assert.Equal("HELLO WORLD", upperCase); */
        }

        [Fact]
        public async Task EventClassShouldDeserialize()
        {
            var messageData = "VGhpcyBpcyBhIGJhc2UgNjQgZW5jb2RlciBqdXN0IHRvIHRlc3Q=";
            
            var eventData = "{\"eventSource\":\"aws:rmq\",\"eventSourceArn\":\"arn:aws:mq:us-west-2:112556298976:broker:pizzaBroker:b-9bcfa592-423a-4942-879d-eb284b418fc8\",\"rmqMessagesByQueue\":{\"Queue::VHost\":[{\"basicProperties\":{\"contentType\":\"text/plain\",\"contentEncoding\":null,\"headers\":{\"header1\":{\"bytes\":[118,97,108,117,101,49]},\"header2\":{\"bytes\":[118,97,108,117,101,50]},\"numberInHeader\":10},\"deliveryMode\":1,\"priority\":34,\"correlationId\":null,\"replyTo\":null,\"expiration\":\"60000\",\"messageId\":null,\"timestamp\":\"Jan 1, 1970, 12:33:41 AM\",\"type\":null,\"userId\":\"AIDACKCEVSQ6C2EXAMPLE\",\"appId\":null,\"clusterId\":null,\"bodySize\":80},\"redelivered\":false,\"data\":\"VGhpcyBpcyBhIGJhc2UgNjQgZW5jb2RlciBqdXN0IHRvIHRlc3Q=\"}]}}";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(eventData));
            Event @event = null;
           
            try
            {
                @event = await JsonSerializer.DeserializeAsync<Event>(stream);
            } catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            var base64Message = @event.RmqMessagesByQueue.Queue.FirstOrDefault().Data;
            var actual = Encoding.UTF8.GetString(Convert.FromBase64String(base64Message));
            var expected = "This is a base 64 encoder just to test";

            Assert.Equal(expected, actual);
        }
    }
}
