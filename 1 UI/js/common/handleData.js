class HandleDataJS {
    constructor() {
        this.loadData();
    }
    loadData() {
        try {
            $('.tb-body tbody').empty();
            formMode = 0;
            // CALL API TO GET DATA
            $.ajax({
                method: "GET",
                url: "http://cukcuk.manhnv.net/v1/Employees/",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
            }).done(res => {
                var data = res;
                console.log(data);
                data.forEach((employee) => {
                    const e__eid = employee.EmployeeId;
                    var e__code = formatDataAPI(employee.EmployeeCode);
                    var e__name = formatDataAPI(employee.FullName);
                    var e__gName = formatDataAPI(employee.GenderName);
                    var e__dob = formatDate(employee.DateOfBirth);
                    var e__phone = formatDataAPI(employee.PhoneNumber);
                    var e__email = formatDataAPI(employee.Email);
                    var e__pos = formatDataAPI(employee.PositionName);
                    var e__depart = formatDataAPI(employee.DepartmentName);
                    var e__salary = fomatSalary(employee.Salary);
                    var e__wstatus = formatworkStatus(employee.WorkStatus);
                    
                    var trHTML = `<tr employeeId="${e__eid}">
                                        <td>${e__code}</td>  
                                        <td>${e__name}</td>
                                        <td>${e__dob}</td>
                                        <td>${e__gName}</td>
                                        <td>${e__phone}</td>
                                        <td class="email">${e__email}</td>
                                        <td>${e__pos}</td>
                                        <td>${e__depart}</td>
                                        <td class="price">${e__salary}</td>
                                        <td>${e__wstatus}</td>
                                </tr>`;
                    $(".tb-body tbody").append(trHTML);
                });
            }).fail(res => {
                switch(res.status) {
                    case 500:
                        alert("Có lỗi xảy ra vui lòng liên hệ với MISA!!");
                        break;
                    case 400:
                        alert("Lấy dữ liệu thất bại !!");
                }
            });
        } catch (error) {
            alert("Có lỗi xảy ra");
        }
    }
}
