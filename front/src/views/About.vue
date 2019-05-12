<template>
  <div class="about">
    <p>Username</p>
    <input class="username" placeholder="Username"></input>

    <p>Password</p>
    <input class="password" placeholder="Password"></input>
    <br>

    <div class="buttons">
      <button class="cancel"><router-link to="/">Cancel</router-link></button>
      <button class="login">Login</button>
    </div>  
  </div>
</template>

<style scoped lang="scss">
  div.about {
    text-align: center;
    display: inline-block;
  }

  div.about p { 
    margin-bottom: 5px;
    text-align: left;
    color: #163a61;
    font-size: 20px;
  }

  div.about input {
    border: none;
    width: 200px;
    border-bottom: 1px solid #163a61;
  }

  div.about div.buttons {
    margin-top: 20px;
  }

  div.about div.buttons button {
    background: #d60a67;
    color: white;
    font-family: sans-serif;
    border: 1px solid #fff;
    border-radius: 3px;
    font-size: 15px;
    width: 50%;
    height: 25px;
  }

  div.about div.buttons button a {
    color: white;
    text-decoration: none;
  }
</style>
