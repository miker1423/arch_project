export default class TaskClient {

    async GetTasks(userId: string){
        // Receives Array of Tasks
        var res = await fetch(`api/tasks/${userId}`, {
            method: "GET"
        });

        if (res.ok) {
            var json = await res.json();
            return json as TaskVM[];
        }
        
        console.log(`ERROR: ${res}`);
        return null;
    }

    async CreateTask(newTask: TaskVM) {
        // Do not send Task ID
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

        console.log(`ERROR: ${res}`);
        return null;
    }

    async UpdateTask(task: TaskVM) {
        // Do not send Task ID and User ID
        var res = await fetch(`api/tasks/${task.userId}/${task.taskId}`, {
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

        console.log(`ERROR: ${res}`);
        return null;
    }

    async DeleteTask(id: string, userId: string) {

        var res = await fetch(`api/tasks/${userId}/${id}`, {
            method: "DELETE"
        });

        if (res.ok) {
            var json = await res.json();
            return json as TaskVM;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }  

}