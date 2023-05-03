using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() {
  HostName = "rabbit-test-instance"  
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
  queue: "hello-world-queue",
  durable: true,
  exclusive: false,
  autoDelete: false,
  arguments: null
);

const string message = "Hello World! - first message in rabbit MQ";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(
  exchange: string.Empty,
  routingKey: "hello-world-queue",
  basicProperties: null,
  body: body
);

System.Console.WriteLine($"Message sent! - '{message}'");