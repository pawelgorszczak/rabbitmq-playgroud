using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

System.Console.WriteLine("Waiting for messages");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) => {
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    System.Console.WriteLine($"Received message - '{message}'");
};

channel.BasicConsume(
    queue: "hello-world-queue",
    autoAck: true,
    consumer: consumer
);

System.Console.WriteLine("Press key to exit");
System.Console.ReadLine();