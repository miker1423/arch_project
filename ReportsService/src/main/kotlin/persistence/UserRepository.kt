package persistence

import extensions.dayMillis
import models.*
import java.time.Instant
import java.util.*

object UserRepository {

    var allReports = mutableMapOf<String, User>()
    var userReport = UserReport()

    fun addTask(task: UserTask) {
        val userId = task.userId
        allReports[userId] = User(userId)
        with(allReports[userId]) {
            this!!.tasks[task.id] = task
            addTodayTask(task.id)
            addDelayedTask(task.id)
        }
    }

    fun addHabit(habit: UserHabit) {
        val userId = habit.userId
        allReports[userId] = User(userId)
        with(allReports[userId]) {
            this!!.id = id
            this!!.habits[habit.id] = habit
            addGoodHabits(habit.id)
            addBadHabits(habit.id)
        }
    }

    private fun addTodayTask(id: String){
        userReport.todayTasks = mutableMapOf()
        for((_, task) in allReports[id]!!.tasks){
            val result = task.dueDate.dayMillis - Date.from(Instant.now()).dayMillis
            if (result == 0L) {
                userReport.todayTasks[task.id] = task.title
            }
        }
    }

    private fun addDelayedTask(id: String){
        userReport.delayedTask = mutableMapOf()
        for((_, task) in allReports[id]!!.tasks){
            val result = task.dueDate.dayMillis - Date.from(Instant.now()).dayMillis
            if (result < 0) {
                userReport.delayedTask[task.id] = task.title
            }
        }
    }

    /*
    Good = 0, Bad = 1, Both = 2
    */
    private fun addGoodHabits(id: String){
        userReport.goodHabits = mutableMapOf()
        for ((_, habit) in allReports[id]!!.habits){
            if (habit.type == 0) userReport.goodHabits[habit.id] = habit.title
        }
    }

    private fun addBadHabits(id: String){
        userReport.badHabits = mutableMapOf()
        for ((_, habit) in allReports[id]!!.habits){
            if (habit.type == 1) userReport.badHabits[habit.id] = habit.title
        }
    }
}