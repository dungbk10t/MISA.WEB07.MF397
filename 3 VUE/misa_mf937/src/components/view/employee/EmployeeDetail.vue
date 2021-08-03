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
              <div class="small-info" fieldName="EmployeeCode">
                <p>Mã nhân viên (<font color="red">*</font>)</p>
                <input
                  id="employee__code"
                  type="text"
                  class="autofocus"
                  tabindex="1"
                  value=""
                  required
                  v-model="employee.EmployeeCode"
                />
              </div>
              <!-- FULLNAME -->
              <div class="small-info" fieldName="FullName">
                <p>Họ và tên(<font color="red">*</font>)</p>
                <input
                  id="employee__fullname"
                  type="text"
                  tabindex="2"
                  value=""
                  required
                  v-model="employee.FullName"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- DATE OF BIRTH -->
              <div class="small-info" fieldName="DateOfBirth">
                <p>Ngày sinh</p>
                <input id="employee__dob" type="date" tabindex="3" value="" />
              </div>
              <!-- GENDER -->
              <div class="small-info" fieldName="Gender">
                <p>Giới tính</p>
                <!-- Dropdown -->
                <div
                  class="dropdown form-dropdown"
                  id="employee__gender"
                  tabindex="4"
                >
                  <div class="dropdown-list">
                    <div class="dropdown-list-item" value="0">
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Nữ</div>
                    </div>
                    <div class="dropdown-list-item" value="1">
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Nam</div>
                    </div>
                    <div class="dropdown-list-item" value="2">
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Không xác định</div>
                    </div>
                  </div>
                  <div class="dropdown-select">
                    <div class="select">Chọn giới tính</div>
                    <i class="fas fa-angle-down"></i>
                  </div>
                </div>
                <!-- Dropdown -->
              </div>
            </div>

            <div class="my-row">
              <!-- ID CARD NUMBER -->
              <div class="small-info" fieldName="IdentityNumber">
                <p>Số CMND/Căn cước(<font color="red">*</font>)</p>
                <input
                  id="employee__idnumber"
                  type="text"
                  tabindex="5"
                  value=""
                  required
                  v-model="employee.IdentityNumber"
                />
              </div>
              <!-- RELESE DATE -->
              <div class="small-info" fieldName="IdentityDate">
                <p>Ngày cấp</p>
                <input
                  id="employee__iddate"
                  type="date"
                  tabindex="6"
                  value=""
                  v-model="employee.IdentityDate"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- RELESE PLACE -->
              <div class="small-info" fieldName="IdentityPlace">
                <p>Nơi cấp</p>
                <input
                  id="employee__idplace"
                  type="text"
                  tabindex="7"
                  value=""
                  v-model="employee.IdentityPlace"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- EMAIL -->
              <div class="small-info" fieldName="Email">
                <p>Email(<font color="red">*</font>)</p>
                <input
                  id="employee__email"
                  type="text"
                  tabindex="8"
                  value=""
                  required
                  v-model="employee.Email"
                />
              </div>
              <!-- PHONE -->
              <div class="small-info" fieldName="PhoneNumber">
                <p>Số điện thoại(<font color="red">*</font>)</p>
                <input 
                    id="employee__phone" 
                    type="text" 
                    tabindex="9" 
                    value="" 
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
                <div
                  class="dropdown form-dropdown"
                  id="employee__position"
                  tabindex="10"
                >
                  <div class="dropdown-list">
                    <div
                      class="dropdown-list-item pos-txt"
                      value="30d41e52-5e66-72bc-6c1c-b47866e0b131"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Giám đốc</div>
                    </div>
                    <div
                      class="dropdown-list-item pos-txt"
                      value="25c6c36e-1668-7d10-6e09-bf1378b8dc91"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Thu ngân</div>
                    </div>
                    <div
                      class="dropdown-list-item pos-txt"
                      value="148ed882-32b8-218e-9c20-39c2f00615e8"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Nhân viên Marketing</div>
                    </div>
                  </div>
                  <div class="dropdown-select">
                    <div class="select">Chọn vị trí</div>
                    <i class="fas fa-angle-down"></i>
                  </div>
                </div>
              </div>
              <!-- DEPARTMENT -->
              <div class="small-info" fieldName="DepartmentId">
                <p>Phòng ban</p>
                <!-- Dropdown -->
                <div
                  class="dropdown form-dropdown"
                  id="employee__department"
                  tabindex="11"
                >
                  <div class="dropdown-list">
                    <div
                      class="dropdown-list-item"
                      value="142cb08f-7c31-21fa-8e90-67245e8b283e"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Phòng Marketting</div>
                    </div>
                    <div
                      class="dropdown-list-item"
                      value="17120d02-6ab5-3e43-18cb-66948daf6128"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Phòng đào tạo</div>
                    </div>
                    <div
                      class="dropdown-list-item"
                      value="469b3ece-744a-45d5-957d-e8c757976496"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Phòng Nhân sự</div>
                    </div>
                    <div
                      class="dropdown-list-item"
                      value="4e272fc4-7875-78d6-7d32-6a1673ffca7c"
                    >
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Phòng Công nghệ</div>
                    </div>
                  </div>
                  <div class="dropdown-select">
                    <div class="select">Chọn phòng ban</div>
                    <i class="fas fa-angle-down"></i>
                  </div>
                </div>
                <!-- Dropdown -->
              </div>
            </div>
            <div class="my-row">
              <!-- TAX CODE -->
              <div class="small-info">
                <p>Mã số thuế cá nhân</p>
                <input
                  id="employee__taxcode"
                  type="text"
                  tabindex="13"
                  value=""
                  v-model="employee.PersonalTaxCode"
                />
              </div>
              <!-- SALARY -->
              <div class="small-info" fieldName="Salary">
                <p>Mức lương cơ bản</p>
                <input
                  id="employee__basesalary"
                  class="right-align"
                  type="text"
                  tabindex="14"
                  value=""
                  v-model="employee.Salary"
                />
              </div>
            </div>
            <div class="my-row">
              <!-- JOIN DATE -->
              <div class="small-info" fieldName="JoinDate">
                <p>Ngày gia nhập công ty</p>
                <input
                  id="employee__joindate"
                  type="date"
                  tabindex="15"
                  value=""
                  v-model="employee.JoinDate"
                />
              </div>
              <!-- WORK STATUS -->
              <div class="small-info" fieldName="WorkStatus">
                <p>Tình trạng công việc</p>
                <!-- Dropdown -->
                <div
                  class="dropdown form-dropdown drop-up"
                  id="employee__workstatus"
                  tabindex="12"
                >
                  <div class="dropdown-list">
                    <div class="dropdown-list-item workStatus-txt" value="0">
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Đang làm việc</div>
                    </div>
                    <div class="dropdown-list-item workStatus-txt" value="1">
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Đang thử việc</div>
                    </div>
                    <div class="dropdown-list-item workStatus-txt" value="2">
                      <i class="fas fa-check"></i>
                      <div class="dd-item-text">Đã nghỉ việc</div>
                    </div>
                  </div>
                  <div class="dropdown-select">
                    <div class="select">Chọn tình trạng</div>
                    <i class="fas fa-angle-down"></i>
                  </div>
                </div>
                <!-- Dropdown -->
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <button id="btn-cancel" class="btn-cancel" @click="btnCancelOnClick">
          <b>Hủy</b>
        </button>
        <button id="btn-save" class="button button-icon btn-save" @click="btnSaveOnclick">
          <i class="fas fa-save"></i>
          <p>Lưu</p>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { required, minLength, between } from 'vuelidate/lib/validators';


export default {
  setup() {},
  validations: {
    name: {
      required,
      minLength: minLength(4)
    },
    age: {
      between: between(20, 30)
    }
  },
  props: {
    isHide: {
      type: Boolean,
      default: true,
      require: true,
    },
    employeeId: {
      type: String,
      default: '',
      require: true,
    },
    mode:{
        type:Number,
        require: true,
        default: 0 // 0 - Thêm mới ; 1 - Sửa
    },
    isLoadAgain: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
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
    loadData() {
      var vm = this;
      console.log(this);
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees")
        .then((res) => {
          vm.employees = res.data;
        })
        .catch((res) => {
          console.log(res); 
        });
    },
    btnSaveOnclick() {
      /** --------------------------------------------------------
       * @Event: Nhấn nút "Save" để cập nhật/thêm mới dữ liệu form
       * @Athor: Phạm Tuấn Dũng
       * @Date: 08.01.2021
       */
      let vm = this;
      if(this.mode == 0) {
        axios
        .post(`http://cukcuk.manhnv.net/v1/Employees/`, vm.employee)
        .then((res) => {
          console.log(res);
          vm.$emit("btnSaveOnClick", true);
          alert("Thêm mới thành công !!");
        })
        .catch((res) => {
          console.log(res);
        });
      }
      else {
        axios
        .put(`http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`,vm.employee)
        .then(() => {
          vm.$emit("btnSaveOnClick", true);
          alert("Sửa mới thành công !!");
        })
        .catch((res) => {
          console.log(res);
        });
      }
    }
  },
  data() {
    return {
      employee: {},
      
    };
  },
  watch: {
    employeeId: function () {
      // alert("Watch: " + this.employeeId);
      // Gọi API lấy thông tin chi tiết
      let vm = this;
      axios
        .get(`http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`)
        .then((res) => {
          console.log(res.data);
          vm.employee = res.data;
        })
        .catch((res) => {
          console.log(res);
        });
        // Binding dữ liệu
        
    },
    isLoadAgain() {
      this.loadData();
    },
    mode: function(){
        if (this.mode == 0) {
            this.employee={};
        }
    }
  },
  computed: {
    testEmployeeId: function () {
      return this.employeeId;
    },
  },
};
</script>

