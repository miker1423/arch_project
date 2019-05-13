package persistence

import models.UsersTaskReport

class AdminTaskReportRepository {

    private var tasksReport: UsersTaskReport = UsersTaskReport()


    fun updateTasksReport(newReport: UsersTaskReport){
        tasksReport = newReport
    }

}