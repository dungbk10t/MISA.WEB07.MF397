class HandleDataJS {
    constructor() {
        this.dataUrl = "";
        this.loadData();
    }
    loadData() {
        try {
            // lấy thông tin các cột dữ liệu
            var employees = null;
            var data = null;
            $.ajax({
                method: "GET",
                url: "http://cukcuk.manhnv.net/v1/Employees/",
                data: null,
                async: false,
                contentType: "application/json",
            })
                .done(function (res) {
                    employees = res;
                })
                .fail(function (res) {
                    alert("Không thể lấy dữ liệu từ API");
                });
            console.log(employees);
            data = employees;
            $.each(data, function (employee) {
                var tableHTML = `<tr id="${employee.EmployeeId}" >
                                    <td>${employee.EmployeeCode}</td>  
                                    <td>${employee.FullName}</td>
                                    <td>${formatDate(employee.DateOfBirth)}</td>
                                    <td>${employee.GenderName}</td>
                                    <td>${employee.PhoneNumber}</td>
                                    <td class="email">${employee.Email}</td>
                                    <td>${employee.PositionName}</td>
                                    <td>${employee.DepartmentName}</td>
                                    <td class="price">${fomatNumber(
                    employee.Salary
                )}</td>
                                    <td>${formatWorkStatus(
                    employee.WorkStatus
                )}</td>
                         </tr>`;
                $(".tb-body tbody").append(tableHTML);
                var num = Number(employee.EmployeeCode.slice(2));
                if (num > max) {
                    max = num;
                }
            });
            max += 1;
        } catch (e) { 

        }
    }
}
