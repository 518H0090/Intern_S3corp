using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KafkaProducerForNetCore.Models.DatabaseContext;
using KafkaProducerForNetCore.Models.DatabaseModels;
using Newtonsoft.Json;
using Confluent.Kafka;
using System.Net;

namespace KafkaProducerForNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly string topicKafka = "Hieu518H0090";

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        [Route("GetAllEmployee")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet]
        [Route("GetEmployeById/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("EditEmployee/{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'DataContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        /// <summary>
        /// Function use to update value to Kafka
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("PostToKafka")]
        public async Task<IActionResult> PostToKafka([FromBody] Employee request)
        {
            var serializedEmployee = JsonConvert.SerializeObject(request);

            var configProducer = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };

            var producer = new ProducerBuilder<Null, string>(configProducer).Build();
            var testValue = await producer.ProduceAsync(topicKafka, new Message<Null, string> { Value = serializedEmployee });

            producer.Flush(TimeSpan.FromSeconds(2));

            return Ok(new
            {
                Message = testValue.Message,
                Key = testValue.Key,
                OffsetNumber = testValue.Offset
            });

        }
    }
}
