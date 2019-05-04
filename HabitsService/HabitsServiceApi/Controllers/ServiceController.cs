using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HabitsServiceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HabitsServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly HabitsContext _habitsContext; 
        public ServiceController(HabitsContext habitsContext)
        {
            _habitsContext = habitsContext;
        }
        // api/habits
        [HttpGet]
        public ActionResult<IEnumerable<Habit>> Get()
        {
            _habitsContext.Habits.Add(null);
            _habitsContext.SaveChanges();
            return new Collection<Habit>();
        }

        // GET api/habits/{id}
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/habits/{user}
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/habits/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Habit value)
        {
        }

        // DELETE api/habits/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Habit habitToRemove = _habitsContext.Habits.Where(habit => habit.Id == id).First();
            _habitsContext.Habits.Remove(habitToRemove);
            await _habitsContext.SaveChangesAsync();
            return Ok(habitToRemove);
        }
    }
}
