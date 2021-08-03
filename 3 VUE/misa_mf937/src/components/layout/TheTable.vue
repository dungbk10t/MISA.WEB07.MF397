<template>
     <div class="grid grid-employee" id="employee__table">
          <table width="100%" border="0">
            <thead>
              <tr>
                <th >#</th>
                <th fieldName="EmployeeCode">Mã nhân viên</th>
                <th fieldName="FullName">Họ và tên</th>
                <th fieldName="GenderName">Giới tính</th>
                <th fieldName="DateOfBirth" format="dmy">Ngày sinh</th>
                <th fieldName="PhoneNumber">Điện thoại</th>
                <th fieldName="Email">Email</th>
                <th fieldName="PositionName">Chức vụ</th>
                <th fieldName="DepartmentName">Phòng ban</th>
                <th fieldName="Salary" format="money" style="text-align: right;" >Mức lương cơ bản</th>
                <th fieldName="WorkStatus" format="workStatus">Tình trạng công việc</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(employee, index) in employees" :key="employee.EmployeeId">
                <td>{{index + 1}}</td>
                <td>{{employee.EmployeeCode}}</td>
                <td>{{employee.FullName}}</td>
                <td>{{employee.DateOfBirth}}</td>
                <td>{{employee.GenderName}}</td>
                <td>{{employee.PhoneNumber}}</td>
                <td>{{employee.Email}}</td>
                <td>{{employee.DepartmentName}}</td>
                <td>{{employee.PositionName}}</td>
                <td>{{employee.Salary}}</td>
                <td>{{employee.WorkStatus}}</td>
              </tr>
            </tbody>
          </table>
        </div>
</template>

<script>

import axios from "axios";
export default {
    name: 'TheTable',
    created(){
        var vm = this;
        axios.get("http://cukcuk.manhnv.net/v1/Employees").then(res=>{
            console.log(res.data);
            vm.employees= res.data;
        }).catch(res=>{
            console.log(res);
        })
    },
    data(){
        return{
            employees : []
        }
    }
}
</script>