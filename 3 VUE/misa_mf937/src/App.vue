<template>
  <div id="app">
    <TheMenu
      :isCollapseMenu="isCollapseMenu"
      @btnToggleOnClick="btnToggleOnClick"
    />
    <EmployeeList
      :isCollapseMenu="isCollapseMenu"
      @btnDeleteOnClick="showDeletePopup"
    />
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
  // CÁC COMPONET ĐƯỢC SỬ DỤNG 
  components: {
    TheMenu,
    EmployeeList,
    ToastMessage,
    Popup,
  },
  // CÁC HÀM KHỞI TẠO CREATED
  created() {
    /**
     * Eventbus hiển thị toast message ở khi xóa nhân viên thành công (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus1.$on("showTooltipDeleteSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã xóa thành công !";
      this.tooltipScaleClass = "scale1";
    });
    /** 
     * Eventbus hiển thị toast message ở khi thêm nhân viên thành công (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus1.$on("showTooltipAddSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã thêm mới thành công !";
      this.tooltipScaleClass = "scale1";
    });
    /**
     * Eventbus hiển thị toast message ở khi cập nhật thông tin nhân viên thành công (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus1.$on("showTooltipUpdateSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã cập nhật thành công !";
      this.tooltipScaleClass = "scale1";
    });
    /**
     * Eventbus hiển thị toast message cảnh báo ở khi dữ liệu ở ô input để trống (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus2.$on("showTooltipInputRequied", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Thông tin này bắt buộc nhập !";
      this.tooltipScaleClass = "scale1";
    });
    /**
     * Eventbus hiển thị toast message cảnh báo khi dữ liệu ở ô input validate sai (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus2.$on("showTooltipInputRequied2", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Thông tin nhập không hợp lệ !";
      this.tooltipScaleClass = "scale1";
    });
    /**
     * Popup thông báo để xác nhận thêm thông tin nhân viên (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus2.$on("showPopupConfirmAdd", () => {
      this.isShowPopup = true;
      this.popupContent = "Bạn có muốn thêm nhân viên này vào danh sách không?";
      this.popupType = "warning";
      this.popupTitle = "Thêm mới bản ghi";
    });
    /**
     * Popup thông báo để xác nhận cập nhật  thông tin nhân viên (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus2.$on("showPopupConfirmUpdate", () => {
      this.isShowPopup = true;
      this.popupContent = "Bạn có muốn cập nhật thông tin nhân viên này không?";
      this.popupType = "warning";
      this.popupTitle = "Cập nhật bản ghi";
    });
    /**
     * Popup thông báo để xác nhận cập nhật  thông tin nhân viên (Phía lắng nghe)
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    eventBus2.$on("showTooltipInputRequiedAll", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Chưa nhập đủ các trường bắt buộc !";
      this.tooltipScaleClass = "scale1";
    });
  },
  // DỮ LIỆU KHỞI TẠO 
  data() {
    return {
      isCollapseMenu: false,
      tooltipType: "",
      tooltipText: "",
      tooltipScaleClass: "scale0",
      isShowAddDialog: false,
      formMode: 1,
      employeeId: "",
      reOpen: false,
      isShowPopup: false,
      popupContent: "",
      popupType: "",
      popupTitle: "",
    };
  },
  // CÁC PHƯƠNG THỨC 
  methods: {
    
    showDeletePopup() {
      this.isShowPopup = true;
      this.popupContent = "Bạn có chắc muốn xóa nhân viên này không ?" ;
      this.popupType = "danger";
      this.popupTitle = "Xóa các bản ghi";
    },
     /**
     * Ẩn Popup
     * Created : Phạm Tuấn Dũng (13/2/2021)
     */
    hidePopup() {
      this.isShowPopup = false;
    },
    /**
     * Đóng Toast 
     * Created : Phạm Tuấn Dũng (13/2/2021)
     */
    closeToolTip() {
      this.tooltipScaleClass = "scale0";
    },
    /**
     * Event : Click vào icon toggle để mở rộng / thu về kich thước bình thường.
     * Created : Phạm Tuấn Dũng (13/2/2021)
     */
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
