using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CPRMSServiceComponents.ServiceComponents.Kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        public KafkaProducer(IConfiguration config)
        {
            var conf = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };
            _producer = new ProducerBuilder<Null, string>(conf).Build();
        }

        public async Task SendAsync<T>(string topic, T data)
        {
            var json = JsonSerializer.Serialize(data);
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = json });
        }
    }

}
