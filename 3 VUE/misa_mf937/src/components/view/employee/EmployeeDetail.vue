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
        <button id="btn-close" @click="btnCancelOnClick">
          <i class="fas fa-times"></i>
        </button>
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
                  :inputValue="employee.EmployeeCode"
                  inputId="employee__code"
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
                  :inputValue="employee.FullName"
                  inputId="employee__fullname"
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
                  :inputValue="employee.DateOfBirth"
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
                  :inputValue="employee.IdentityNumber"
                  inputId="employee__idnumber"
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
                  :inputValue="employee.IdentityDate"
                  inputId="employee__iddate"
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
                  inputPlacehoder=""
                  :inputValue="employee.PersonalTaxCode"
                  inputId="employee__taxcode"
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
    isLoadAgain: {
      type: Boolean,
      default: false,
    },
    isReOpenForm: {
      type: Boolean,
      default: false,
    },
  },

  created() {
    // this.loadData();
    // eventBus1.$on("loadData", () => {
    //   this.loadData();
    // });
    this.employee.Salary = "";

    eventBus2.$on("addOrUpdateData", () => {
        this.addData();
        console.log("confirm Pressed");
      // else {
      //   this.updateData();
      //   console.log("confirm 1");
      // }
    });
    this.getNewEmployeeCode();
    // eventBus2.$on("invalidInput", () => {
    //   this.isValidForm = false;
    // });
  },
  methods: {
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
     * @Event: Hàm loadData
     * @Athor: Phạm Tuấn Dũng
     * @Date: 23.07.2021
     */
    // loadData() {
    //   var vm = this;
    //   console.log(this);
    //   axios
    //     .get("http://cukcuk.manhnv.net/v1/Employees")
    //     .then((res) => {
    //       vm.employees = res.data;
    //     })
    //     .catch((res) => {
    //       console.log(res);
    //     });
    // },
    getNewEmployeeCode() {
      var vm = this;
      let tmpEmployee={};
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
      let tmpEmployee= vm.employee;

      tmpEmployee.Salary=parseInt( (tmpEmployee.Salary +'').replaceAll('.',''));
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
      console.log(tmpEmployee);
    },
    updateDataaaa() {
      let vm = this;
      // console.log(vm.employee);
      axios
        .put(
          `http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`,
          vm.employee
        )
        .then(() => {
          // vm.$emit("btnSaveOnClick",true)
          setTimeout(() => {
            console.log(vm.employee);
            eventBus2.$emit("confirmCloseAddForm");
            eventBus2.$emit("loadData");
          }, 500);
          eventBus1.$emit("showTooltipUpdateSuccess");
        })
        .catch((res) => {
          console.log(res);
        });
    },
    btnSaveOnclick() {
      /** --------------------------------------------------------
       * @Event: Nhấn nút "Save" để cập nhật/thêm mới dữ liệu form
       * @Athor: Phạm Tuấn Dũng
       * @Date: 01.08.2021
       */
      // if (
      //   this.employee.EmployeeCode !== "" &&
      //   this.employee.FullName !== "" &&
      //   this.employee.IdentityNumber !== "" &&
      //   this.employee.Email !== "" &&
      //   this.employee.PhoneNumber !== ""
      // ) {
      // eventBus2.$emit("validateFormInput");
      if (this.mode == 0) {
        console.log("btnsave 0");
        eventBus2.$emit("showPopupConfirmAdd");
      } else {
        console.log("btnsave 1");
        eventBus2.$emit("showPopupConfirmUpdate");
      }
      // setTimeout(() => {
      //   if (this.isValidForm) {
      //     //ad
      //   } else {
      //     console.log("form is not valid");
      //     this.isValidForm= true;
      //   }
      // }, 500);

      // } else {
      //   eventBus2.$emit("showTooltipInputRequiedAll");
      // }
    },

    validateEmployeeCode() {
      console.log("EmployeeCode 01");
      if (
        this.employee.EmployeeCode == "" ||
        this.employee.EmployeeCode == undefined
      ) {
        eventBus2.$emit("showTooltipInputRequied");
        console.log("INVALID EMPLOYEE_CODE !!");
      }
    },
    validateFullName() {
      console.log("FullName 01");
      if (this.employee.FullName == "" || this.employee.FullName == undefined) {
        eventBus2.$emit("showTooltipInputRequied");
        console.log("INVALID FULL_NAME !!");
      }
    },
    validateIdentityNumber() {
      console.log("Inden 01");
      if (
        this.employee.IdentityNumber == "" ||
        this.employee.IdentityNumber == undefined
      ) {
        eventBus2.$emit("showTooltipInputRequied");
        console.log("INVALID INDETITY NUMBER !!");
      }
    },
    validateEmail() {
      console.log("Email 01");
      if (this.employee.Email == "" || this.employee.Email == undefined) {
        eventBus2.$emit("showTooltipInputRequied");
        console.log("INVALID EMAIL NULL !!");
      } else if (!store.validateEmail(this.employee.Email)) {
        // Tạo một toast message mới : ND : Thông tin nhập không hợp lệ !!
        eventBus2.$emit("showTooltipInputRequie2");
        console.log("INVALID EMAIL !!");
      }
    },
    validatePhoneNumber() {
      console.log("Phone 01");
      if (
        this.employee.PhoneNumber == "" ||
        this.employee.PhoneNumber == undefined
      ) {
        eventBus2.$emit("showTooltipInputRequied");
        console.log("INVALID PHONE NUMBER NULL !!");
      } else if (!store.validatePhoneNumber(this.employee.PhoneNumber)) {
        // Tạo một toast message mới : ND : Thông tin nhập không hợp lệ !!
        eventBus2.$emit("showTooltipInputRequied2");
        console.log("INVALID PHONE NUMBER 222 !!");
      }
    },
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
    resetEntityInfo() {
      let vm = this,
        newEmployee = {};
      newEmployee.DepartmentId = "";
      newEmployee.PositionId = "";
      newEmployee.Gender = "";
      newEmployee.WorkStatus = "";
      vm.employee = newEmployee;
    },
    formatSalary() {
      if (!this.employee.Salary) {
        console.log("--> vao If");
        this.employee.Salary = "";
      }
      console.log(this.employee.Salary);
      this.employee.Salary = this.formatSalary1(this.employee.Salary);
      console.log("2" + this.employee.Salary);
    },
    // getNewEmployeeCode() {
    //   console.log("Get New EmployeeCode");
    // },
  },
  data() {
    return {
      employee: {},
      newEmployeeCode: "",
      salary: "",
      // isValidForm: true,
    };
  },
  watch: {
    isReOpenForm: function () {
      let vm = this;
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

