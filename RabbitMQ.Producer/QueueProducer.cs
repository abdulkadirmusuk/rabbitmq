using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)//IModel Rabbit mq dan gelen bir interface
        {
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);//kanal özelliklerini tanımladık
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };//kanallardan dağıtılacak mesaj,
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));//Mesaj nesnesi serileştirilir.
                channel.BasicPublish("", "demo-queue", null, body);//producer mesajı publish eder
                count++;
                Thread.Sleep(1000);//1 saniye uyku
            }

            
        }
    }
}
