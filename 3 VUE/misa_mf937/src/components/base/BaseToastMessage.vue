<template>
  <div class="messenger-wrapper">
    <div class="toast-messenger" scale="0" v-bind:type="tooltipType">
      <i v-bind:class="['fas', tooltipIClass]"></i>
      <p>{{ tooltipText }}</p>
      <button><i class="fas fa-times"></i></button>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseToolTip",
  props: {
    tooltipText: String,
    tooltipIClass: String,
    tooltipType: String,
  },
};
</script>

<style scoped>
.messenger-wrapper {
  position: fixed;
  top: 4px;
  left: 50%;
  margin-left: -165px;
}

.toast-messenger {
  width: 330px;
  height: 48px;
  display: flex;
  align-items: center;
  padding-left: 24px;
  position: relative;
  border-radius: 4px;
  box-sizing: border-box;
  transform: scale(0);
  transition: 0.5s;
  box-shadow: 0px 3px 6px 0px rgba(0, 0, 0, 0.16);
  z-index: 1000;
  background-color: #ffffff;
  opacity: 1;
}

.toast-messenger p {
  line-height: 48px;
  margin-left: 10px;
}

.toast-messenger i {
  font-size: 24px;
}

.toast-messenger button {
  position: absolute;
  right: 10px;
  border: none;
  outline: none;
  background-color: #fff;
  border-radius: 50px;
}

.toast-messenger button:hover {
  background-color: #f1f1f1;
}

.toast-messenger.danger i {
  color: #eb5757;
}

.toast-messenger.success i {
  color: #01b075;
}

.toast-messenger.warning i {
  color: #f1c04e;
}

.toast-messenger.primary i {
  color: #eb5757;
}

.scale1 {
  transform: scale(1);
}
</style>