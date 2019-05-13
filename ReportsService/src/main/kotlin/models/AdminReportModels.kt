package models

data class HabitTuple(
    val level: Int,
    var count: Int
)

data class UserHabitTuple(
    val username: String,
    val level: Int,
    val description: String
)

data class UsersHabitReport (
    var allHabits: Array<HabitTuple>,
    var worstHabitsPerUser: Array<UserHabitTuple>,
    var bestHabitsPerUser: Array<UserHabitTuple>
)

data class UsersTaskReport(
    var completedBeforeDue: Int,
    var completedAfterDue: Int,
    var delayedTask: Int,
    var availableTask: Int,
    var availableToBeDone: Int
)