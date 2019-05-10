export default class TaskClient {

    async GetTasks(userId: string){
        var res = await fetch(`api/tasks/${userId}`, {
            method: "GET"
        });

        if (res.ok) {
            var json = await res.json();
            return json as TaskVM;
        }

        return null;
    }

    async CreateTask(newTask: TaskVM) {
        var res = await fetch(`api/tasks/${newTask.userId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newTask)
        });

        if (res.ok) {
            var json = await res.json();
            return json as TaskVM;
        }

        return null;
    }

    async UpdateTask(task: TaskVM) {
        var res = await fetch(`api/tasks`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(task)
        });

        if (res.ok) {
            var json = await res.json();
            return json as TaskVM;
        }

        return null;
    }

    async DeleteTask(id: string, userId: string) {
        var res = await fetch(`api/tasks/${userId}/${id}`, {
            method: "DELETE"
        });

        if (res.ok) {
            var json = res.json();
            return json;
        }

        return null
    }  

}