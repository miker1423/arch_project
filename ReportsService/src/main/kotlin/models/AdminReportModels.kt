package models

data class HabitsPerRange(
    var redRange: Int = 0,
    var orangeRange: Int = 0,
    var yellowRange: Int = 0,
    var greenRange: Int = 0,
    var blueRange: Int = 0
)

data class UserHabit(
    val title: String,
    val score: Int,
    val username: String
)

data class UsersHabitReport (
    var habitsPerRange: HabitsPerRange = HabitsPerRange(),
    var worstHabit: UserHabit? = null,
    var bestHabit: UserHabit? = null
)

data class UsersTaskReport(
    var completedBeforeDue: Int = 0,
    var completedAfterDue: Int = 0,
    var delayedTask: Int = 0,
    var availableTask: Int = 0,
    var availableToBeDone: Int = 0
)