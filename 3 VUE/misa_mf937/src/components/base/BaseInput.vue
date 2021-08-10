<template>
  <div class="inputdiv">
    <label v-if="labelText"></label>
    <input
      v-bind:id="inputId"
      v-bind:class="inputClass"
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
    // dataType: String,
    inputValue: String,
    inputId: String,
    inputRequied: {
      type: Boolean,
      default: false,
    },
  },

// data() {
//     return {
//       isInvalid: false,
//     };
//   },
//   methods: {
//     onBlurDoValidate(inputValue) {
//       let vm = this;
//       console.log("currentInputValue", inputValue);
//       if( !inputValue){
//         vm.isInvalid= true;
//       }else{
//         vm.isInvalid= false;
//       }
//       eventBus2.$emit('invalidInput');
//       //Goi ham validate
//       // eventBus2.$emit("validateData", [inputValue, vm.dataType]);

//       // Hiển thị hoặc ẩn thông báo nếu có hoa không có lỗi
//       // vm.isInvalid = true;
//     },
//     created() {
//       let vm= this;
//       eventBus2.$on('validateFormInput' ,()=>{
//         vm.onBlurDoValidate();
//         console.log("got command")  
//       })
//     },
//   },
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
  font-family: GoogleSans-Italic;
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