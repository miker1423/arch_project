<template>
  <div class="login">
    <p>Username</p>
    <input class="username" id="username" placeholder="Username" v-model="username"/>

    <p>Password</p>
    <input type="password" class="password" placeholder="Password" v-model="password"/>
    <br>

    <div class="buttons">
      <button class="cancel-button"><router-link to="/">Cancel</router-link></button>
      <button class="login-button" @click="onLoginClick">Login</button>
    </div>  
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import AuthenticationClient from '../clients/AuthenticationClient';
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class LoginComponent extends Vue{

  constructor() {
    super()
    this.onLoginClick = this.onLoginClick.bind(this);
  }

  private username: string = ""
  private password: string = ""
  private authenticationClient: AuthenticationClient = new AuthenticationClient()
  

  async onLoginClick(){    
    console.log("Hi")
    let user: UserVM = {
      id: "",
      email: "",
      username: this.username,
      password: this.password,
    }

    let authUser = await this.authenticationClient.loginUser(user)
  }
}
</script>



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
    font-family: sans-serif;
  }

  div.login input {
    border: none;
    width: 200px;
    border-bottom: 1px solid #163a61;
    font-family: sans-serif;
  }

  div.login div.buttons {
    margin-top: 20px;
  }

  div.login div.buttons button {
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

  div.login div.buttons button a {
    color: white;
    text-decoration: none;
  }
</style>
