<template>
  <div
    class="employee-dialog"
    id="employee__dialog"
    title="Thông tin nhân viên"
    :class="{ 'e-dialog-hidden': isHide }"
  >
    <div class="dialog-modal"></div>
    <div class="employee-profile-dialog">
      <div class="dialog-header">
        <p>THÔNG TIN NHÂN VIÊN</p>
        <Button
          buttonId="btn-close"
          iClass="fa-times"
          @btnClick="btnCancelOnClick"
        />
      </div>
      <div class="dialog-content">
        <div class="dialog-content-left">
          <div class="avt-default"></div>
          <div class="text-avatar">
            (Vui lòng chọn ảnh có định dạng <br />.jpg, .jpeg, .png, .gif)
          </div>
        </div>
        <div class="dialog-content-right">
          <div class="thongtinchung">
            <div class="title-info">
              <p>A. THÔNG TIN CHUNG</p>
              <div class="layout"></div>
            </div>
            <div class="my-row">
              <!-- EMPLOYEE CODE -->
              <div class="small-info numberField" fieldName="EmployeeCode">
                <p>Mã nhân viên (<font color="red">*</font>)</p>
                <Input
                  inputClass="autofocus"
                  inputType="text"
                  inputPlacehoder=""
                  inputId="employee__code"
                  :inputValue="employee.EmployeeCode"
                  :isBorderRed="statusValide_EmployeeCode"
                  @input-blur="validateEmployeeCode"
                  v-model="employee.EmployeeCode"
                />
              </div>
              <!-- FULLNAME -->
              <div class="small-info" fieldName="FullName">
                <p>Họ và tên (<font color="red">*</font>)</p>
                <Input
                  inputClass="autofocus"
                  inputType="text"
                  inputPlacehoder=""
                  inputId="employee__fullname"
                  :inputValue="employee.FullName"
                  :isBorderRed="statusValide_FullName"
                  @input-blur="validateFullName"
                  v-model="employee.FullName"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- DATE OF BIRTH -->
              <div class="small-info" fieldName="DateOfBirth">
                <p>Ngày sinh</p>
                <!-- <input id="employee__dob" type="date" tabindex="3" value="" /> -->
                <Input
                  inputClass="autofocus"
                  inputType="date"
                  inputId="employee__dateofbirth"
                  v-model="employee.DateOfBirth"
                />
              </div>
              <!-- GENDER -->
              <div class="small-info" fieldName="Gender">
                <p>Giới tính</p>
                <!-- Dropdown -->
                <Dropdown
                  dropdownClass="dropdown form-dropdown"
                  dropdownId="employee__gender"
                  dropdownItemList="dropdown-list-item"
                  tabindex="4"
                  dropdownItemValueId="Gender"
                  dropdownItemValueName="GenderName"
                  :selectedId="employee.Gender + ''"
                  v-model="employee.Gender"
                />
                <!-- Dropdown -->
              </div>
            </div>

            <div class="my-row">
              <!-- ID CARD NUMBER -->
              <div class="small-info" fieldName="IdentityNumber">
                <p>Số CMND/ Căn cước (<font color="red">*</font>)</p>
                <Input
                  inputClass="autofocus"
                  inputType="text"
                  inputPlacehoder=""
                  inputId="employee__idnumber"
                  :inputValue="employee.IdentityNumber"
                  :isBorderRed="statusValide_IndentityNumber"
                  @input-blur="validateIdentityNumber"
                  v-model="employee.IdentityNumber"
                />
              </div>
              <!-- RELESE DATE -->
              <div class="small-info" fieldName="IdentityDate">
                <p>Ngày cấp</p>
                <Input
                  inputType="date"
                  inputPlacehoder=""
                  inputId="employee__iddate"
                  :inputValue="employee.IdentityDate"
                  v-model="employee.IdentityDate"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- RELESE PLACE -->
              <div class="small-info" fieldName="IdentityPlace">
                <p>Nơi cấp</p>
                <Input
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="employee.IdentityPlace"
                  inputId="employee__idplace"
                  v-model="employee.IdentityPlace"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- EMAIL -->
              <div class="small-info" fieldName="Email">
                <p>Email (<font color="red">*</font>)</p>
                <Input
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="employee.Email"
                  :isBorderRed="statusValide_Email"
                  inputId="employee__email"
                  @input-blur="validateEmail"
                  v-model="employee.Email"
                />
              </div>
              <!-- PHONE -->
              <div class="small-info" fieldName="PhoneNumber">
                <p>Số điện thoại (<font color="red">*</font>)</p>
                <Input
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="employee.PhoneNumber"
                  :isBorderRed="statusValide_PhoneNumber"
                  inputId="employee__phone"
                  @input-blur="validatePhoneNumber"
                  v-model="employee.PhoneNumber"
                />
              </div>
            </div>
          </div>
          <div class="thongtincongviec">
            <div class="title-info">
              <p>B. THÔNG TIN CÔNG VIỆC</p>
              <div class="layout"></div>
            </div>
            <div class="my-row">
              <!-- POSISION -->
              <div class="small-info" fieldName="PositionId">
                <p>Vị trí</p>
                <!-- Dropdown -->
                <Dropdown
                  dropdownClass="dropdown form-dropdown"
                  dropdownId="employee__position"
                  dropdownItemList="dropdown-list-item"
                  tabindex="10"
                  dropdownItemValueId="PositionId"
                  dropdownItemValueName="PositionName"
                  :selectedId="employee.PositionId"
                  v-model="employee.PositionId"
                  myUrl="v1/Positions"
                />
                <!-- Dropdown -->
              </div>
              <!-- DEPARTMENT -->
              <div class="small-info" fieldName="DepartmentId">
                <p>Phòng ban</p>
                <!-- Dropdown -->
                <Dropdown
                  dropdownClass="dropdown form-dropdown"
                  dropdownId="employee__department"
                  dropdownItemList="dropdown-list-item"
                  tabindex="11"
                  dropdownItemValueId="DepartmentId"
                  dropdownItemValueName="DepartmentName"
                  :selectedId="employee.DepartmentId"
                  v-model="employee.DepartmentId"
                  myUrl="api/Department"
                />
                <!-- Dropdown -->
              </div>
            </div>
            <div class="my-row">
              <!-- TAX CODE -->
              <div class="small-info">
                <p>Mã số thuế cá nhân</p>
                <Input
                  inputType="text"
                  inputId="employee__taxcode"
                  :inputValue="employee.PersonalTaxCode"
                  v-model="employee.PersonalTaxCode"
                />
              </div>
              <!-- SALARY -->
              <div class="small-info number" fieldName="Salary">
                <p>Mức lương cơ bản</p>
                <Input
                  inputClass="right-align"
                  inputType="text"
                  inputPlacehoder=""
                  inputId="employee__basesalary"
                  :inputValue="formatSalary1(employee.Salary)"
                  v-model="employee.Salary"
                  @formatSalary="formatSalary"
                />
                <div class="money-unit">(VNĐ)</div>
              </div>
            </div>
            <div class="my-row">
              <!-- JOIN DATE -->
              <div class="small-info" fieldName="JoinDate">
                <p>Ngày gia nhập công ty</p>
                <Input
                  inputType="date"
                  inputPlacehoder=""
                  :inputValue="employee.JoinDate"
                  v-model="employee.JoinDate"
                />
              </div>
              <!-- WORK STATUS -->
              <div class="small-info" fieldName="WorkStatus">
                <p>Tình trạng công việc</p>
                <!-- Dropdown -->
                <Dropdown
                  dropdownClass="dropdown form-dropdown"
                  dropdownId="employee__workstatus"
                  dropdownItemList="dropdown-list-item"
                  tabindex="11"
                  dropdownItemValueId="WorkStatus"
                  dropdownItemValueName="WorkStatusName"
                  :selectedId="employee.WorkStatus + ''"
                  v-model="employee.WorkStatus"
                />
                <!-- Dropdown -->
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <Button
          buttonId="btn-cancel"
          buttonClass="button button-icon"
          buttonText="Hủy"
          @btnClick="btnCancelOnClick"
        />
        <Button
          buttonId="btn-save"
          buttonClass="button button-icon"
          buttonText="Lưu"
          iClass="fa-save"
          @btnClick="btnSaveOnclick"
        />
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../../../main.js";
import { required, minLength, between } from "vuelidate/lib/validators";
import { eventBus1, eventBus2 } from "../../../main.js";
import Input from "../../base/BaseInput.vue";
import Dropdown from "../../base/BaseDropdown.vue";
import Button from "../../base/BaseButton.vue";
export default {
  setup() {},
  components: {
    Input,
    Dropdown,
    Button,
  },
  validations: {
    name: {
      required,
      minLength: minLength(4),
    },
    age: {
      between: between(20, 30),
    },
  },
  // CÁC THUỘC TÍNH
  props: {
    isHide: {
      type: Boolean,
      default: true,
      require: true,
    },
    employeeId: {
      type: String,
      default: "",
      require: true,
    },
    mode: {
      type: Number,
      require: true,
      default: 0, // 0 - Thêm mới ; 1 - Sửa
    },
    isReOpenForm: {
      type: Boolean,
      default: false,
    },
  },

  created() {
    this.employee.Salary = "";
    eventBus2.$on("addOrUpdateData", () => {
      this.addData();
    });
    this.getNewEmployeeCode();
  },
  // CÁC PHƯƠNG THỨC
  methods: {
    /** --------------------------------------------------------
     * @Function:Hàm format định dạng lương
     * @Athor: Phạm Tuấn Dũng
     * @Date: 23.07.2021
     */
    formatSalary1(myinput) {
      myinput += "";
      if (myinput != null) {
        myinput.replaceAll(".", "");

        let onlynumber = "";
        for (var i = 0; i < myinput.length; i++) {
          if (!isNaN(parseInt(myinput[i], 10))) {
            onlynumber += myinput[i];
          }
        }
        return Number(onlynumber).toLocaleString("vi");
      }
      return 0;
    },
    /** --------------------------------------------------------
     * @Event: Nhấn nút "Cancel" để hủy điền form
     * @Athor: Phạm Tuấn Dũng
     * @Date: 23.07.2021
     */
    btnCancelOnClick() {
      this.$emit("btnAddOnClick", true);
    },
    /** --------------------------------------------------------
     * @Event: Hàm gọi API lấy về mã nhân viên mới.
     * @Athor: Phạm Tuấn Dũng
     * @Date: 10.08.2021
     */
    getNewEmployeeCode() {
      var vm = this;
      let tmpEmployee = {};
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode")
        .then((res) => {
          console.log(res.data);
          tmpEmployee.EmployeeCode = res.data;
          vm.employee = tmpEmployee;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    addData() {
      let vm = this;
      console.log("SavedEmployee", vm.employee);
      let tmpEmployee = vm.employee;

      tmpEmployee.Salary = parseInt(
        (tmpEmployee.Salary + "").replaceAll(".", "")
      );
      if (vm.mode == 0) {
        axios
          .post(`http://cukcuk.manhnv.net/v1/Employees/`, tmpEmployee)
          .then(() => {
            setTimeout(() => {
              eventBus2.$emit("confirmCloseAddForm");
              eventBus2.$emit("loadData");
            }, 500);
            eventBus1.$emit("showTooltipAddSuccess");
          })
          .catch((res) => {
            console.log(res);
          });
      } else {
        axios
          .put(
            `http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`,
            tmpEmployee
          )
          .then(() => {
            setTimeout(() => {
              eventBus2.$emit("confirmCloseAddForm");
              eventBus2.$emit("loadData");
            }, 500);
            eventBus1.$emit("showTooltipUpdateSuccess");
          })
          .catch((res) => {
            console.log(res);
          });
      }
    },
    /** --------------------------------------------------------
     * @Event: Nhấn nút "Save" để cập nhật/thêm mới dữ liệu form
     * @Athor: Phạm Tuấn Dũng
     * @Date: 01.08.2021
     */
    btnSaveOnclick() {
      if (this.mode == 0) {
        console.log("btnsave 0");
        eventBus2.$emit("showPopupConfirmAdd");
      } else {
        console.log("btnsave 1");
        eventBus2.$emit("showPopupConfirmUpdate");
      }
    },
    /**
     * Hàm validate dữ liệu mã nhân viện
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    validateEmployeeCode() {
      console.log("EmployeeCode 01");
      if (
        this.employee.EmployeeCode == "" ||
        this.employee.EmployeeCode == undefined
      ) {
        eventBus2.$emit("showTooltipInputRequied");
        this.statusValide_EmployeeCode = true;
        console.log("INVALID EMPLOYEE_CODE !!");
      } else {
        this.statusValide_EmployeeCode = false;
      }
    },
    /**
     * Hàm validate dữ liệu họ tên
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    validateFullName() {
      console.log("FullName 01");
      if (this.employee.FullName == "" || this.employee.FullName == undefined) {
        eventBus2.$emit("showTooltipInputRequied");
        this.statusValide_FullName = true;
        console.log("INVALID FULL_NAME !!");
      } else {
        this.statusValide_FullName = false;
      }
    },
    /**
     * Hàm validate dữ liệu họ tên đầy đủ
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    validateIdentityNumber() {
      console.log("Inden 01");
      if (
        this.employee.IdentityNumber == "" ||
        this.employee.IdentityNumber == undefined
      ) {
        eventBus2.$emit("showTooltipInputRequied");
        this.statusValide_IndentityNumber = true;
        console.log("INVALID INDETITY NUMBER !!");
      } else {
        this.statusValide_IndentityNumber = false;
      }
    },
    /**
     * Hàm validate dữ liệu email
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    validateEmail() {
      console.log("Email 01");
      if (this.employee.Email == "" || this.employee.Email == undefined) {
        eventBus2.$emit("showTooltipInputRequied");
        this.statusValide_Email = true;
        console.log("INVALID EMAIL NULL !!");
      } else if (!store.validateEmail(this.employee.Email)) {
        // Tạo một toast message mới : ND : Thông tin nhập không hợp lệ !!
        eventBus2.$emit("showTooltipInputRequied2");
        this.statusValide_Email = true;
        console.log("INVALID EMAIL !!");
      } else {
        this.statusValide_Email = false;
      }
    },
    /**
     * Hàm validate dữ liệu số điện thoại
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    validatePhoneNumber() {
      console.log("Phone 01");
      if (
        this.employee.PhoneNumber == "" ||
        this.employee.PhoneNumber == undefined
      ) {
        eventBus2.$emit("showTooltipInputRequied");
        this.statusValide_PhoneNumber = true;
        console.log("INVALID PHONE NUMBER NULL !!");
      } else if (!store.validatePhoneNumber(this.employee.PhoneNumber)) {
        // Tạo một toast message mới : ND : Thông tin nhập không hợp lệ !!
        eventBus2.$emit("showTooltipInputRequied2");
        this.statusValide_PhoneNumber = true;
        console.log("INVALID PHONE NUMBER 222 !!");
      } else {
        this.statusValide_PhoneNumber = false;
      }
    },
    /**
     * Hàm format dữ liệu sinh nhật về chuẩn yyyy-mm-dd. Mục đích :  Hiển thị lên form
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    formatDateToValue(dateInput) {
      let dob = null;
      if (dateInput != null) {
        let newDate = new Date(dateInput);
        let date = newDate.getDate();
        date = date < 10 ? "0" + date : date;
        let month = newDate.getMonth() + 1;
        month = month < 10 ? "0" + month : month;
        let year = newDate.getFullYear();
        dob = `${year}-${month}-${date}`;
      }
      return dob;
    },
    /**
     * Hàm reset dữ liệu nhân viên với các trường (mã nhân viên, mã phòng ban, giới tính, tình trạng công việc).
     * Mục đích :
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    resetEntityInfo() {
      let vm = this,
        newEmployee = {};
      newEmployee.DepartmentId = "";
      newEmployee.PositionId = "";
      newEmployee.Gender = "";
      newEmployee.WorkStatus = "";
      vm.employee = newEmployee;
    },
    /**
     * Hàm format lại dữ liệu tiền lương lên form. Ví dụ 1000000 -> 1.000.000
     * Created by : Phạm Tuấn Dũng (13/08/2021)
     */
    formatSalary() {
      if (!this.employee.Salary) {
        this.employee.Salary = "";
      }
      this.employee.Salary = this.formatSalary1(this.employee.Salary);
    },
  },
  // DỮ LIỆU KHỞI TẠO
  data() {
    return {
      employee: {}, // Object employee để lưu thông tin 1 nhân viên
      newEmployeeCode: "", // Mã nhân viên mới

      // Trang thái validate. Mục đích : input form border xanh hoặc đỏ tương ứng với các trại thái đúng / sai
      statusValide_EmployeeCode: false,
      statusValide_FullName: false,
      statusValide_IndentityNumber: false,
      statusValide_Email: false,
      statusValide_PhoneNumber: false,
    };
  },
  // HÀM WATCH
  watch: {
    /**
     * Hàm cập nhật dữ liệu khi gọi lại form
     */
    isReOpenForm: function () {
      let vm = this;

      // Cập nhật lại trạng thái validate là false (tức chưa validate) khi mở lại form
      vm.statusValide_EmployeeCode = false;
      vm.statusValide_FullName = false;
      vm.statusValide_IndentityNumber = false;
      vm.statusValide_Email = false;
      vm.statusValide_PhoneNumber = false;

      vm.resetEntityInfo();
      if (vm.mode == 1) {
        // Gọi API lấy thông tin chi tiết
        axios
          .get(`http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`)
          .then((res) => {
            console.log(res.data);
            var employee = res.data;
            employee.DateOfBirth = vm.formatDateToValue(employee.DateOfBirth);
            employee.IdentityDate = vm.formatDateToValue(employee.IdentityDate);
            employee.JoinDate = vm.formatDateToValue(employee.JoinDate);
            vm.employee = employee;
          })
          .catch((res) => {
            console.log(res);
          });
        // Binding dữ liệu
      } else if (vm.mode == 0) {
        vm.resetEntityInfo();
        vm.getNewEmployeeCode();
      }
    },
    mode: function () {
      if (this.mode == 0) {
        this.employee = {};
      }
    },
  },
};
</script>

