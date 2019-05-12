<template>
  <div class="login">
    <p>Username</p>
    <input class="username" placeholder="Username"></input>

    <p>Password</p>
    <input type="password" class="password" placeholder="Password"></input>
    <br>

    <div class="buttons">
      <button class="cancel-button"><router-link to="/">Cancel</router-link></button>
      <button class="login-button"><router-link to="/maindashboard">Login</router-link></button>
    </div>  
  </div>
</template>

<style scoped lang="scss">
  div.login {
    text-align: center;
    display: inline-block;
  }

  div.login p { 
    margin-bottom: 5px;
    text-align: left;
    color: #163a61;
    font-size: 20px;
  }

  div.login input {
    border: none;
    width: 200px;
    border-bottom: 1px solid #163a61;
  }

  div.login div.buttons {
    margin-top: 20px;
  }

  div.login div.buttons button {
    background: #d60a67;
    color: white;
    font-family: sans-serif;
    border: 1px solid #fff;
    border-radius: 3px;
    font-size: 15px;
    width: 50%;
    height: 25px;
  }

  div.login div.buttons button a {
    color: white;
    text-decoration: none;
  }
</style>
