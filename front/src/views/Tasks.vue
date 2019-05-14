<template>
  <div class="tasks">
    <h1>Tasks</h1>
    <div v-if="tasks != null">
        <TaskComponent v-for="task in tasks"
          v-bind:key="task.id"
          v-bind:id="task.id"
          v-bind:title="task.title"
          v-bind:description="task.description"
          v-bind:reminderHour="task.reminderHour"
          v-bind:reminderDays="task.reminderDays"
          v-bind:complete="task.complete"
          v-bind:userId="task.userId"
        />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import TaskComponent from '@/components/TaskComponent.vue'; // @ is an alias to /src
import TasksClient from '../clients/TasksClient';

@Component({
  components: {
      TaskComponent,
  },
})

export default class Tasks extends Vue {

  private username: string = ""
  private id: string = ""

  private tasksClient = new TasksClient()
  private tasks?: TaskVM[]

  constructor() {
      super()
      let json = window.localStorage.getItem("login")
      let user = JSON.parse(json!) as UserVM
      this.username = user.username
      this.id = user.id
      this.getTasks()
    }


  async getTasks(){
    this.tasks = [ {
      taskId: "1",
      title: "Task 1",
      description: "Cool desc",
      reminderHour: 0,
      reminderDays: 0,
      userId: "00001",
      dueDate: 0,
      complete: false
    },
    {
      taskId: "2",
      title: "Task 2",
      description: "Not so Cool desc",
      reminderHour: 2,
      reminderDays: 3,
      userId: "00001",
      dueDate: 0,
      complete: false
    }]
    /*let userTasks = await this.tasksClient.GetTasks(this.id)
    if (userTasks != null) {
      this.tasks = userTasks
    }*/
  }

}
</script>
