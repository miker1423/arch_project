package persistence

import extensions.dayMillis
import models.UserTask
import models.UsersTaskReport
import java.time.Instant
import java.util.*

object TaskRepository {

    var allTasks = mutableMapOf<String, UserTask>()
    var tasksReport = UsersTaskReport()

    fun addTask(task: UserTask) {
        val datedTask  = if (task.complete) task.copy(completionDate = Date.from(Instant.now())) else task
        allTasks[datedTask.id] = datedTask
        runReports()
    }

    private fun runReports() {
        tasksReport = UsersTaskReport()
        checkCompletedTasks()
        checkDelayedTasks()
        checkTodayTasks()
    }

    private fun checkTodayTasks() {
        allTasks.forEach {
            val (key, value) = it
            if (value.complete) return
            val result = value.dueDate.dayMillis - Date.from(Instant.now()).dayMillis
            tasksReport = if (result == 0L) {
                tasksReport.copy(availableTodayTask = tasksReport.availableTodayTask + 1)
            } else tasksReport
        }
    }

    private fun checkDelayedTasks() {
        allTasks.forEach {
            val (key, value) = it
            if (value.complete) return
            val result = value.dueDate.dayMillis - Date.from(Instant.now()).dayMillis
            tasksReport = if (result < 0) {
                tasksReport.copy(delayedTask = tasksReport.delayedTask + 1)
            } else {
                tasksReport.copy(availableTask = tasksReport.availableTask + 1)
            }
        }
    }

    private fun checkCompletedTasks() {
        allTasks.forEach {
            val (key, value) = it
            if (!value.complete) return
            val result = value.completionDate!!.dayMillis - value.dueDate.dayMillis
            tasksReport = if (result < 0) {
                tasksReport.copy(completedBeforeDue = tasksReport.completedBeforeDue + 1)
            } else {
                tasksReport.copy(completedAfterDue = tasksReport.completedAfterDue + 1)
            }
        }
    }

}