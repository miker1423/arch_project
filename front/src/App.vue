<template>
  <div id="app">
    <div id="nav">
      <NavbarComponent/>
      <!--<router-link to="/">Home</router-link>-->
    </div>
    <router-view/>
  </div>
</template>

<script lang="ts">
//import Vue from 'vue';
//import Component from 'vue-class-component';
import { Component, Vue } from 'vue-property-decorator';
//import Navbar from './components/Navbar.vue';
import NavbarComponent from '@/components/Navbar.vue';
//import Component from 'vue-class-component';

@Component({
  components: {
    NavbarComponent,
  },
})

export default class App extends Vue{}

</script>

<style lang="scss">
body {
  margin: unset;
}

#nav {
  display: inline-block;
  width: 100%;
}

#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

</style>