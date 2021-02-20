using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            //TTL Time to leave. Producer ın yayın yapmayı bırakacğı süreyi belirleriz. Şöyle ki;
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",3000}
            };//3 saniye yayın yap sonra dur demek.Sonra aşağıda exchangeDeclare de arguments : ttl olarak bunu veririz

            //channel.ExchangeDeclare ile burada bir exchange işlemi yaptığımı bildiriyoruz
            channel.ExchangeDeclare("demo-direct-exchange",ExchangeType.Direct,arguments:ttl);//kanal özelliklerini tanımladık
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-direct-exchange", "account.init", null, body);//producer mesajı publish eder. Bir Exchange ve RoutingKey verdik
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
