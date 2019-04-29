using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public class Habit
    {
        private int difficulty;
        private string type;
        private double score;
        private string description;
        private string title;
        private string id;

        public Habit(int difficulty, string type, double score, string description, string title, string id)
        {
            this.SetDifficulty(difficulty);
            this.Settype(type ?? throw new ArgumentNullException(nameof(type)));
            this.Setscore(score);
            this.Setdescription(description ?? throw new ArgumentNullException(nameof(description)));
            this.Settitle(title ?? throw new ArgumentNullException(nameof(title)));
            this.Setid(id ?? throw new ArgumentNullException(nameof(id)));
        }

        public Habit()
        {

        }

        private int GetDifficulty()
        {
            return difficulty;
        }

        private void SetDifficulty(int value)
        {
            difficulty = value;
        }

        private string Gettype()
        {
            return type;
        }

        private void Settype(string value)
        {
            type = value;
        }

        private double Getscore()
        {
            return score;
        }

        private void Setscore(double value)
        {
            score = value;
        }

        private string Getdescription()
        {
            return description;
        }

        private void Setdescription(string value)
        {
            description = value;
        }

        private string Gettitle()
        {
            return title;
        }

        private void Settitle(string value)
        {
            title = value;
        }

        private string Getid()
        {
            return id;
        }

        private void Setid(string value)
        {
            id = value;
        }
    }
}
