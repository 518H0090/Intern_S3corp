using Confluent.Kafka;
using System;

namespace ConsumerKafkaNet
{
    class Program
    {
        private static readonly string bootstrapServer = "localhost:9092";
        private static readonly string topicKafka = "Hieu518H0090";

        static void Main(string[] args)
        {
            var configConsumer = new ConsumerConfig
            {
                BootstrapServers = bootstrapServer,
                GroupId = "hieu-id",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumerKafka = new ConsumerBuilder<Null, string>(configConsumer).Build();

            consumerKafka.Subscribe(topicKafka);

            while(consumerKafka != null)
            {
                var value = consumerKafka.Consume();
                Console.WriteLine(new 
                {
                    Message = value.Message,
                    Offset = value.Offset,
                    Value = value.Message.Value
                });
            }
        }
    }
}