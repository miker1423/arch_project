<template>
    <div class="task">
        <Card/>
    </div> 
</template>


<script lang="ts">
import Vue from 'vue';
import Card from '@/components/Card.vue';
import Component from 'vue-class-component';


@Component({
    components: {
        Card
    }
})

export default class TasksWrapper extends Vue {

}
</script>
