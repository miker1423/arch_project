package models

enum class EntityType {
    Task, Habit
}

data class Message(
    val Entity: EntityType,
    val Score: Int,
    val UserId: String,
    val EntityId: String,
    val HabitType: Int,
    val Completed: Boolean,
    val Title: String,
    val DueDate: Long
)