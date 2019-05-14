<template>
    <div class="topnav">
        <div class="sidebar" style="display:none" id="sidebar">
            <button id="sidemenu-options"><router-link to="/maindashboard">Dashboard</router-link></button>
            <button id="sidemenu-options"><router-link to="/tasks">Tasks</router-link></button>
            <button id="sidemenu-options"><router-link to="/habits">Habits</router-link></button>
            <button id="sidemenu-options"><router-link to="/reports">Reports</router-link></button>
            <button id="sidemenu-options"><router-link to="/">Logout</router-link></button>
        </div>
        <div id="main-navbar">
            <button class="active" id="sidebar-button">Home</button>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-property-decorator';

export default class Navbar extends Vue{}

/*export default class NavbarComponent extends Vue{
    openSidebar(){       
        if(!document.getElementById("sidebar")!.classList.contains('open')){
            document.getElementById("sidebar")!.classList.toggle('open');
            document.getElementById("sidebar")!.style.display = "block";
            document.getElementById("main-navbar")!.classList.add('open');
        } else if(document.getElementById("sidebar")!.classList.contains('open')) {
            document.getElementById("sidebar")!.classList.toggle('open');
            document.getElementById("main-navbar")!.classList.remove("open");
            document.getElementById("sidebar")!.style.display = "none";     
        }    
    }
}*/

</script>

<style scoped lang="scss">
div.topnav div#main-navbar {   
  overflow: hidden;
  background-color: #2c3e50;
}

div.topnav div#sidebar.open {
    width: 19.9%;
    float: left;
    background: #2c3e50;
}

div.topnav div#sidebar button#sidemenu-options {
    width: 100%;
    background: none;
    border: none;
    cursor: pointer;
    text-align: left;
    padding: 10px;
}

div.topnav div#sidebar button#sidemenu-options:hover {
    background-color: #d60a67;
}

div.topnav div#sidebar button#sidemenu-options a{
    text-decoration: none;
    color: white;
    font-size: 16px;
}

div.topnav div#main-navbar.open {
    width: 80%;
    float: right;
}

div.topnav div#main-navbar button {
  float: left;
  display: block;
  background-color: #2c3e50;
  border: none;
  outline: none;
  cursor: pointer;
  color: #f2f2f2;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;
}

div.topnav div#main-navbar button:hover {
  color: #d60a67;
}
</style>