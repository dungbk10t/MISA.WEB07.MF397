<template>
  <div class="content">
    <div class="content-header">
      <div class="header-left">
        <select>
          <option value="">Nhà hàng Biển Đông</option>
          <option value="">Nhà hàng Biển Tây</option>
          <option value="">Nhà hàng Biển Nam</option>
          <option value="">Nhà hàng Biển Bắc</option>
        </select>
      </div>
      <div class="header-right">
        <div class="avatar"></div>
        <p>Phạm Tuấn Dũng</p>
        <div class="option-icon"></div>
      </div>
    </div>
    <div class="content-body">
      <div class="content-title">
        <b>Danh sách nhân viên</b>
        <span>
          <button
            id="btn-delete"
            class="button button-icon"
            @click="btnDeleteOnClick"
          >
            <i class="fas fa-trash-alt"></i>
            <p>Xóa nhân viên</p>
          </button>
          <button
            id="btn-add"
            class="button button-icon"
            @click="btnAddOnClick(false)"
          >
            <i class="fas fa-user-plus"></i>
            <p>Thêm nhân viên</p>
          </button>
        </span>
      </div>
      <div class="content-toolbar">
        <div class="toolbar-left" >
          <input
            class="input-icon input-search"
            type="text"
            placeholder="Tìm kiếm theo mã, tên hoặc số điện thoại"
          />
          <!-- Dropdown Position 1  -->
          <Dropdown
            style="width:213px"
            defaultName="Tất cả vị trí"
            dropdownClass="dropdown form-dropdown"
            dropdownId="employee__position"
            dropdownItemList="dropdown-list-item"
            dropdownItemValueId="PositionId"
            dropdownItemValueName="PositionName"
            myUrl="v1/Positions"
          />
          <!-- Dropdown Position 1  -->
          <!-- Dropdown Department 1  -->
          <Dropdown
            style="width:213px"
            defaultName="Tất cả phòng ban"
            dropdownClass="dropdown form-dropdown"
            dropdownId="employee__department_1"
            dropdownItemList="dropdown-list-item"
            tabindex="11"
            dropdownItemValueId="DepartmentId"
            dropdownItemValueName="DepartmentName"
             myUrl="api/Department"
          />

          <!-- Dropdown Department 1  -->
        </div>

        <div class="toolbar-right">
          <button
            id="btn-refresh"
            class="button button-refresh"
            @click="btnRefreshOnClick"
          >
            <i class="fas fa-sync-alt"></i>
          </button>
        </div>
      </div>
      <div class="grid grid-employee" id="employee__table">
        <table width="100%" border="0">
          <thead>
            <tr>
              <th>
                <div class="checkbox"></div>
              </th>
              <th>#</th>
              <th fieldName="EmployeeCode">Mã nhân viên</th>
              <th fieldName="FullName">Họ và tên</th>
              <th fieldName="DateOfBirth" format="dmy">Ngày sinh</th>
              <th fieldName="GenderName">Giới tính</th>
              <th fieldName="PhoneNumber">Điện thoại</th>
              <th fieldName="Email">Email</th>
              <th fieldName="PositionName">Chức vụ</th>
              <th fieldName="DepartmentName">Phòng ban</th>
              <th fieldName="Salary" format="money" style="text-align: right">
                Mức lương cơ bản
              </th>
              <th fieldName="WorkStatus" format="workStatus">
                Tình trạng công việc
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(employee, index) in employees"
              :key="employee.EmployeeId"
              @dblclick="rowOnDblClick(employee.EmployeeId)"
            >
              <td>
                <div
                  class="checkbox"
                  :class="{
                    'checked icon-tick': checkedBoxs.includes(
                      employee.EmployeeId
                    ),
                  }"
                  @click="selectcheckBox(employee.EmployeeId)"
                ></div>
                <!-- <div class="checkbox checked" @click="selectcheckBox(employee.EmployeeId)"></div> -->
              </td>
              <td>{{ index + 1 }}</td>
              <td>{{ employee.EmployeeCode }}</td>
              <td>{{ employee.FullName }}</td>
              <td>{{ formatDate(employee.DateOfBirth) }}</td>
              <td>{{ employee.GenderName }}</td>
              <td>{{ employee.PhoneNumber }}</td>
              <td>{{ employee.Email }}</td>
              <td>{{ employee.DepartmentName }}</td>
              <td>{{ employee.PositionName }}</td>
              <td style="text-align: right">
                {{ fomatSalary(employee.Salary) }}
              </td>
              <td>{{ formatworkStatus(employee.WorkStatus) }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="content-paging">
        <div class="paging-bar">
          <div class="paging-record-info">
            Hiển thị <b>1-10/10000</b> Nhân viên hàng
          </div>
          <div class="paging-option">
            <div class="btn-select-prev-list-page"></div>
            <div class="btn-select-prev-page"></div>
            <div class="list-page">
              <button class="page-number">1</button>
              <button class="page-number">2</button>
              <button class="page-number">3</button>
              <button class="page-number">4</button>
            </div>
            <div class="btn-select-next-page"></div>
            <div class="btn-select-next-list-page"></div>
          </div>
          <div class="paging-record-option">10 Nhân viên trên trang</div>
        </div>
      </div>
    </div>
    <EmployeeDetail
      v-bind:isHide="isHideDialogDetail"
      v-bind:employeeId="employeeId"
      :mode="modeForm"
      @btnAddOnClick="btnAddOnClick"
      @btnSaveOnClick="btnSaveOnClick"
      :isReOpen="reOpen"
    />
  </div>
</template>

<script>
import EmployeeDetail from "@/components/view/employee/EmployeeDetail";
import Dropdown from "../../base/BaseDropdown.vue";
import axios from "axios";
import { eventBus1 } from "../../../main";

export default {
  name: "EmployeeList",
  components: {
    EmployeeDetail,
    Dropdown,
  },
  data() {
    return {
      employee: {},
      employees: [],
      checkedBoxs: [],
      employeeId: "",
      isHideDialogDetail: true,
      modeForm: 0,
      reOpen: false,
    };
  },
  created() {
    var vm = this;
    axios
      .get("http://cukcuk.manhnv.net/v1/Employees")
      .then((res) => {
        console.log(res.data);
        vm.employees = res.data;
      })
      .catch((res) => {
        console.log(res);
      });
  },

  methods: {
    /** -----------------------------------------------------------------------
     * @Event : Hiển thị form chi tiết khi nhấn button thêm nhân viên
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    btnAddOnClick(isHide) {
      this.isHideDialogDetail = isHide;
      this.modeForm = 0; // Khi nhấn vào cập nhật trạng thái 0 - Thêm mới
    },
    /** -----------------------------------------------------------------------
     * @Event : Thêm mới / cập nhật dữ liệu dữ liệu khi click vào nut "Save"
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    btnSaveOnClick(isHide) {
      this.btnAddOnClick(true);
      if (isHide) {
        this.loadData();
      }
    },
    /** -----------------------------------------------------------------------
     * @Event : Hiển thị form chi tiết khi double vào một hàng của bảng dữ liệu.
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    rowOnDblClick(empId) {
      // alert(employeeId);
      this.isHideDialogDetail = false;
      this.employeeId = empId;
      this.modeForm = 1; // Khi nhấn vào cập nhật trạng thái 1 - Sửa
    },
    btnDeleteOnClick() {
      var vm = this;
      vm.checkedBoxs.forEach(function (item) {
        axios
          .delete("http://cukcuk.manhnv.net/v1/Employees/" + item)
          .then(() => {
            vm.checkedBoxs = vm.checkedBoxs.filter((e) => e !== item);
            if (vm.checkedBoxs.length == 0) {
              eventBus1.$emit("showTooltipDeleteSuccess");
              vm.loadData();
              // alert("Đã xóa hết các bản ghi được chọn ! ");
            }
          });
      });
    },
    btnRefreshOnClick() {
      this.loadData();
    },
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
    selectcheckBox(id) {
      if (this.checkedBoxs.includes(id)) {
        this.checkedBoxs = this.checkedBoxs.filter((e) => e !== id);
      } else {
        this.checkedBoxs.push(id);
      }
    },
    /** -----------------------------------------------------------------------
     * @Format : Format dữ liệu ngày sinh thành dạng dd/mm/yyyy
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    formatDate(dateInput) {
      let dob = null;
      if (dateInput != null) {
        let newDate = new Date(dateInput);
        let date = newDate.getDate();
        let month = newDate.getMonth() + 1;
        let year = newDate.getFullYear();
        dob = `${date}/${month}/${year}`;
      }
      return dob;
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
        dob = `${year}-${month}/${date}`;
      }
      return dob;
    },
    /** -----------------------------------------------------------------------
     * @Format : Format dữ liệu lương thành dạng có dấu chấm . VD : 1.000.000
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    fomatSalary(inputData) {
      if (inputData == null) {
        return "";
      } else {
        inputData += "";
        var x = inputData.split(".");
        var x1 = x[0];
        var x2 = x.length > 1 ? "." + x[1] : "";
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
          x1 = x1.replace(rgx, "$1" + "." + "$2");
        }
        return x1 + x2;
      }
    },
    /** -----------------------------------------------------------------------
     * @Format : Format dữ liệu tình trạng công việc
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    formatworkStatus(inputwStatusStr) {
      var wStatusStr;
      switch (inputwStatusStr) {
        case 0:
          wStatusStr = "Đang thử việc";
          break;
        case 1:
          wStatusStr = "Đang làm việc";
          break;
        case 2:
          wStatusStr = "Đã nghỉ việc";
          break;
        case 3:
          wStatusStr = "Đã nghỉ hưu";
          break;
        default:
          wStatusStr = "";
          break;
      }
      return wStatusStr;
    },
  },
};
</script>