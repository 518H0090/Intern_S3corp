using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestTopicKafkaForNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ProducerConfig _config;
        private readonly string _topicName = "testopicoptions";

        public EmployeeController(ProducerConfig config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("Send")]
        public async Task<IActionResult> Get([FromBody] Employee employee)
        {
            string seriallizeEmployee = JsonConvert.SerializeObject(employee);

            using (var producer = new ProducerBuilder<Null,string>(_config).Build())
            {
                await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = seriallizeEmployee});
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);    
            }
        }
    }
}
