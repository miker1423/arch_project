﻿using System;
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

        // GET api/habits/{userId}
        [HttpGet("user/{id}")]
        public IActionResult GetHabitsByUserId(string id)
        {
            if (Guid.TryParse(id, out Guid userId))
            {
                IEnumerable<Habit> foundHabits = _habitsService.GetHabitsByUserId(userId);
                if (foundHabits != null && !foundHabits.Any()) return NotFound();
                return Ok(foundHabits);
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

        // PUT api/habits/score/add
        [HttpPut("score/add/{id}")]
        public IActionResult AddScore(string id)
        {
            if (Guid.TryParse(id, out Guid habitId))
            {
                Habit updatedHabit = _habitsService.UpdateScore(habitId, true);
                return Ok(updatedHabit);
            }
            return BadRequest();
        }

        // PUT api/habits/score/substract
        [HttpPut("score/substract/{id}")]
        public IActionResult SubstractScore(string id)
        {
            if (Guid.TryParse(id, out Guid habitId))
            {
                Habit updatedHabit = _habitsService.UpdateScore(habitId, false);
                return Ok(updatedHabit);
            }
            return BadRequest();
        }

        // DELETE api/habits/{habitId}
        [HttpDelete("{id}")]
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
