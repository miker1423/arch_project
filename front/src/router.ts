import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Tasks from './views/Tasks.vue';
import Habits from './views/Habits.vue';

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
      path: '/about',
      name: 'about',
      component: () => import( './views/About.vue'),
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
