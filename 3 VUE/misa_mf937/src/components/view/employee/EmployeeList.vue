<template>
  <div class="content" :class="{ 'collapse-content': isCollapseMenu }">
    <div class="content-header">
      <div class="header-left">
        <div class="iconMB"></div>
        <!-- Dropdown Restaurant  -->
        <Dropdown
          class="dropdown-select-header"
          defaultName="Tất cả vị trí"
          dropdownClass="dropdown form-dropdown"
          dropdownItemList="dropdown-list-item"
          dropdownItemValueId="RestaurantId"
          dropdownItemValueName="RestaurantName"
        />
        <!-- Dropdown Restaurant  -->
      </div>
      <div class="header-right">
        <div class="iconBell"></div>
        <div class="avatar"></div>
        <p>Phạm Tuấn Dũng</p>
        <div class="option-icon"></div>
      </div>
    </div>
    <div class="content-body">
      <div class="content-title">
        <b>Danh sách nhân viên</b>
        <span>
          <Button
            v-if="isShowDeleteButton"
            buttonId="btn-delete"
            buttonClass="button button-icon"
            iClass="fa-trash-alt"
            buttonText="Xóa nhân viên"
            @btnClick="btnDeleteOnClick"
          />
          <Button
            buttonId="btn-add"
            buttonClass="button button-icon"
            iClass="fa-user-plus"
            buttonText="Thêm nhân viên"
            @btnClick="btnAddOnClick(false)"
          />
        </span>
      </div>
      <div class="content-toolbar">
        <div class="toolbar-left">
          <Input
            inputClass="input-icon input-search autofocus "
            inputType="text"
            inputPlacehoder="Tìm kiếm theo mã, tên hoặc số điện thoại"
          />
          <!-- Dropdown Position 1  -->
          <Dropdown
            style="width: 213px"
            defaultName="Tất cả vị trí"
            dropdownClass="dropdown form-dropdown"
            dropdownId="employee__position"
            dropdownItemList="dropdown-list-item"
            dropdownItemValueId="PositionId"
            dropdownItemValueName="PositionName"
            myUrl="v1/Positions"
            selectedId="default"
          />
          <!-- Dropdown Position 1  -->
          <!-- Dropdown Department 1  -->
          <Dropdown
            style="width: 213px"
            defaultName="Tất cả phòng ban"
            dropdownClass="dropdown form-dropdown"
            dropdownId="employee__department_1"
            dropdownItemList="dropdown-list-item"
            tabindex="11"
            dropdownItemValueId="DepartmentId"
            dropdownItemValueName="DepartmentName"
            myUrl="api/Department"
            selectedId="default"
          />
          <!-- Dropdown Department 1  -->
        </div>

        <div class="toolbar-right">
          <Button
            buttonId="btn-refresh"
            buttonClass="button button-refresh"
            iClass="fa-sync-alt"
            @btnClick="btnRefreshOnClick"
          />
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
      <Paging
        :totalRecord="totalRecord"
        :pagingSize="pagingSize"
        :currentPage="currentPage"
        @changeCurrentPage="changeCurrentPage"
        @changePagingSize="changePagingSize"
      />
    </div>
    <EmployeeDetail
      v-bind:isHide="isHideDialogDetail"
      v-bind:employeeId="employeeId"
      :mode="mode"
      @btnAddOnClick="btnAddOnClick"
      :isReOpenForm="isReOpenForm"
      
    />  
  </div>
</template>

<script>
import EmployeeDetail from "@/components/view/employee/EmployeeDetail";
import Dropdown from "../../base/BaseDropdown.vue";
import Button from "../../base/BaseButton.vue";
import Input from "../../base/BaseInput.vue";
import Paging from "../../layout/TheContentPaging.vue";

import axios from "axios";
import { eventBus1 } from "../../../main";
import { eventBus2 } from "../../../main";

export default {
  name: "EmployeeList",
  /**
   * Các component được sử dụng 
   */
  components: {
    EmployeeDetail,
    Dropdown,
    Button,
    Input,
    Paging,
  },
  /**
   * Dữ liệu khởi tạo 
   * Created : Phạm Tuấn Dũng (13/08/2021)
   */
  data() {
    return {
      employee: {}, // Object employee dùng để lưu dữ liệu thông tin 1 nhân viên 
      employees: [], // Mảng employees dùng để lưu danh sách nhiều nhân viên
      checkedBoxs: [], // checkedBoxs mảng checkboxs để lưu các id của nhân viện được lựa chọn 
      employeeId: "", // mã nhân viên 
      isHideDialogDetail: true, // Trang thái đóng Form
      isShowDeleteButton: false, // Trạng thái mở Form
      mode: 0, // 2 chế độ 0 : thêm mới, 1 : update
      isReOpenForm: false, // 
      
      totalRecord: 180, // Tổng số bản ghi khởi tạo
      pagingSize: 10,  // Tổng số nhân viên trên 1 bản ghi. Khởi tạo giá trị bắt đầu bằng 10 
      currentPage: 1, // Bản ghi hiện tại

    };
  },
  /**
   * @Created : Các hàm khởi tạo 
   * @Author : Phạm Tuấn Dũng (13/08/2021)
   */
  created() {
    var vm = this;
    axios
      .get("http://cukcuk.manhnv.net/v1/Employees")
      .then((res) => {
        // console.log(res.data);
        vm.employees = res.data;
      })
      .catch((res) => {
        console.log(res);
      });
    // EventBus gọi hàm loadData (Bên lắng nghe)
    eventBus2.$on("loadData", () => {
      this.loadData();
      console.log("loading");
    });
    // EventBus gọi hàm deletaData (Bên lắng nghe)
    eventBus2.$on("deleteData", () => {
      this.deleteData();
    });
    // EventBus gọi hàm đóng Form (Bên lắng nghe)
    eventBus2.$on("confirmCloseAddForm", () => {
      this.isHideDialogDetail = true;
    });
  },
  props: {
    isCollapseMenu: Boolean,
  },
  methods: {
    /** -----------------------------------------------------------------------
     * @Event : Hiển thị form chi tiết khi nhấn button thêm nhân viên
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    btnAddOnClick(isHide) {
      this.isHideDialogDetail = isHide;
      this.mode = 0; // Khi nhấn vào cập nhật trạng thái 0 - Thêm mới
      this.isReOpenForm = !this.isReOpenForm;
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
    /** ------------------------------------------------------------------------
     * @Event : Hiển thị form chi tiết khi double vào một hàng của bảng dữ liệu.
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    rowOnDblClick(empId) {
      // alert(employeeId);
      this.isHideDialogDetail = false;
      this.employeeId = empId;
      this.mode = 1; // Khi nhấn vào cập nhật trạng thái 1 - Sửa
      this.isReOpenForm = !this.isReOpenForm;
    },
    /** -----------------------------------------------------------------------
     * @Event : Xóa nhân viên khi click vào nút delete
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    btnDeleteOnClick() {
      this.$emit("btnDeleteOnClick");
    },
    deleteData() {
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
    /** -----------------------------------------------------------------------
     * @Event : Sự kiện click vào nút Refresh để load dữ liệu
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    btnRefreshOnClick() {
      this.loadData();
    },
    /** -----------------------------------------------------------------------
     * @Function : Hàm loaddata
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    loadData() {
      var vm = this;
      // console.log(this);
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees")
        .then((res) => {
          vm.employees = res.data;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    /** -----------------------------------------------------------------------
     * @Function : Tick vào checkbox ứng với mã nhân viên tương ứng. Mục đich : Xóa nhân viên theo id
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    selectcheckBox(id) {
      if (this.checkedBoxs.includes(id)) {
        this.checkedBoxs = this.checkedBoxs.filter((e) => e !== id);
        if (this.checkedBoxs.length == 0) {
          this.hideDeleteButton();
        }
      } else {
        this.checkedBoxs.push(id);
        this.showDeleteButton();
      }
    },
    /** -----------------------------------------------------------------------
     * @evetn : Sự kiện ẩn / hiện nút Delete tương ứng khi tick vào checkbox 
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
     */
    showDeleteButton() {
      this.isShowDeleteButton = true;
    },
    hideDeleteButton() {
      this.isShowDeleteButton = false;
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
    /** -----------------------------------------------------------------------
     * @Format : Format dữ liệu ngày sinh thành dạng yyyy-mm-dd .Mục đích : Để trả về đúng định dạnh khi hiển thị trong Form
     * @Author : Author : Phạm Tuấn Dũng
     * @Date : 31/07/2021
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
    //
    changeCurrentPage(newCurrentPage){
       this.currentPage = newCurrentPage;
    },
    changePagingSize(newPagingSize) {
      this.pagingSize = newPagingSize;
    }
  },
};
</script>

<style scoped>
.collapse-content {
  width: calc(100% -54px);
}
</style>