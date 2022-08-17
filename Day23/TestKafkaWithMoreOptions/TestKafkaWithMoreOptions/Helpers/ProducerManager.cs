using Confluent.Kafka;

namespace TestKafkaWithMoreOptions.Helpers
{
    public class ProducerManager
    {
        private readonly string _topicName;
        private readonly ProducerConfig _config;
        private static readonly Random _random = new Random();
        private static ProducerBuilder<string, string> _producer;

        public ProducerManager(string topicName, ProducerConfig config)
        {
            _topicName = topicName;
            _config = config;
            _producer = new ProducerBuilder<string, string>(_config);
        }

        public async Task WriteMessage(string messageReceive)
        {

            var produceValue = await _producer.Build().ProduceAsync(_topicName, new Message<string, string>
            {
                Key = _random.Next(5).ToString(),
                Value = messageReceive
            });

            Console.WriteLine($"{produceValue.Offset} - {produceValue.TopicPartitionOffset} " +
                $"- {produceValue.Message.Value} - {produceValue.Message.Key}");

            return;
        }
    }
}
