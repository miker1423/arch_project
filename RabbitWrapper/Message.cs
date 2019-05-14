using System;

namespace RabbitWrapper
{
    public class Message 
    {
        public EntityType Entity { get; set; }
        public double Score { get; set; }
        public Guid UserId { get; set; }
        public Guid EntityId { get; set; }
        public int HabitType { get; set; } 
        public bool Completed { get; set; }
        public string Title { get; set; }
        public long DueDate { get; set; } 
    }

    public enum EntityType 
    {
        Habit,
        Task
    }
}