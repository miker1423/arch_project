
namespace RabbitWrapper
{
    public class Message 
    {
        public EntityType Entity { get; set; }
        public HabitMessageType HabitType { get; set; }
    }

    public enum EntityType 
    {
        Habit,
        Task
    }

    public enum HabitMessageType 
    {
        
    }
}