<template>
  <div class="tasks">
    <h1>Tasks</h1>
    <TaskComponent/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import TaskComponent from '@/components/TaskComponent.vue'; // @ is an alias to /src

@Component({
  components: {
      TaskComponent,
  },
})

export default class Tasks extends Vue {

}
</script>
