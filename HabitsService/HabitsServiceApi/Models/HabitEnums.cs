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
        Easy = 0,
        Medium = 1,
        Hard = 2
    } 

    public class Algorithm
    {
        private Algorithm _next;
        public Algorithm(Algorithm next) => _next = next;

        public double StartScoring(Habit habit, double partialScore) => GetScore(habit, _next, partialScore);

        public double GetScore(Habit habit, Algorithm algorithm, double partialScore)
        {
            CalculateScore(habit, ref partialScore);
            if (algorithm is null) return partialScore;
            else return algorithm.StartScoring(habit, partialScore);
        }

        public virtual void CalculateScore(Habit habit, ref double score)
        {
            //Marking habits as good
            if (true)
            {
                if (habit.Score > 40 && habit.Score <= 50)
                {
                    if (habit.Difficulty == HabitDifficulties.Easy)
                    {
                        score += 1;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Medium)
                    {
                        score += (3.0f / 2.0f);
                    }
                    else if (habit.Difficulty == HabitDifficulties.Hard)
                    {
                        score += (5.0f / 2.0f);
                    }
                }
                else if (habit.Score > 50)
                {
                    score += 1;
                }
                else
                {
                    if (habit.Difficulty == HabitDifficulties.Easy)
                    {
                        score = score + 2 * 0.25;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Medium)
                    {
                        score = score + 3 * 0.25;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Hard)
                    {
                        score = score + 5 * 0.25;
                    }
                }

            }
            //Marking habits as bad 
            else
            {
                if(habit.Score >= 0 && habit.Score < 10)
                {
                    if (habit.Difficulty == HabitDifficulties.Easy)
                    {
                        score = score - 2 * 1.5;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Medium)
                    {
                        score = score - 3 * 1.5;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Hard)
                    {
                        score = score - 5 * 1.5; 
                    }
                } else if (habit.Score < 0)
                {
                    if (habit.Difficulty == HabitDifficulties.Easy)
                    {
                        score = score - 2 * 2;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Medium)
                    {
                        score = score - 3 * 2;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Hard)
                    {
                        score = score - 5 * 2;
                    }
                }
                else
                {
                    if (habit.Difficulty == HabitDifficulties.Easy)
                    {
                        score = score - 2 * 0.25;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Medium)
                    {
                        score = score - 3 * 0.25;
                    }
                    else if (habit.Difficulty == HabitDifficulties.Hard)
                    {
                        score = score - 5 * 0.25;
                    }
                }
            }
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
