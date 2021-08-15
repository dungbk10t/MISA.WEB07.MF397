<template>
  <button 
      v-bind:class="buttonClass" 
      v-bind:id="buttonId"
      @click="$emit('btnClick')"
  >
      <i v-if="iClass !== ''" v-bind:class="['fas' , iClass]"></i>
      <p>{{buttonText}}</p>
  </button>
</template>

<script>
export default {
  name: "Button",
  props:{
    buttonId: String,
    buttonText : String,
    iClass:{
      type: String,
      default: ''
    },
    buttonClass: String
  }
};
</script>

<style scoped>

</style>