using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public enum HabitTypes
    {
        Good,
        Bad,
        Both
    }

    public enum HabitDifficulties
    {
        Easy,
        Medium,
        Hard
    }

    public class Algorithm
    {
        private Algorithm _next;
        public Algorithm(Algorithm next) => _next = next;

        public double StartScoring(Habit habit, double partialScore) => GetScore(habit, _next, partialScore);

        public double GetScore(Habit habit, Algorithm algorithm, double partailScore)
        {
            CalculateScore(habit, ref partailScore);
            if (algorithm is null) return partailScore;
            else return algorithm.StartScoring(habit, partailScore);
        }

        public virtual void CalculateScore(Habit habit, ref double score)
        {
            score = 193;
        }

        public class Algorithm2 : Algorithm
        {
            public Algorithm2(Algorithm next) : base(next) {}

            public override void CalculateScore(Habit habit, ref double score)
            {
                base.CalculateScore(habit, ref score);
            }
        }
    }

    public static class ScoreCalculator
    {
        public static double GetScore(Habit habit)
        {
            return new Algorithm(new Algorithm(null)).StartScoring(habit, habit.Score);
        }
    }
}
