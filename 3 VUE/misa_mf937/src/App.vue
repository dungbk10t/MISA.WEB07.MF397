<template>
  <div id="app">
    <TheMenu />
    <EmployeeList />
    <ToastMessage
      :tooltipText="tooltipText"
      :tooltipType="tooltipType"
      :tooltipScaleClass="tooltipScaleClass"
      @closeToolTip="closeToolTip"
    />
    <Popup
      :class="{ 'popup-show': isShowPopup }"
      :popupTitle="popupTitle"
      :popupContent="popupContent"
      :popupType="popupType"
      @hidePopup1="hidePopup"
    />
  </div>
</template>

<script>
import { eventBus1 } from "./main.js";
import { eventBus2 } from "./main.js";
import TheMenu from "@/components/layout/TheMenu";
import EmployeeList from "@/components/view/employee/EmployeeList";
import Popup from "./components/base/BasePopup.vue";
import ToastMessage from "./components/base/BaseToastMessage.vue";

export default {
  name: "App",
  components: {
    TheMenu,
    EmployeeList,
    ToastMessage,
    Popup,
  },
  created() {
    eventBus1.$on("showTooltipDeleteSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã xóa thành công !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus1.$on("showTooltipAddSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã thêm mới thành công !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus1.$on("showTooltipUpdateSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã cập nhật thành công !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus2.$on("showTooltipInputRequied", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Thông tin này bắt buộc nhập !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus2.$on("showPopupConfirmAdd", () => {
      this.isShowPopup = true;
      this.popupContent = "Bạn có muốn thêm nhân viên này vào danh sách không?";
      this.popupType = "warning";
      this.popupTitle = "Thêm mới bản ghi";
    });
    eventBus2.$on("showPopupConfirmUpdate", () => {
      this.isShowPopup = true;
      this.popupContent = "Bạn có muốn cập nhật thông tin nhân viên này không?";
      this.popupType = "warning";
      this.popupTitle = "Cập nhật bản ghi";
    });
    eventBus2.$on("showTooltipInputRequiedAll", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Chưa nhập đủ các trường bắt buộc !";
      this.tooltipScaleClass = "scale1";
    });
  },
  data() {
    return {
      tooltipType: "",
      tooltipText: "",
      tooltipScaleClass: "scale0",
      isCollapseMenu: false,
      isShowAddDialog: false,
      forMode: 1,
      employeeId: "",
      reOpen: false,
      isShowPopup: false,
      popupContent: "",
      popupType: "",
      popupTitle: "",
    };
  },
  methods: {
    showAddForm() {
      this.isShowAddDialog = true;
      this.forMode = 1;
      this.employeeId = "";
      this.reOpen = !this.reOpen;
    },
    hideForm() {
      this.isShowAddDialog = false;
    },
    showEditForm1(id) {
      this.isShowAddDialog = true;
      this.forMode = 0;
      this.employeeId = id;
      this.reOpen = !this.reOpen;
    },
    showDeletePopup() {
      this.isShowPopup = true;
      this.popupContent =
        "Bạn có muốn xóa các bản ghi này không? Bạn có muốn xóa các bản ghi này không? Bạn có muốn xóa các bản ghi này không?";
      this.popupType = "danger";
      this.popupTitle = "Xóa các bản ghi";
    },
    hidePopup() {
      this.isShowPopup = false;
    },
    closeToolTip() {
      this.tooltipScaleClass = "scale0";
    },
    btnToggleOnClick() {
      this.isCollapseMenu = !this.isCollapseMenu;
      eventBus1.$emit("collapseMenu", this.isCollapseMenu);
    },
  },
};
</script>

<style>
@import url("./assets/font/fontawesome-5.15.1/css/all.css");
@import url("./css/common/main.css");
</style>
