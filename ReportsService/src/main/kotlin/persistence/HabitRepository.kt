package persistence

import models.HabitsPerRange
import models.UserHabit
import models.UsersHabitReport
import java.lang.Exception

object HabitRepository {

    var allHabits = mutableMapOf<String, UserHabit>()
    var habitsReport = UsersHabitReport()

    fun addHabit(habit: UserHabit){
        allHabits[habit.id] = habit
        checkBestHabit(habit)
        checkWorstHabit(habit)
        updateHabitsRange()
    }

    private fun checkBestHabit(habit: UserHabit){
        if (habitsReport.bestHabit == null) {
            habitsReport.bestHabit = habit
        }
        for ((_, habit) in allHabits) {
            if (habit.score > habitsReport.bestHabit!!.score) {
                habitsReport.bestHabit = habit
            }
        }
    }

    private fun checkWorstHabit(habit: UserHabit){
        if (habitsReport.worstHabit == null) {
            habitsReport.worstHabit = habit
        }
        for ((_, habit) in allHabits) {
            if (habit.score < habitsReport.worstHabit!!.score) {
                habitsReport.worstHabit = habit
            }
        }
    }

    private fun updateHabitsRange(){
        habitsReport.habitsPerRange = HabitsPerRange()
        for((_, habit) in allHabits) {
            val score = habit.score
            with(habitsReport.habitsPerRange) {
                habitsReport.habitsPerRange = when {
                    (score < 0) -> this.copy(redRange = redRange + 1)
                    (score in 0..10) -> this.copy(orangeRange = orangeRange + 1)
                    (score in 11..40) -> this.copy(yellowRange = yellowRange + 1)
                    (score in 41..50) -> this.copy(greenRange = greenRange + 1)
                    (score > 50) -> this.copy(blueRange = blueRange + 1)
                    else -> throw Exception("WTF")
                }
            }
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
