<template>
  <div class="tasks">
    <h1>Habits</h1>
    <HabitsWrapper/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HabitsWrapper from '@/components/HabitsWrapper.vue'; // @ is an alias to /src

@Component({
  components: {
      HabitsWrapper,
  },
})

export default class Habits extends Vue {

}
</script>
