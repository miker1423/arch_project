import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Tasks from './views/Tasks.vue';
import Habits from './views/Habits.vue';
import Login from './views/Login.vue';
import Register from './views/Register.vue';
import MainDashboard from './views/MainDashboard.vue';
import Reports from './views/Reports.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
    {
      path: '/register',
      name: 'register',
      component: Register,
    },
    {
      path: '/maindashboard',
      name: 'maindashboard',
      component: MainDashboard,
    },
    {
      path: '/reports',
      name: 'reports',
      component: Reports,
    },
    {
      path: '/tasks',
      name: 'tasks',
      component: Tasks,
    },
    {
      path: '/habits',
      name: 'habits',
      component: Habits,
    },
  ],
});