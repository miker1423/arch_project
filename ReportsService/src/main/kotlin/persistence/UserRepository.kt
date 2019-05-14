package persistence

import extensions.dayMillis
import models.*
import java.time.Instant
import java.util.*

object UserRepository {

    var allReports = mutableMapOf<String, User>()
    var userReport = mutableMapOf<String, UserReport>()

    fun addTask(task: UserTask) {
        val userId = task.userId
        allReports[userId] = User(userId)
        with(allReports[userId]) {
            this!!.tasks[task.id] = task
            if (userReport[userId] == null) userReport[id] = UserReport(id)
            addTodayTask(task.id)
            addDelayedTask(task.id)
        }
    }

    fun addHabit(habit: UserHabit) {
        val userId = habit.userId
        allReports[userId] = User(userId)
        with(allReports[userId]) {
            this!!.habits[habit.id] = habit
            if (userReport[userId] == null) userReport[id] = UserReport(id)
            addGoodHabits(habit.id)
            addBadHabits(habit.id)
        }
    }

    private fun addTodayTask(id: String){
        for((_, task) in allReports[id]!!.tasks){
            val result = task.dueDate.dayMillis - Date.from(Instant.now()).dayMillis
            if (result == 0L) {
                userReport[id]!!.todayTasks[task.id] = task.title
            }
        }
    }

    private fun addDelayedTask(id: String){
        for((_, task) in allReports[id]!!.tasks){
            val result = task.dueDate.dayMillis - Date.from(Instant.now()).dayMillis
            if (result < 0) {
                userReport[id]!!.delayedTask[task.id] = task.title
            }
        }
    }

    /* Good = 0, Bad = 1, Both = 2 */
    private fun addGoodHabits(id: String){
        for ((_, habit) in allReports[id]!!.habits){
            if (habit.type == 0) userReport[id]!!.goodHabits[habit.id] = habit.title
        }
    }

    private fun addBadHabits(id: String){
        for ((_, habit) in allReports[id]!!.habits){
            if (habit.type == 1) userReport[id]!!.badHabits[habit.id] = habit.title
        }
    }
}