<template>
  <div id="app">
    <div id="nav">
      <navbar/>
      <!--<router-link to="/">Home</router-link>-->
    </div>
    <router-view/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import navbar from '@/components/nav-menu/navbar.vue'; // @ is an alias to /src
@Component({
  components: {
    navbar,
  },
})
export default class App extends Vue {}
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