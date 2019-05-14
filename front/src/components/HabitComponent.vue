<template>
    <div>
        <h1>{{ title }}</h1>
        <p>Cool description</p>
    </div> 
</template>


<script lang="ts">
import Vue from 'vue';
import { Prop, Component } from 'vue-property-decorator';

@Component
export default class HabitComponent extends Vue {

    @Prop() id!: string;
    @Prop() title!: string
    @Prop() description!: string
    @Prop() difficulty!: number
    @Prop() score!: number
    @Prop() type!: number
    @Prop() userId!: string

    mounted() {
        console.log(this.$props.id)
    }
}
</script>
