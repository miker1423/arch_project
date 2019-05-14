export default class HabitClient {

    async GetHabits(userId: string) {
        var res = await fetch(`api/habits/${userId}`, {
            method: "GET",
        });

        if (res.ok) {
            var json = await res.json();
            return json as HabitVM[];
        }

        console.log(`ERROR: ${res}`);
        return null;
    }

    async CreateHabit(habit: HabitVM){
        var newHabit: HabitVM = {
            userId: habit.userId,
            difficulty: habit.difficulty,
            type: habit.type,
            score: habit.score,
            description: habit.description,
            title: habit.title,
            id: "00000000-0000-0000-0000-000000000000"
        }

        var res = await fetch(`api/habits`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newHabit)
        });

        if (res.ok) {
            var json = await res.json();
            return json as HabitVM;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }

    async UpdateHabit(habit: HabitVM){
        var res = await fetch(`api/habits/`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(habit)
        });

        if (res.ok) {
            var json = await res.json();
            return json as HabitVM;
        }

        return null;
    }

    async DeleteHabit(id: string) {
        var res = await fetch(`api/habits/${id}`, {
            method: "DELETE",
        });

        if (res.ok) {
            var json = await res.json();
            return json as HabitVM;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }

    async UpdateScore(habit: HabitVM, modifier: boolean) {
        var action = modifier ? "add" : "substract";

        var res = await fetch(`api/habits/score/${action}/${habit.id}`, {
            method: "PUT" 
        });

        if (res.ok) {
            var json = await res.json();
            return json as HabitVM;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }

}