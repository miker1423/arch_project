package models

data class TaskReport(
    val date: String,
    val description: String
)

data class HabitReport(
    val type: Int,
    val description: String
)

data class UserReport(
    val id: String,
    val tasks: Array<TaskReport>,
    val habits: Array<HabitReport>
)