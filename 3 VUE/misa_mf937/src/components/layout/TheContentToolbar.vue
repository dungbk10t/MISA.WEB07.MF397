<template>
  <div>
    <div class="content-toolbar">
      <div class="toolbar-left">
        <Input
          inputClass="input-search input-icon"
          inputType="text"
          inputPlacehoder="Tìm kiếm theo mã, tên hoặc số điện thoại"
        />
      </div>
      <div class="toolbar-right">
        <Button
          buttonId="btnRefesh"
          buttonClass="button button-refresh"
          iClass="fa-sync-alt"
          buttonText=""
        />
      </div>
    </div>
  </div>
  
</template>

<script>
import Input from "../base/BaseInput.vue";
import Button from "../base/BaseButton.vue"

export default {
  name: "TheContentToolbar",
  components: {
    Input, Button
  },
};
</script>

