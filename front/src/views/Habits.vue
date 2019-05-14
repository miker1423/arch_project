<template>
  <div class="habits">
    <h1>Habits</h1>
    <div v-if="habits != null">
        <HabitComponent v-for="habit in habits"
          v-bind:key="habit.id"
          v-bind:id="habit.id"
          v-bind:title="habit.title"
          v-bind:description="habit.description"
          v-bind:difficulty="habit.difficulty"
          v-bind:score="habit.score"
          v-bind:type="habit.type"
          v-bind:userId="habit.userId"
        />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import HabitComponent from '@/components/HabitComponent.vue'; // @ is an alias to /src
import HabitClient from '../clients/HabitClient';

@Component({
  components: {
      HabitComponent,
  },
})

export default class Habits extends Vue {

  private username: string = ""
  private id: string = ""

  private habitClient = new HabitClient()
  private habits?: HabitVM[]

  constructor() {
      super()
      let json = window.localStorage.getItem("login")
      let user = JSON.parse(json!) as UserVM
      this.username = user.username
      this.id = user.id
      this.getHabits()
    }


  async getHabits(){
    this.habits = [ {
      id: "1",
      title: "Habit 1",
      description: "Cool habit",
      userId: "00001",
      difficulty: 1,
      score: 20,
      type: 0
    },
    {
      id: "2",
      title: "Habit 2",
      description: "Bad habit",
      userId: "00001",
      difficulty: 4,
      score: 40,
      type: 1
    }]
    /*let userHabits = await this.habitClient.GetHabits(this.id)
    if (userHabits != null) {
      this.habits = userHabits
    }*/
  }

}
</script>
