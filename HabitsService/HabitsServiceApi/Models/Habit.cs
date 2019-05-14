using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public class Habit
    {
        public Guid UserId { get; set; }
        public HabitDifficulties Difficulty { get; set; }
        public HabitTypes Type { get; set; }
        public double Score { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }

        private static Habit _emptyHabit = new Habit();

        public Habit(int difficulty, int type, double score, string description, string title, Guid id, Guid userId)
        {
            Difficulty = (HabitDifficulties) difficulty;
            Type = (HabitTypes) type;
            Score = score;
            Description = description;
            Title = title;
            Id = id;
            UserId = userId;
        }

        public Habit()
        {

        }

        public static Habit EmptyHabit()
        {
            return _emptyHabit;
        }
    }
}
