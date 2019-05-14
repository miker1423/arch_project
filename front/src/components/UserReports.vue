<template>
    <div>
        <h1>{{ title }}</h1>
    </div> 
</template>


<script lang="ts">
import Vue from 'vue';
import { Prop, Component } from 'vue-property-decorator';


@Component
export default class UserReports extends Vue {

    @Prop() userReports!: UserReportVM
    @Prop() title!: string

    mounted(){
        console.log(this.$props.userReports)
    }
}
</script>
