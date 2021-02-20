using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Producer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory 
            { 
                //Docker Container Port Dinleniyor..
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection(); //factory ile bağlantı oluşturuyorum
            using var channel = connection.CreateModel();//kanal oluşturdum

            //Burayı tek consumer ve producer olduğunda tanımlamıştık. Bunu QueueProducer methodune bağladık artık
            /*channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false, 
                autoDelete: false, 
                arguments: null);//kanal özelliklerini tanımladık
            var message = new { Name = "message", Message = "Hello!" };//kanallardan dağıtılacak mesaj,
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));//Mesaj nesnesi serileştirilir.
            channel.BasicPublish("", "demo-queue", null, body);//producer mesajı publish eder*/


            //QueueProducer.Publish(channel);//yukarıda yorum satırına aldığım kodu çoklu mesaj için böyle kullandım

            //QueueProducer.Publish exchange olmadan kanallara dağıtım yapar aşağıdaki directexchangepublisher ise sadece belirli özellikleri alır

            DirectExchangePublisher.Publish(channel);

        }
    }
}
