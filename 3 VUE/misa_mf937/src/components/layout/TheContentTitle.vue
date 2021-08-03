<template>
  <div class="content-title">
    <b>Danh sách nhân viên</b>
    <span>
      <Button
        buttonId="btn-delete"
        buttonClass="button button-icon"
        iClass="fa-trash-alt"
        buttonText="Xóa nhân viên"
      />
      <Button
        buttonId="btn-add"
        buttonClass="button button-icon"
        iClass="fa-user-plus"
        buttonText="Thêm nhân viên"
      />
    </span>
  </div>
</template>

<script>
import Button from "../base/BaseButton.vue";

export default {
  name: "TheContentTitle",
  components: {
    Button,
  },
};
</script>

