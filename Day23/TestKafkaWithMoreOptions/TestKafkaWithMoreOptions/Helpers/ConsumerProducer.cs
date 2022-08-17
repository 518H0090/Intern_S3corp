using Confluent.Kafka;
using Newtonsoft.Json;

namespace TestKafkaWithMoreOptions.Helpers
{
    public class ConsumerProducer
    {
        private readonly string _topicName;
        private readonly ConsumerConfig _config;
        private static ConsumerBuilder<string, string> _consumer;

        public ConsumerProducer(string topicName, ConsumerConfig config)
        {
            _topicName = topicName;
            _config = config;
            _consumer = new ConsumerBuilder<string, string>(_config);
        }

        public KeyAndValue ReadMessage()
        {
            var consumerBuild = _consumer.Build();
            consumerBuild.Subscribe(_topicName);

            CancellationTokenSource cts = new CancellationTokenSource();

            //Console.CancelKeyPress += (_, e) => {
            //    e.Cancel = true; // prevent the process from terminating.
            //    cts.Cancel();
            //};

            try
            {
                var consumeResult = consumerBuild.Consume(cts.Token);

                return new KeyAndValue
                {
                    KeyKafka = consumeResult.Value,
                    ValueKafka = consumeResult.Value
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                consumerBuild.Close();
                return null;
            }
          
        }
    }
}
