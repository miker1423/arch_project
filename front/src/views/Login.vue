<template>
  <div class="login">
    <p>Username</p>
    <input class="username" id="username" placeholder="Username"/>

    <p>Password</p>
    <input type="password" class="password" placeholder="Password"/>
    <br>

    <div class="buttons">
      <button class="cancel-button"><router-link to="/">Cancel</router-link></button>
      <button class="login-button" >Login</button>
    </div>  
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import AuthenticationClient from '../clients/AuthenticationClient';

type Result = {
    id: "",
    username: string,
    password: string
};

export default class LoginComponent extends Vue{
  authenticationClient: AuthenticationClient = new AuthenticationClient()

  //@Prop() loginUsers?: boolean

  onLoginClick(){
    
    /*if (this.$props.loginUsers){
      let promise = this.authenticationClient.loginUser(input)
        .then(results => {
        this.addToResults(results, 3)
      });
    }*/

    console.log("click");
  }
}
</script>

<!--<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Vue from 'vue';
import AuthenticationClient from '../clients/AuthenticationClient';

export default class LoginComponent extends Vue{
    authenticationClient: AuthenticationClient = new AuthenticationClient()
    
    /*@Prop() username?: string
    @Prop() password?: string*/

    login(){  
      let user = (<HTMLInputElement> document.getElementById("username")).value;
      console.log("user: " + user);
        // llamar al cliente
        // info lista   
    }
}

</script>-->

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
