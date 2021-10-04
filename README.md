# Lambda-QueueWatcher-BatchConsuming

## Objective

Develop an AWS Lambda Function that triggers when an "trigger queue" receive a message, start MassTransit bus, consumes all messages and when the queue is empty, shutdown the hosted application, freeing all the resources, preventing unnecessary billing.

## Tech Stack
 - .Net 5.0
 - .NET Core 3.1
 - AWS Lambda
 - SAM CLI
 - Flurl
 - RabbitMQ
 - MassTransit
 - 
