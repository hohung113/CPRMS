using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Core.CPRMSServiceComponents.ServiceComponents.Kafka
{
    public abstract class KafkaConsumerHostedService<T> : BackgroundService
    {
        private readonly string _topic;
        private readonly IConsumer<Ignore, string> _consumer;

        protected KafkaConsumerHostedService(IConfiguration config, string topic)
        {
            _topic = topic;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = $"group-{typeof(T).Name}",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            _consumer.Subscribe(_topic);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    var message = JsonSerializer.Deserialize<T>(result.Message.Value);
                    HandleMessage(message);
                }
            }, stoppingToken);
        }

        protected abstract void HandleMessage(T message);
    }

}
