<template>
  <div class="reports">
    <h2>REPORTS!!!</h2>

    <button class="home-button"><router-link to="/">Home</router-link></button>
  </div>
</template>

<style scoped lang="scss">
div.reports h2 {
    font-family: sans-serif;
    font-size: 45px;
}

div.reports button {
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

div.reports button a {
    color: white;
    text-decoration: none;
}
</style>