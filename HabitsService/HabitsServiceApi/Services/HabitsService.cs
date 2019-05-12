using HabitsServiceApi.Interfaces;
using HabitsServiceApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Services
{
    public class HabitsService : IHabitsService
    {
        private readonly HabitsContext _habitsContext;

        public HabitsService(HabitsContext habitsContext)
        {
            _habitsContext = habitsContext;
        }

        public Guid CreateHabit(Habit habit)
        {
            habit.Id = Guid.NewGuid();

            _habitsContext.Habits.Add(habit);
            _habitsContext.SaveChanges();
            return habit.Id;
        }

        public Habit DeleteHabit(Guid id)
        {
            Habit habitToRemove = _habitsContext.Habits.First(habit => habit.Id == id);
            _habitsContext.Habits.Remove(habitToRemove);
            _habitsContext.SaveChanges();
            return habitToRemove;
        }

        public IEnumerable<Habit> GetAllHabits() => _habitsContext.Habits.ToList();

        public Habit GetHabit(Guid habitId) 
            => _habitsContext.Habits.First(habit => habit.Id == habitId);

        public Habit UpdateHabit(Habit habit)
        {
            _habitsContext.Habits.Update(habit);
            _habitsContext.SaveChanges();

            return habit;
        }
    }
}
