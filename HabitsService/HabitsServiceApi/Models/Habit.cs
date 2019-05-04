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
        public string Id { get; set; }

        public Habit(int difficulty, string type, double score, string description, string title, string id)
        {
            this.SetDifficulty(difficulty);
            this.Settype(type);
            this.Setscore(score);
            this.Setdescription(description);
            this.Settitle(title);
            this.Setid(id);
        }

        public Habit()
        {

        }

        private void SetDifficulty(int value)
        {
            Difficulty = value;
        }

        private void Settype(string value)
        {
            Type = value;
        }

        private void Setscore(double value)
        {
            Score = value;
        }

        private void Setdescription(string value)
        {
            Description = value;
        }

        private void Settitle(string value)
        {
            Title = value;
        }

        private void Setid(string value)
        {
            Id = value;
        }
    }
}
