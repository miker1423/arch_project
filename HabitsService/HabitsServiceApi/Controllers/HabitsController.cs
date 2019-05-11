using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HabitsServiceApi.Interfaces;
using HabitsServiceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HabitsServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : Controller
    {
        private IHabitsService _habitsService;
        public HabitsController(IHabitsService habitsService)
        {
            _habitsService = habitsService;
        }
        // api/habits
        [HttpGet]
        public IActionResult Get() => Ok(_habitsService.GetAllHabits());
        
        // GET api/habits/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if(Guid.TryParse(id, out Guid habitId))
            {
                Habit foundHabit = _habitsService.GetHabit(habitId);
                if (foundHabit is null) return NotFound();
                return Ok(foundHabit); 
            }            
            return BadRequest();
        }

        // POST api/habits/
        [HttpPost]
        public IActionResult Post([FromBody] Habit habit)
        {
            Guid habitId =_habitsService.CreateHabit(habit);
            return Created($"api/habits/{habitId}", habit);
        }

        // PUT api/habits/
        [HttpPut]
        public IActionResult Put([FromBody] Habit value)
        {
            if (value is null) return BadRequest();
            Habit updatedHabit = _habitsService.UpdateHabit(value);
            return Ok(updatedHabit);
        }

        // DELETE api/habits/{habitId}
        [HttpDelete("{habitId}")]
        public IActionResult Delete(string id)
        {
            if (Guid.TryParse(id, out Guid habitId))
            {
                Habit deletedHabit = _habitsService.DeleteHabit(habitId);
                return Ok(deletedHabit);
            }

            return BadRequest();
        }
    }
}
