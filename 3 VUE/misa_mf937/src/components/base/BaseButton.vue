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
button {
  cursor: pointer;
  outline: none;
}

.button {
  height: 40px;
  min-width: 100px;
  background-color: #019160;
  border-radius: 4px;
  font-size: 13px;
  color: #ffffff;
  display: flex;
  padding-left: 24px;
  padding-right: 24px;
  align-items: center;
  border: 1px solid #019160;
  outline: none;
}

.button:hover {
  background-color: #2fbe8e;
}

.button.button-icon {
  padding-left: 16px;
  padding-right: 16px;
  min-width: 50px;
}

.button.button-icon i {
  margin-right: 8px;
}

.button.button-refresh {
  border: 1px solid #bbbbbb;
  background-color: #ffffff;
  width: 45px;
  min-width: 45px;
  color: #000000;
  padding: 0;
}

.button.button-refresh:hover {
  background-color: #e9ebee;
  border: none;
}

.button.button-refresh i {
  margin: auto 16px;
}
</style>