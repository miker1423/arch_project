<template>
  <div>
    <div class="main-dashboard">
      <h2>Welcome, {{ username }}</h2>
      <button class="home-button"><router-link to="/">Home</router-link></button>
      <UserReports v-if="reports != null" title="Report 1" :userReports="reports"/>
  </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Prop, Component, Watch } from 'vue-property-decorator';
import TasksClient from '../clients/TasksClient';
import HabitClient from '../clients/HabitClient';
import ReportClient from '../clients/ReportClient';
import UserReports from '../components/UserReports.vue';

@Component({
  components:{
    UserReports
  }
})
export default class MainDashboard extends Vue {

    private username: string = ""
    private id: string = ""
    
    private tasksClient = new TasksClient()
    private tasks: TaskVM[] = []

    private habitClient = new HabitClient()
    private habits: HabitVM[] = []

    private reportClient = new ReportClient()
    private reports?: UserReportVM

    constructor() {
      super()
      let json = window.localStorage.getItem("login")
      let user = JSON.parse(json!) as UserVM
      this.username = user.username
      this.id = user.id
      this.getHabits()
      this.getTasks()
      this.getUserReport()
    }

    async getTasks(){
      /*let userTasks = await this.tasksClient.GetTasks(this.id)
      if (userTasks != null) {
        this.tasks = userTasks
      }*/
    }

    async getHabits(){
      /*let userHabits = await this.habitClient.GetHabits(this.id)
      if (userHabits != null) {
        this.habits = userHabits
      }*/
    }

    async getUserReport(){
      /*let userReport = await this.reportClient.GetReport(this.id)*/
      let map: Map<string, string> = new Map()
      this.reports = {
          id: "",
          todayTasks: map,
          delayedTask: map,
          goodHabits: map,
          badHabits: map
      }
      /*if (userReport != null) {
        this.reports = userReport
      }*/
    }
}
</script>


<style scoped lang="scss">
div.main-dashboard h2 {
    font-family: sans-serif;
    font-size: 45px;
}

div.main-dashboard button {
    background: #d60a67;
    color: white;
    font-family: sans-serif;
    border: 1px solid #fff;
    border-radius: 3px;
    font-size: 20px;
    width: 25%;
    height: 35px;
    margin-right: 10px;
}

div.main-dashboard button a {
    color: white;
    text-decoration: none;
}
</style>