<template>
  <div class="popup" v-bind:type="popupType">
    <div class="popup-header">
      <p>{{ popupTitle }}</p>
      <button class="btn-exit" @click="hidePopup">
        <i class="fas fa-times"></i>
      </button>
    </div>
    <div class="popup-content">
      <div v-if="popupType !== 'notification'" class="popup-content-left" >
        <i v-if="popupType == 'danger'" class="fas fa-exclamation-triangle danger"></i>
        <i v-if="popupType == 'warning'" class="fas fa-exclamation-triangle warning"></i>
      </div>
      <div v-if="popupType == 'notification'" class="popup-content-right long">
        <p>
          {{ popupContent }}
        </p>
      </div>
      <div v-else class="popup-content-right short">
        <p>
          {{ popupContent }}
        </p>
      </div>
    </div>
    <div class="popup-footer">
      <Button v-if="popupType !== 'notification'" buttonClass="button btn-y" buttonText="NO" @btnClick="hidePopup"/>
      <Button v-if="popupType !== 'danger'" buttonClass="button btn-x" buttonText="YES" @btnClick="btnXOnClick" />
      <Button v-if="popupType == 'danger'" buttonClass="button btn-z" buttonText="YES" @btnClick="btnZOnClick" />
    </div>
    
  </div>
</template>



<script>
import Button from "./BaseButton.vue";
import { eventBus2 } from "../../main.js";


export default {
  name: "BasePopup",
  props: {
    popupType: String,
    popupTitle: String,
    popupContent: String,
  },
  components: {
    Button,
  },
  methods:{
    btnZOnClick(){
      eventBus2.$emit('deleteData');
      this.hidePopup();
    },
    hidePopup(){
      this.$emit('hidePopup1');
    },
    btnXOnClick(){
      eventBus2.$emit('addOrUpdateData');
      this.hidePopup();
    }
  }
};
</script>

<style scoped>
.popup {
  height: 200px;
  width: 450px;
  border-radius: 4px;
  overflow: hidden;
  padding: 0;
  box-shadow: 0px 3px 6px 0px rgba(0, 0, 0, 0.16);
  position: fixed;
  left: 50%;
  margin-left: -225px;
  top: 300px;
  z-index: 1001;
  background-color: #fff;
  display: none;
}

.popup-show{
  display: block;
}

.popup .popup-header {
  width: 100%;
  height: 50px;
  display: flex;
  justify-content: space-between;
}

.popup .popup-header p {
  display: inline-block;
  line-height: 26px;
  width: 370px;
  margin-left: 24px;
  margin-top: 24px;
  margin-bottom: 0px;
  font-weight: bold;
  font-size: 16px;
}

.popup .popup-header button {
  width: 30px;
  height: 30px;
  outline: none;
  border: none;
  border-bottom-left-radius: 4px;
  background-color: #fff;
  color: #000;
}

.popup .popup-header button:hover {
  background-color: #e5e5e5;
}

.popup .popup-content {
  width: 100%;
  height: calc(100% - 110px);
  box-sizing: border-box;
  display: flex;
  align-items: center;
}

.popup .popup-content .popup-content-left {
  width: 32px;
  margin-left: 24px;
  background-color: #f4f4f4;
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 50%;
}

.popup .popup-content .popup-content-left i.warning {
  color: #F1C04E;
}

.popup .popup-content .popup-content-left i.danger {
  color: #EA2B2B;
}

.popup .popup-content .popup-content-left p {
  line-height: 20px;
}

.popup .popup-content .popup-content-right {
  width: 370px;
  height: 100%;
  padding: 24px 0px 24px 10px;
  box-sizing: border-box;
}

.popup .popup-content .popup-content-right.long{
  width: 423px;
  padding-left: 24px;
}

.popup .popup-content .popup-content-right.short{
  width: 370px;
  padding-left: 10px;
}

.popup .popup-content .popup-content-right p {
  line-height: 20px;
  margin: 0;
}

.popup .popup-footer {
  width: 100%;
  height: 60px;
  background-color: #f4f4f4;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding-right: 24px;
  box-sizing: border-box;
}

.popup .popup-footer .btn-x {
  min-width: 100px;
  justify-content: center;
}

.popup .popup-footer .btn-y {
  background-color: #f4f4f4;
  color: #000;
  padding-left: 16px;
  padding-right: 16px;
  box-sizing: border-box;
  margin-right: 16px;
  border: none;
  justify-content: center;
}


.popup .popup-footer .btn-y:hover {
  background-color: #e5e5e5;
}

.popup .popup-footer .btn-z {
  min-width: 100px;
  justify-content: center;
  background-color: #f65454;
  border: none;
}

.popup .popup-footer .btn-z:hover {
  background-color: #a71a1a;
}
</style>