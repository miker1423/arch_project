package persistence

import models.UserHabit
import models.UsersHabitReport

object AdminHabitReportRepository {

    var allHabits: MutableList<UserHabit> = ArrayList()
    var habitsReport: UsersHabitReport = UsersHabitReport()

    fun addHabit(habit: UserHabit){
        allHabits.add(habit)
        checkBestHabit(habit)
        checkWorstHabit(habit)
        updateHabitsRange(habit)
    }

    private fun checkBestHabit(habit: UserHabit){
        if (habitsReport.bestHabit == null) {
            habitsReport.bestHabit = habit
        }
        for (habit in allHabits) {
            if (habit.score > habitsReport.bestHabit!!.score) {
                habitsReport.bestHabit = habit
            }
        }
    }

    private fun checkWorstHabit(habit: UserHabit){
        if (habitsReport.worstHabit == null) {
            habitsReport.worstHabit = habit
        }
        for (habit in allHabits) {
            if (habit.score < habitsReport.worstHabit!!.score) {
                habitsReport.worstHabit = habit
            }
        }
    }

    private fun updateHabitsRange(habit: UserHabit){
        val score = habit.score
        when {
            (score < 0) -> habitsReport.habitsPerRange.redRange++
            (score in 0..10) -> habitsReport.habitsPerRange.orangeRange++
            (score in 11..40) -> habitsReport.habitsPerRange.yellowRange++
            (score in 41..50) -> habitsReport.habitsPerRange.greenRange++
            (score > 50) -> habitsReport.habitsPerRange.blueRange++
        }
        /*
        Red: Negative
        Orange: 0 - 10
        Yellow: 11 - 40
        Green: 41 - 50
        Blue: 51+
        */
    }
}
