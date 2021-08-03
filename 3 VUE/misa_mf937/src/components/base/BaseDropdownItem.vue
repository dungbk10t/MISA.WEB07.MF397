<template>
  <div 
    v-bind:class="['dropdown-list-item',dropdownItemList]"
    v-bind:valueid="dropdownItemValueId"
    v-bind:valuename="dropdownItemValueName"
  >
    <i class="fas fa-check"></i>
    <div class="dd-item-text">{{dropdownItemValueName}}</div>
  </div>
</template>

<script>
export default {
    name: "BaseDropdownItem",
    props:{
    dropdownItemList: String,
    dropdownItemValueId: String,
    dropdownItemValueName: String
  }
};
</script>

<style scoped>
@import url("../../css/base/dropdown.css");
</style>