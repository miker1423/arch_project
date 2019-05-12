<template>
    <div class="card">
        <h3>Task 1</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam sit amet risus dolor.</p>
    </div> 
</template>

<script lang="ts">
import Vue from 'vue';

export default class Card extends Vue {

}
</script>


<style lang="scss" scoped>
.card {
    width: 300px;
    height: 300px;
    border: 1px solid lightgray;
}
</style>