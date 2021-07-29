<template>
  <div class="content-body">
    <div class="content-title">
      <b>Danh sách nhân viên</b>
      <span>
        <button id="btn-delete" class="button button-icon">
          <i class="fas fa-trash-alt"></i>
          <p>Xóa nhân viên</p>
        </button>
        <button id="btn-add" class="button button-icon">
          <i class="fas fa-user-plus"></i>
          <p>Thêm nhân viên</p>
        </button>
      </span>
    </div>
    <div class="content-toolbar">
      <div class="toolbar-left">
        <input
          class="input-icon input-search"
          type="text"
          placeholder="Tìm kiếm theo mã, tên hoặc số điện thoại"
        />
        <!-- Dropdown Position 1  -->
        <div
          class="dropdown form-dropdown dropdown-item-check"
          id="employee__position_1"
          value=""
        >
          <div class="dropdown-list"></div>
          <div class="dropdown-select">
            <div class="select">Chọn vị trí</div>
            <i class="fas fa-angle-down"></i>
          </div>
        </div>
        <!-- Dropdown Position 1  -->
        <!-- Dropdown Department 1  -->
        <div
          class="dropdown form-dropdown"
          id="employee__department_1"
          value=""
        >
          <div class="dropdown-list"></div>
          <div class="dropdown-select">
            <div class="select">Chọn phòng ban</div>
            <i class="fas fa-angle-down"></i>
          </div>
        </div>
        <!-- Dropdown Department 1  -->
      </div>

      <div class="toolbar-right">
        <button id="btn-refresh" class="button button-refresh">
          <i class="fas fa-sync-alt"></i>
        </button>
      </div>
    </div>
    <div class="grid grid-employee" id="employee__table">
      <table class="tb-head" border="0">
        <thead>
          <tr>
            <!-- <th></th> -->
            <th>#</th>
            <th fieldName="EmployeeCode">Mã nhân viên</th>
            <th fieldName="FullName">Họ và tên</th>
            <th fieldName="GenderName">Giới tính</th>
            <th fieldName="DateOfBirth" format="dmy">Ngày sinh</th>
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
      </table>
      <table class="tb-body" border="0">
        <tbody>
          <tr>
            <td>NV-512340</td>
            <td>Phạm Tuấn Dũng</td>
            <td>18/02/1998</td>
            <td>Nam</td>
            <td>0865004185</td>
            <td class="email">dungbk10t@gmail.com</td>
            <td>Nhân viên</td>
            <td>Phòng Nhân sự</td>
            <td class="price">10.000.000</td>
            <td>Đang thử việc</td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="content-paging">
      <div class="paging-left">
        <p>Hiển thị 1-10/1000 nhân viên</p>
      </div>
      <div class="paging-center">
        <button class="btn-inline">
          <i class="fas fa-angle-double-left"></i>
        </button>
        <button class="btn-inline">
          <i class="fas fa-angle-left"></i>
        </button>
        <button class="circle">1</button>
        <button class="circle">2</button>
        <button class="circle">3</button>
        <button class="circle">4</button>
        <button class="btn-inline">
          <i class="fas fa-angle-right"></i>
        </button>
        <button class="btn-inline">
          <i class="fas fa-angle-double-right"></i>
        </button>
      </div>
      <div class="paging-right">
        <p>10 nhân viên/trang</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
</style>