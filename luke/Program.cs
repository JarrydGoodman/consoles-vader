using System;
using System.Text;
using RabbitMQ.Client;

namespace luke
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Who are you?");
            string name = Console.ReadLine();

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

                string message = $"Hello my name is, {name}.";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "hello-father",
                    basicProperties: null,
                    body: body
                );
            }

            Console.WriteLine("Message sent to: [not-your-father]");
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
