
var flagAdd = 1;
myEmployeeId = '';
var max = 0;    // Biến lấy mã nhân viên lớn nhất
var status = 0; // Edit : Status = 1 ; Add new : Status = 0;
var id = "";    // Trả về id khi lấy ra 1 element row  của employee table khi click vào tr
var employee=""; // Biến lấy mã id employee
$(document).ready(function () {
    new EmployeeJS();
})

class EmployeeJS extends HandleDataJS {
    constructor() {
        super();
        this.setEvent();
        // this.loadDropdown();
    }
    setDataUrl() {
        this.dataUrl = "http://cukcuk.manhnv.net/v1/Employees";
    }
    // loadDropdown() {
    //     //bind dữ liệu lên các dropdown
    //     //1.Dropdown phòng ban
    //     // loadDropdownData("Department");
    //     //2.Dropdown vị trí
    //     // loadDropdownData("Position");
    // }
    setEvent() {
        //1. Sự kiện khi click thêm mới
        $('#btn-addUser').click(btnAddOnClick);
        //2. Close form
        $('#btn-close').click(btnCloseOnclick);
        $('.close').click(btnCloseOnclick);
        // //3. Save form
        $('#btn-save').click(btnSaveOnClick);
        //4. Cancel Form'
        $('#btn-cancel').click(btnCancelOnClick);
        //5. Refresh
        $('#btn-refresh').click(btnRefreshOnClick);
        //7. Edit Form
        $('#btn-edit').click(btnEditOnClick);
        //8. Click vao 1 hang
        // $('.table-employee__list tr').dblclick(tableOneRowOnDbClick);
        $('.grid table').on('dblclick', 'tbody tr', tableOneRowOnDbClick);
        //
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
    var myUrl = "http://cukcuk.manhnv.net/v1/Employees";
    var method = 'POST';
    if (flagAdd != 1) {
        myUrl = "http://cukcuk.manhnv.net/v1/Employees/" + myEmployeeId;
        method = 'PUT';
    }
    console.log(myUrl);
    console.log(method);

    var email = $('#employee__email').val();
    var salary = $('#employee__basesalary').val();
    if (validateEmail(email) && $.isNumeric(salary)) {
        var employee = {};
        employee.EmployeeCode = $('#employee__code').val();
        employee.FullName = $('#employee__fullname').val();
        employee.DateOfBirth = $('#employee__dob').val();
        employee.Gender = ($('#employee__gender').attr('value') == "Nam") ? 1 : 0;
        employee.IdentityNumber = $('#employee__idnumber').val();
        employee.IdentityDate = $('#employee__iddate').val();
        employee.IdentityPlace = $('#employee__idplace').val();
        employee.Email = $('#employee__email').val();
        employee.PhoneNumber = $('#employee__phone').val();
        employee.PositionId = $('#employee__position').attr('value');
        employee.DepartmentId = $('#employee__department').attr('value');
        employee.PersonalTaxCode = $('#employee__taxcode').val();
        employee.Salary = $('#employee__basesalary').val();
        employee.JoinDate = $('#employee__joindate').val();
        employee.WorkStatus = ($('#employee__workstatus').attr('value') == "Đang làm việc") ? 1 : 0;
        console.log(employee);

        // gọi ajax post dữ liệu
        $.ajax({
            url: myUrl,
            method: method,
            data: JSON.stringify(employee),
            dataType: 'json',
            contentType: 'application/json'
        }).done(res => {
            if (flagAdd == 1) {
                alert('Thêm mới thành công!');
            } else {
                alert('Cập nhật thông tin thành công!');
            }
        }).fail(res => {
            alert("Đã có lỗi xảy ra!");
        });

        // Ẩn dialog đi và load lại dữ liệu
        setTimeout(location.reload(), 2000);
        $('.dialog').css("visibility", "hidden");
    } else {
        alert("Vui lòng kiểm tra lại email và lương!");
    }
}

function btnCancelOnClick(e) {
    $('.dialog').css("visibility", "hidden");
    $('.dialog-delete').hide();
    $('.dialog-delete strong span').remove();
    $('.dialog-edit-delete .title span').remove();
}

function btnRefreshOnClick(e) {
    location.reload();
}
function btnEditOnClick(e) {
    editEmployee();
    $('.dialog').css("visibility", "hidden");
}
function editEmployee(){
    status = 1;
    $.ajax({
        method: "GET",
        url: "http://cukcuk.manhnv.net/v1/Employees/" + id,
        data: null,
        async: false
    }).done(function (response) {
        $('#employee__code').val(response.EmployeeCode);
        $('#employee__fullname').val(response.FullName);
        $('#employee__dob').val(formatDate(response.DateOfBirth));
        $('#employee__gender').val(response.Gender);
        $('#employee__idnumber').val(response.IdentityNumber);
        $('#employee__idplace').val(response.IdentityPlace);
        $('#employee__iddate').val(formatDate(response.IdentityDate));
        $('#employee__taxcode').val(response.PersonalTaxCode);
        $('#employee__email').val(response.Email);
        $('#employee__phone').val(response.PhoneNumber);
        $('#employee__position').val(response.PositionId);
        $('#employee__department').val(response.DepartmentId);
        $('#employee__basesalary').val(response.Salary);
        $('#employee__workstatus').val(response.WorkStatus);
        $('#employee__joindate').val(formatDate(response.JoinDate));
    }).fail(function (response) {
        alert('Không lấy được!');
    }); 
    console.log()
    $('.dialog-edit-delete').hide();
}

function tableOneRowOnDbClick(e) {
    flagAdd = 0;
    
    id = $(this).attr('id');
    $.ajax({
        method: "GET", 
        url: "http://cukcuk.manhnv.net/v1/Employees/" + id, 
        data: null, // các biến dữ liệu được gửi lên server (ten_bien1:dữ liệu, ten_bien2:dữ liệu, ...)
        async: false, // chạy đồng bộ (=false), chạy bất đồng bộ(=true)
        contentType: "application/json" // kiểu nội dung dữ liệu được gửi lên server
    }).done(function (response) {
        employee = response;
        console.log(employee);
    }).fail(function (response) {
        alert("Không thể lấy dữ liệu từ Api");
    });
    $('.dialog-edit-delete .title').append(`<span>${employee.EmployeeCode}</span>`)
    $('.dialog-edit-delete').show();
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
