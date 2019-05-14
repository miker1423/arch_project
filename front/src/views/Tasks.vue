<template>
  <div class="tasks">
    <h1>Tasks</h1>
    <TasksWrapper/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import TasksWrapper from '@/components/TasksWrapper.vue'; // @ is an alias to /src

@Component({
  components: {
      TasksWrapper,
  },
})

export default class Tasks extends Vue {

}
</script>
