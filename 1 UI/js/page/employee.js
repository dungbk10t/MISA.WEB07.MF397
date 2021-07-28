var formMode = 0; // 1: Sửa dữ liệu - 0: Thêm mới dữ liệu
var eID = ""; // Nhận id employee   
var data;
$(document).ready(function () {
    $('.dialog').css("visibility", "hidden");
    $('.dialog-delete').hide();
    $('.dialog-edit-delete').hide();
    $('.dialog-delete strong span').remove();
    $('.dialog-edit-delete .title span').remove();
    new EmployeeJS();
})

class EmployeeJS extends HandleDataJS {
    constructor() {
        super();
        this.setEvent();
        this.loadDropdown();
    }
    setDataUrl() {
        this.dataUrl = "http://cukcuk.manhnv.net/v1/Employees";
    }
    loadDropdown() {
        //bind dữ liệu lên các dropdown
        // 1. Dropdown position form
        loadPositionData("employee__position");
        // 2. Dropdown department form
        loadDepartmentData("employee__department");
        // 3. Dropdown position table
        loadPositionData("employee__position_1");
        // 4. Dropdown  position table
        loadDepartmentData("employee__department_1");
    }
    setEvent() {
        //1. Sự kiện khi click thêm mới
        $('#btn-addUser').click(btnAddOnClick);
        //2. Close form
        $('#btn-close').click(btnCloseOnclick);
        $('#btn-close1').click(btnCloseOnclick);
        $('#btn-close2').click(btnCloseOnclick);
        // //3. Save form
        $('#btn-save').click(btnSaveOnClick);
        //4. Cancel Form'
        $('#btn-cancel').click(btnCancelOnClick);
        //5. Refresh
        $('#btn-refresh').click(btnRefreshOnClick);
        // 7. Edit Form
        $('#btn-edit').click(btnEditOnClick);
        //8. Click vao 1 hang
        $('.tb-body tr').dblclick(tableOneRowOnDbClick);
        // $('.grid table').on('dblclick', 'tbody tr', tableOneRowOnDbClick);
        $('#btn-delete1').click(btnChooseDelete);
        $('#btn-delete2').click(btnOnClickDelete);
    }

}
//1. Open
function btnAddOnClick(e) {
    flagAdd = 1;
    $('.dialog').css("visibility", "visible");
    $('.dialog input').val(null);
    $('.dialog input').removeClass('border-red');
    $('.autofocus').focus();
    //lấy mã nhân viên mới binding vào form
    $.ajax({
        url: "http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode",
        method: 'GET',
    }).done(res => {
        $('#employee__code').val(res);
    }).fail(res => {
    });

}
//2. Close
function btnCloseOnclick(e) {
    $('.dialog').css("visibility", "hidden");
    $('.dialog-delete').hide();
    $('.dialog-edit-delete').hide();
    $('.dialog-delete strong span').remove();
    $('.dialog-edit-delete .title span').remove();
}
// 3. Save
function btnSaveOnClick(e) {
    try {
        var e__code = $('#employee__code').val();
        var e__name = $('#employee__fullname').val();
        var e__dob = $('#employee__dob').val();
        var e__gender = $('#employee__gender').attr('value');
        var e__idnum = $('#employee__idnumber').val();
        var e__iddate = $('#employee__iddate').val();
        var e__idplace = $('#employee__idplace').val();
        var e__email = $('#employee__email').val();
        var e__phone = $('#employee__phone').val();
        var e__pos = $('#employee__position').attr('value');
        var e__depart = $('#employee__department').attr('value');
        var e__tax = $('#employee__taxcode').val();
        var e__salary = $('#employee__basesalary').val();
        var e__jdate = $('#employee__joindate').val();
        var e__wstatus = $('#employee__workstatus').attr('value');

        if (validateEmail(e__email) && $.isNumeric(e__phone.replaceAll('.', '')) && validateDob(e__dob)) {
            var newEmployee = {};
            newEmployee.EmployeeCode = e__code;
            newEmployee.FullName = e__name;
            newEmployee.DateOfBirth = e__dob;
            newEmployee.Gender = e__gender;
            newEmployee.IdentityNumber = e__idnum;
            newEmployee.IdentityDate = e__iddate;
            newEmployee.IdentityPlace = e__idplace;
            newEmployee.Email = e__email;
            newEmployee.PhoneNumber = e__phone;
            newEmployee.PositionId = e__pos;
            newEmployee.DepartmentId = e__depart;
            newEmployee.PersonalTaxCode = e__tax;
            newEmployee.Salary = e__salary;
            newEmployee.JoinDate = e__jdate;
            newEmployee.WorkStatus = e__wstatus;
            console.log(newEmployee);

            // CALL AJAX FROM API : POST DATA METHOD
            if (formMode == 1) {
                $.ajax({
                    url: `http://cukcuk.manhnv.net/v1/Employees/${eID}`,
                    method: "PUT",
                    data: JSON.stringify(newEmployee),
                    contentType: "application/json; charset=utf-8",
                }).done(res => {
                    alert("Sửa dữ liệu thành công !!");
                    loadData();
                }).fail(res => {
                    alert("Sửa dữ liệu thất bại !!");;
                })
            }
            if (formMode == 0) {
                $.ajax({
                    url: "http://cukcuk.manhnv.net/v1/Employees",
                    method: "POST",
                    data: JSON.stringify(newEmployee),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done((res) => {
                    alert('Thêm mới thành công');
                    loadData();
                }).fail(function (res) {
                    alert('Không thêm được');
                })
            }
        } else {
            alert("Kiểm tra lại dữ liệu !!");
        }
    } catch (error) {
        alert("Có lỗi !!");
        console.log(error);
    }

}

function loadData() {
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
            data = res;
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
                var trHTML = $(`<tr employeeId = "${e__eid}">
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
                            </tr>`);
                $(".tb-body tbody").append(trHTML);
            });
        }).fail(res => {
            switch (res.status) {
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

function btnCancelOnClick(e) {
    $('.dialog').css("visibility", "hidden");
    $('.dialog-delete').hide();
    $('.dialog-delete strong span').remove();
    $('.dialog-edit-delete .title span').remove();
    alert("CANCEL");
}

function btnRefreshOnClick(e) {
    loadData();
    alert("Load dự liệu thành công");
}
function btnEditOnClick(e) {
    // alert("Edit");
    formMode = 1;
    // $('.dialog input').removeClass('border-red');
    try {
        // CALL API TO GET DATA
        $.ajax({
            url: `http://cukcuk.manhnv.net/v1/Employees/${eID}`,
            method: "GET",
        }).done(res => {
            data = res;
            $('#employee__code').val(data.EmployeeCode);
            $('#employee__fullname').val(data.FullName);
            $('#employee__dob').val(formatDateToValue(data.DateOfBirth));
            $('#employee__idnumber').val(data.IdentityNumber);
            $('#employee__iddate').val(formatDateToValue(data.IdentityDate));
            $('#employee__idplace').val(data.IdentityPlace);
            $('#employee__taxcode').val(data.PersonalTaxCode);
            $('#employee__email').val(data.Email);
            $('#employee__phone').val(data.PhoneNumber);
            $('#employee__joindate').val(formatDateToValue(data.JoinDate));
            $('#employee__basesalary').val(fomatSalary(data.Salary));

            /* ******************************************** */

            $("#employee__gender .select").text(data.GenderName);
            var genderId = $("#employee__gender").find(`[value='${data.Gender}']`);
            var genderIdList = $("#employee__gender .dropdown-list .dropdown-list-item");
            $.each(genderIdList, function(index, item){
                $(item).removeClass("dropdown-item-check")
                $(item).children(".fa-check").css("opacity", "0");
            })
            genderId.toggleClass("dropdown-item-check");
            genderId.children(".fa-check").css("opacity", "1");

            //Binding vị trí lên form
            var positionId = $("#employee__position").find(`[value='${data.PositionId}']`);
            var positionIdList = $("#employee__position .dropdown-list .dropdown-list-item");
            $.each(positionIdList, function(index, item){
                $(item).removeClass("dropdown-item-check");
                $(item).children(".fa-check").css("opacity", "0");
            })
            positionId.toggleClass("dropdown-item-check");
            positionId.children(".fa-check").css("opacity", "1");
            $("#employee__position .select").text(positionId.text())

            //Binding phòng ban lên form
            var departmentId = $("#employee__department").find(`[value='${data.DepartmentId}']`);
            var departmentIdList = $("#employee__department .dropdown-list .dropdown-list-item");
            $.each(departmentIdList, function(index, item){
                $(item).removeClass("dropdown-item-check");
                $(item).children(".fa-check").css("opacity", "0");
            })
            departmentId.toggleClass("dropdown-item-check");
            departmentId.children(".fa-check").css("opacity", "1");
            $("#employee__department .select").text(departmentId.text());

            //Binding tính trạng công việc lên form
            $("#employee__workstatus .select").text(data.WorkStatus);
            var workStatusId = $("#employee__workstatus").find(`[value='${data.WorkStatus}']`);
            var workStatusIdList = $("#employee__workstatus .dropdown-list .dropdown-list-item");
            $.each(workStatusIdList, function(index, item){
                $(item).removeClass("dropdown-item-check");
                $(item).children(".fa-check").css("opacity", "0");
            })
            workStatusId.toggleClass("dropdown-item-check");
            workStatusId.children(".fa-check").css("opacity", "1");
            $("#employee__workstatus .select").text(workStatusId.text().trim());

           
        }).fail(res => {
            switch (res.status) {
                case 500:
                    alert("Có lỗi xảy ra vui lòng liên hệ với MISA!!");
                    break;
                case 400:
                    alert("Binding dữ liệu vào form thất bại !!");
            }
        })
        $('.autofocus').focus();
        $('.dialog').css("visibility", "visible");
    } catch (error) {
        alert("Có lỗi");
    }
}
function editFormEmployee() {
    // formMode = 1;
    try {
        //CALL API TO GET DATA
        eID = $(this).attr('employeeId');
        console.log(id);
        $.ajax({
            url: `http://cukcuk.manhnv.net/v1/Employees/${eID}`,
            method: "GET",
            data: null,
            async: false
        }).done(res => {
            data = res;
            console.log(data);
            $('#employee__code').val(data.EmployeeCode);
            $('#employee__fullname').val(data.FullName);
            $('#employee__dob').val(data.DateOfBirth);
            $('#employee__idnumber').val(data.IdentityNumber);
            $('#employee__iddate').val(data.IdentityPlace);
            $('#employee__email').val(data.Email);
            $('#employee__phone').val(data.PhoneNumber);
            $('#employee__basesalary').val(data.Salary);

            /* Binding data dropdown vao form*/
            
            // $("#employee__gender .select").text(res.GenderName);
            // matchItemDropdown(data,"employee__gender",Gender);
            // matchItemDropdown(data,"employee__position",PositionId);
            // matchItemDropdown(data,"employee__department",DepartmentId);
            // matchItemDropdown(data,"employee__workstatus",WorkStatus);
            // Binding giới tính lên form
            $("#employee__gender .select").text(data.GenderName);
            var genderId = $("#employee__gender").find(`[value='${data.Gender}']`);
            var genderIdList = $("#employee__gender .dropdown-list .dropdown-list-item");
            $.each(genderIdList, function(index, item){
                $(item).removeClass("dropdown-item-check")
                $(item).children(".fa-check").css("opacity", "0");
            })
            genderId.toggleClass("dropdown-item-check");
            genderId.children(".fa-check").css("opacity", "1");

            //Binding vị trí lên form
            var positionId = $("#employee__position").find(`[value='${data.PositionId}']`);
            var positionIdList = $("#employee__position .dropdown-list .dropdown-list-item");
            $.each(positionIdList, function(index, item){
                $(item).removeClass("dropdown-item-check");
                $(item).children(".fa-check").css("opacity", "0");
            })
            positionId.toggleClass("dropdown-item-check");
            positionId.children(".fa-check").css("opacity", "1");
            $("#employee__position .select").text(positionId.text())

            //Binding phòng ban lên form
            var departmentId = $("#employee__department").find(`[value='${data.DepartmentId}']`);
            var departmentIdList = $("#employee__department .dropdown-list .dropdown-list-item");
            $.each(departmentIdList, function(index, item){
                $(item).removeClass("dropdown-item-check");
                $(item).children(".fa-check").css("opacity", "0");
            })
            departmentId.toggleClass("dropdown-item-check");
            departmentId.children(".fa-check").css("opacity", "1");
            $("#employee__department .select").text(departmentId.text());

            //Binding tính trạng công việc lên form
            $("#employee__workstatus .select").text(data.WorkStatus);
            var workStatusId = $("#employee__workstatus").find(`[value='${data.WorkStatus}']`);
            var workStatusIdList = $("#employee__workstatus .dropdown-list .dropdown-list-item");
            $.each(workStatusIdList, function(index, item){
                $(item).removeClass("dropdown-item-check");
                $(item).children(".fa-check").css("opacity", "0");
            })
            workStatusId.toggleClass("dropdown-item-check");
            workStatusId.children(".fa-check").css("opacity", "1");
            $("#employee__workstatus .select").text(workStatusId.text().trim());

            
        }).fail(res => {
            switch (res.status) {
                case 500:
                    alert("Có lỗi xảy ra vui lòng liên hệ với MISA!!");
                    break;
                case 400:
                    alert("Binding dữ liệu vào form thất bại !!");
            }
        })
        $('.autofocus').focus();
        $('.dialog').css("visibility", "visible");
    } catch (error) {
        alert("Có lỗi");
        console.log(error);
    }
}

function tableOneRowOnDbClick(e) {
    try {
        formMode = 1;
        eID = $(this).attr("employeeId");
        $.ajax({
            method: "GET",
            url: "http://cukcuk.manhnv.net/v1/Employees/" + eID,
            data: null, // các biến dữ liệu được gửi lên server (ten_bien1:dữ liệu, ten_bien2:dữ liệu, ...)
            async: false, // chạy đồng bộ (=false), chạy bất đồng bộ(=true)
            contentType: "application/json" // kiểu nội dung dữ liệu được gửi lên server
        }).done(function (res) {
            data = res;
            console.log(data);
        }).fail(function (res) {
            alert("Không thể lấy dữ liệu từ Api");
        });
        $('.dialog-edit-delete .title').append(`<span>${data.EmployeeCode}</span>`)
        $('.dialog-edit-delete').show();
    } catch (error) {
        alert("Có lỗi");
        console.log(error);
    }
}

function loadPositionData(id) {
    try {
        $.ajax({
            url: "http://cukcuk.manhnv.net/v1/Positions",
            method: 'GET',
            async: false
        }).done(res => {
            $("#" + id + " .dropdown-list").empty();
            res.forEach(e => {
                const positionID = e["PositionId"];
                const positionName = e["PositionName"];
                console.log(positionName);
                console.log(positionID);
                let optionHTML = $(`<div class="dropdown-list-item pos-txt" value="${positionID}">
                                        <i class="fas fa-check"></i>
                                        <div class="dd-item-text">${positionName}</div>
                                    </div>`);
                $("#" + id + " .dropdown-list").append(optionHTML);
            });
        }).fail(res => {
            alert("Không load được vị trí!!");
        });
    } catch (error) {

    }
}

function loadDepartmentData(id) {
    try {
        $("#" + id + " .dropdown-list").empty();
        $.ajax({
            url: "http://cukcuk.manhnv.net/api/Department",
            method: 'GET',
            async: false
        }).done(res => {
            $("#" + id + " .dropdown-list").empty();
            res.forEach(e => {
                const departmentID = e["DepartmentId"];
                const departmentName = e["DepartmentName"];
                console.log(departmentID);
                console.log(departmentName);
                let optionHTML = $(`<div class="dropdown-list-item depart-txt" value="${departmentID}">
                                        <i class="fas fa-check"></i>
                                        <div class="dd-item-text">${departmentName}</div>
                                    </div>`);
                $("#" + id + " .dropdown-list").append(optionHTML);
            });
        }).fail(res => {
            alert("Không load được phòng ban!!");
        })
    } catch (error) {
        console.log(error);
    }
}

function btnChooseDelete() {
    $('.dialog-edit-delete').hide();
    $('.dialog-delete strong').append(`<span>${data.EmployeeCode}</span>`)
    $('.dialog-delete').show();
}
function btnOnClickDelete() {
    // Xoa voi 1 user
    try {
        $('.dialog-edit-delete .title span').remove();
        $('.dialog-delete strong span').remove();
        $('.dialog-delete').hide();

        $.ajax({
            method: "DELETE",
            url: `http://cukcuk.manhnv.net/v1/Employees/${eID}`,
        }).done(res => {
            alert("Xóa dữ liệu thành công !!");
        }).fail(res => {
            alert("Xóa dữ liệu thất bại !!");
        });
        loadData();
    } catch (error) {
        alert("Có lỗi");
    }
}
// ----> TABLE CODE ID <----
// id='employee__code'
// id='employee__fullname'
// id='employee__dob'
// id='employee__gender'
// id='employee__idnumber'
// id='employee__iddate'
// id='employee__idplace'
// id='employee__email'
// id='employee__phone'
// id='employee__position' 
// id='employee__department' 
// id='employee__taxcode'
// id='employee__basesalary'
// id='employee__joindate'
// id='employee__workstatus'