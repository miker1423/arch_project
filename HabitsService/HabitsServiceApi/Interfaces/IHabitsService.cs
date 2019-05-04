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

        int CreateHabit();

        Habit GetHabit();

        int UpdateHabit();

        int DeleteHabit();
    }
}
