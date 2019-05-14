<template>
  <router-view/>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

export default class App extends Vue{ }

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