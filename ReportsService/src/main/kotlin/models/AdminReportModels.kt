package models

import java.time.Instant
import java.util.*

data class HabitsPerRange(
    val redRange: Int = 0,
    val orangeRange: Int = 0,
    val yellowRange: Int = 0,
    val greenRange: Int = 0,
    val blueRange: Int = 0
)

data class UserHabit(
    val id: String,
    val title: String,
    val score: Int,
    val userId: String
)

data class UserTask(
    val id: String,
    val complete: Boolean,
    val dueDate: Date,
    val completionDate: Date? = null
)

data class UsersHabitReport (
    var habitsPerRange: HabitsPerRange = HabitsPerRange(),
    var worstHabit: UserHabit? = null,
    var bestHabit: UserHabit? = null
)

data class UsersTaskReport(
    val completedBeforeDue: Int = 0,
    val completedAfterDue: Int = 0,
    val delayedTask: Int = 0,
    val availableTask: Int = 0,
    val availableTodayTask: Int = 0
)