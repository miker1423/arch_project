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
    override var id: String,
    override var userId: String,
    val title: String,
    val score: Int,
    val type: Int
): IdentifiableEntity

data class UserTask(
    override var id: String,
    override var userId: String,
    val title: String,
    val complete: Boolean,
    val dueDate: Date,
    val completionDate: Date? = null
): IdentifiableEntity

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

data class User(
    var id: String = "",
    val tasks: MutableMap<String, UserTask> = mutableMapOf<String, UserTask>(),
    val habits: MutableMap<String, UserHabit> = mutableMapOf<String, UserHabit>()
)

data class UserReport(
    var id: String = "",
    var todayTasks: MutableMap<String, String> = mutableMapOf(),
    var delayedTask: MutableMap<String, String> = mutableMapOf(),
    var goodHabits: MutableMap<String, String> = mutableMapOf(),
    var badHabits: MutableMap<String, String> = mutableMapOf()
)

interface IdentifiableEntity {
    var id: String
    var userId: String
}