using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace vader
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqp://dvquwasp:uhWWbvjH8vteZQXRShimYxHeNNyW02OR@wombat.rmq.cloudamqp.com/dvquwasp");

            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "hello-father",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message received: \"{message}\"");
                };
                channel.BasicConsume(
                    queue: "hello-father",
                    autoAck: true,
                    consumer: consumer
                );

                Console.WriteLine("Now receiving new children");
                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
