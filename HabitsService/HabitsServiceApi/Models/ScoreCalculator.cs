using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public static class ScoreCalculator
    {
        public static double GetScore(Habit habit, bool add)
        {
            return new Algorithm(new Algorithm(null)).StartScoring(habit, habit.Score, add);
        }
    }
}
