using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public class Habit
    {
        public Guid UserId { get; set; }
        public int Difficulty { get; set; }
        public string Type { get; set; }
        public double Score { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }

        private static Habit _emptyHabit = new Habit();

        public Habit(int difficulty, string type, double score, string description, string title, Guid id)
        {
            Difficulty = difficulty;
            Type = type;
            Score = score;
            Description = description;
            Title = title;
            Id = id;
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
