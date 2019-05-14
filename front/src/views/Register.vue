<template>
  <div class="register">
      <p>Username</p>
      <input class="username" placeholder="Username"/>

      <p>Email</p>
      <input class="email" placeholder="Email"/>

      <p>Password</p>
      <input type="password" class="password" placeholder="Password"/>
      <br>
        
      <div class="buttons">
        <button class="cancel-button"><router-link to="/">Cancel</router-link></button>
        <button class="register-button" v-on:click="onRegisterClick()">Register</button>
      </div>  
  </div>
</template>

<!--<script lang="ts">
import Vue from 'vue';
import AuthenticationClient from '../clients/AuthenticationClient';

type Result = {
    id: "",
    username: string,
    email: string,
    password: string
};

export default class RegisterComponent extends Vue{
  authenticationClient: AuthenticationClient = new AuthenticationClient()

  @Prop() registerUser?: boolean

  onRegisterClick(input: string){
    if (this.$props.registerUser){
      let promise = this.authenticationClient.loginUser(input)
        .then(results => {
        this.addToResults(results, 3)
      });
    }

    console.log("click");
  }
}
</script>-->

<style scoped lang="scss">
  div.register {
    text-align: center;
    display: inline-block;
  }

  div.register p { 
    margin-bottom: 5px;
    text-align: left;
    color: #163a61;
    font-size: 20px;
  }

  div.register input {
    border: none;
    width: 200px;
    border-bottom: 1px solid #163a61;
  }

  div.register div.buttons {
    margin-top: 20px;
  }

  div.register div.buttons button {
    background: #d60a67;
    cursor: pointer;
    color: white;
    font-family: sans-serif;
    border: 1px solid #fff;
    border-radius: 3px;
    font-size: 15px;
    width: 50%;
    height: 25px;
  }

   div.register div.buttons button a {
    color: white;
    text-decoration: none;
  }
</style>
