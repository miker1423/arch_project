<template>
  <div class="home">
    <h2>Welcome to Task Manager System</h2>
    <button class="login"><router-link to="/login">Login</router-link></button>
    <button class="register"><router-link to="/register">Register</router-link></button>
  </div>
</template>

<style scoped lang="scss">
div.home h2 {
    font-family: sans-serif;
    font-size: 45px;
}

div.home button {
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

div.home button a {
    color: white;
    text-decoration: none;
}

</style>
