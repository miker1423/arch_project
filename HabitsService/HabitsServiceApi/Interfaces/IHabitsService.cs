using HabitsServiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Interfaces
{
    public interface IHabitsService
    {
        IEnumerable<Habit> GetAllHabits();

        Guid CreateHabit(Habit habit);

        Habit GetHabit(Guid id);

        Habit UpdateHabit(Habit habit);

        Habit DeleteHabit(Guid id);
    }
}
