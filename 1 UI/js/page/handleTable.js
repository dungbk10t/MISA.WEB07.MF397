/**
 * 1. Select table element
 * 2. Call API get data
 * 3. Render table 
 */
var tableBodyEmployee = $('.tb-body tbody');
// console.log(tableBodyEmployee);
var tableEmployeeData;

$(document).ready(function() {
    $.ajax({
        url: 'http://cukcuk.manhnv.net/v1/Employees',
        method: 'GET'
    }).done(function(res) {
       tableEmployeeData = res;
       renderTable(tableEmployeeData);
       handleEventTableRow();
    }).fail(function(res) {
        console.log('failed');
    });
});
/**Sau khoảng 1(s) mới lấy dữ liệu -> sẽ không bị undefinded */
setTimeout(() => {
    // console.log(tableEmployeeData[5]);
}, 1000);


function renderTable(tableData) {
    var tableHTML = '';
    for (var i = 0; i < tableData.length; i++) {
        // console.log(tableData[i].FullName);
        tableHTML +=                        
                    `<tr>
                        <td><input class="checkbox" type="checkbox" name="" id=""></td>
                        <td>${tableData[i].EmployeeCode}</td>  
                        <td>${tableData[i].FullName}</td>
                        <td>${tableData[i].DateOfBirth}</td>
                        <td>${tableData[i].GenderName}</td>
                        <td>${tableData[i].PhoneNumber}</td>
                        <td class="email">${tableData[i].Email}</td>
                        <td>${tableData[i].PositionName}</td>
                        <td>${tableData[i].DepartmentName}</td>
                        <td class="price">${tableData[i].Salary}</td>
                        <td>${tableData[i].WorkStatus}</td>
                    </tr>`;
    }
    tableBodyEmployee.append(tableHTML);
    /**
     * Thêm vào trước : prepend
     * Thêm bình thường: append
     */
}
// RENDER POPUP (DETAIL INFORMATION FORM)
var employeeCode = $('#employee__code')[0];
var employeeFullName = $('#employee__fullname')[0];
var employeeDob = $('#employee__dob');
var employeeGender = $('#employee__gender');
var employeeIdNumber = $('#employee__idnumber');
var employeeIdDate = $('#employee__iddate');
var employeeIdPlace = $('#employee__idplace');
var employeeEmail = $('#employee__email');
var employeePhone = $('#employee__phone');
var employeePosition = $('#employee__position');
var employeeDepartment = $('#employee__department');
var employeeTaxCode = $('#employee__taxcode');
var employeeBaseSalary = $('#employee__basesalary');
var employeeJoinDate = $('#employee__joindate');
var employeeWorkStatus = $('#employee__workstatus');

// console.log( employeeCode);
// console.log(employeeFullName);
function handleEventTableRow() {
    var tableRows = $('.table-employee__row');
    console.log(tableRows);
}


