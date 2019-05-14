<template>
  <div class="main-dashboard">
    <h2>Welcome, username</h2>

    <button class="home-button"><router-link to="/">Home</router-link></button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Prop } from 'vue-property-decorator';

export default class MainDashboard extends Vue {

  // @Prop() username: string
  
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