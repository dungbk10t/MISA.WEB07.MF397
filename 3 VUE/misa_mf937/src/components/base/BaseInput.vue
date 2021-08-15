<template>
  <div class="inputdiv">
    <label v-if="labelText"></label>
    <input
      v-bind:id="inputId"
      v-bind:class="[inputClass,{'hasError': isBorderRed}]"
      v-bind:type="inputType"
      v-bind:placeholder="inputPlacehoder"
      v-bind:value="inputValue"
      @input="[$emit('input', $event.target.value) , $emit('formatSalary')]"
      @blur="$emit('input-blur')"
    />
  </div>
</template>

<script>

export default {
  name: "BaseInput",
  props: {
    labelText: String,
    inputClass: String,
    inputType: String,
    inputPlacehoder: String,
    inputValue: String,
    inputId: String,
    isBorderRed: Boolean,
    inputRequied: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style scoped>
input {
  width: 325px;
  height: 40px;
  padding: 10px 16px;
  border-radius: 4px;
  outline: none;
  border: solid 1px #bbbbbb;
  box-sizing: border-box;
  font-family: "GoogleSans-Regular";
  font-size: 13px;
}

input:focus {
  border: 1px solid #019160;
}

input.input-search {
  font-family: GoogleSans-Regular;
}
input.hasError{
  border-color: #FF4747;
}
select.chondi {
  width: 325px;
  height: 40px;
  padding: 10px 16px;
  border-radius: 4px;
  outline: none;
  border: solid 1px #bbbbbb;
  box-sizing: border-box;
}

input.input-icon {
  background-repeat: no-repeat;
  background-position: 16px center;
  background-size: 16px;
  padding-left: 40px;
}
</style>