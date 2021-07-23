flagAdd = 1;
/* 
----> TABLE CODE ID <----
id='employee__code'
id='employee__fullname'
id='employee__dob'
id='employee__gender'
id='employee_idnumber'
id='employee__iddate'
id='employee__idplace'
id='employee__email'
id='employee__phone'
id='employee__position' 
id='employee__department' 
id='employee__taxcode'
id='employee__basesalary'
id='employee__joindate'
id='employee__workstatus'

----> HTML ATTRIBUTE OBJECT JSON CODE <----
EmployeeCode
FullName
DateOfBirth
Gender
GenderName
IdentityNumber
IdentityDate
IdentityPlace
PersonalTaxCode
PhoneNumber
PositionId
DepartmentId
Salary
WorkStatus
JoinDate 
*/

$(document).ready(function() {
    $('employee__dialog').hide();
    loadData();
    setEvent();
    $('.tb-body tr:even').addClass('even');
});

var max = 0;    // Biến lấy mã nhân viên lớn nhất
var status = 0; // Edit : Status = 1 ; Add new : Status = 0;
var id = "";    // Trả về id khi lấy ra 1 element row  của employee table khi click vào tr

function setEvent() {
    var em =""; // // Biến lấy mã 1 employee
    // 1. Sự kiện : Click vào Button để thểm user data
    $('#btn-addUser').click(function(){
        $('#employee__dialog #employee__code').val('NV'+ max);
        $('#employee__dialog').show();
        $('#employee__code').focus();
    });
    // 2. Sự kiện : Click vào Button để đóng Dailog
    $('#btn-close').click(function(){
        $('#employee__dialog').hide();
        $('.dialog-delete').hide();
        $('.dialog-edit-delete').hide();
        $('.dialog-delete strong span').remove();
        $('.dialog-edit-delete .title span').remove();
    });
    // 3. Sự kiện : Hủy điền form Dailog
    $('.btn-cancel').click(function(){
        $('#employee__dialog').hide();
        $('.dialog-delete').hide();
        $('.dialog-delete strong span').remove();
        $('.dialog-edit-delete .title span').remove();
    });
    // 4. Sự kiện : Click chọn 1 hàng trong bảng dữ liệu Employee
    $('.tb-body tr').dblclick(function(){
        id = $(this).attr('id');
        $.ajax({
            method: "GET", 
            url: "http://cukcuk.manhnv.net/v1/Employees/" + id, 
            data: null,                     // Các biến dữ liệu được gửi lên server (ten_bien1:dữ liệu, ten_bien2:dữ liệu, ...)
            async: false,                   // Chạy đồng bộ (=false), chạy bất đồng bộ(=true)
            contentType: "application/json" // Kiểu nội dung dữ liệu được gửi lên server
        }).done(function (response) {
            console.log(response);
            em = response;
        }).fail(function (response) {
            alert("Không thể lấy dữ liệu từ API !!");
        });
        $('.dialog-edit-delete .title').append(`<span>${em.EmployeeCode}</span>`)
        $('.dialog-edit-delete').show();
    });
    // 5. Sự kiện : Click vào xóa lần thứ nhất
    $('#delete1').click(function(){
        $('.dialog-edit-delete').hide();
        $('.dialog-delete strong').append(`<span>${em.EmployeeCode}</span>`)
        $('.dialog-delete').show();
    });
    // 6. Sự kiện : Click vào button xác nhận xóa hẳn
    // $('#delete2').click(function(){
    //     $('.dialog-edit-delete .title span').remove();
    //     $('.dialog-delete strong span').remove();
    //     $('.dialog-delete').hide();
    // });
    // 7. Sự kiện : Chỉnh sửa dữ liệu
    $('#btn-edit').click(function(){
        editEmployee();
        $('#employee__dialog').show();
    });
    // 8. Sự kiện : Đẩy dữ liệu lên API
    // $('#btn-Save').click(onClickButtonSave){
    //     var check = checkIsValidForm();
    //     if(check){
    //         onClickButtonSave();
    //     }
    // });
    $('#btn-Save').click(onClickButtonSave);
    // 9. Sư kiện : Xóa dữ liệu
    // $('#delete2').click(function(){
    //     $.ajax({
    //         method: "DELETE",
    //         url: "http://cukcuk.manhnv.net/v1/Employees/" + id,
    //         data: null,
    //         async: false
    //     }).done(function (response) {
    //        alert('Xóa dữ liệu thành công !');
    //     }).fail(function (response) {
    //         alert('Không xóa được dữ liệu !');
    //     }); 
    //     $('.dialog-delete').hide();
    //     location.reload();
    // });
}
// Đổ dữ liệu vào form và bảng

function loadData() {
    var data = getData();
    handleDataTableHTML(data);
}
 /**
 * 1. 
 * @Function : getData
 * @author : Phạm Tuấn Dũng
 * @date : 21/07/2021
 * @returns employess
 */
function getData() {
    var employees = null;
    $.ajax({
        method: "GET",
        url: "http://cukcuk.manhnv.net/v1/Employees/", 
        data: null,
        async: false,
        contentType: "application/json"
    }).done(function(res) {
        employees = res;
    }).fail(function (res) {
        alert("Không thể lấy dữ liệu từ API");
    })
    console.log(employees);
    return employees; 
}
// Xử lý bảng dữ liệu rồi thêm vào 
function handleDataTableHTML(data) {
    $.each(data, function(index, employee) {
        var tableHTML = `<tr id="${ employee.EmployeeId}" >
                            <td><input class="checkbox" type="checkbox" name="" id=""></td>
                            <td>${employee.EmployeeCode}</td>  
                            <td>${employee.FullName}</td>
                            <td>${formatDate(employee.DateOfBirth)}</td>
                            <td>${employee.GenderName}</td>
                            <td>${employee.PhoneNumber}</td>
                            <td class="email">${employee.Email}</td>
                            <td>${employee.PositionName}</td>
                            <td>${employee.DepartmentName}</td>
                            <td class="price">${fomatNumber(employee.Salary)}</td>
                            <td>${formatWorkStatus(employee.WorkStatus)}</td>
                         </tr>`;
        $('.tb-body tbody').append(tableHTML);
        var num = Number(employee.EmployeeCode.slice(2));
        if(num > max) {
            max = num
        }
    });
    max += 1;
}

/* ************************************ BEGIN : CÁC HÀM XỬ LÝ DỮ LIỆU ************************************ */

// Hàm định dạng lương
function fomatNumber(inputNumber) {
    inputNumber += '';
    x = inputNumber.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    return x1 + x2;
}
// Hàm định dạng ngày tháng năm
function formatDate(inputDate) {
    var now = new Date(inputDate);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day)+"/"+(month)+"/"+now.getFullYear();
    return today;
}
// console.log(formatDate('2000-12-11T00:00:00'));
// Hàm định dạng trạng thái công việc
function formatWorkStatus(inputWorkStatus){
    var workStatus
    switch (inputWorkStatus) {
        case 0:
            workStatus = "Đang thử việc";
            break;
        case 1:
            workStatus = "Đang làm việc";
            break;
        case 2:
            workStatus = "Đã nghỉ việc";
            break;
        case 3:
            workStatus = "Đã nghỉ hưu";
            break;
        default:
            workStatus = ""
            break;
    }
    return workStatus;
}
// Hàm kiểm tra 1 chuỗi có phải email hay không
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
  }

/* ************************************ END : CÁC HÀM XỬ LÝ DỮ LIỆU ************************************ */

/* ************************************ BEGIN : CÁC HÀM XỬ LÝ SỰ KIỆN ************************************ */


function onClickButtonSave(e) {
    var myUrl = "http://cukcuk.manhnv.net/v1/Employees";
    var method = 'POST';
    //console.log(flagAdd);
    //console.log(myEmployeeId);
    if (flagAdd != 1) {
        myUrl = "http://cukcuk.manhnv.net/v1/Employees/" + myEmployeeId;
        method = 'PUT';
    }
    console.log(myUrl);
    console.log(method);

    var email = $('#inputEmail').val();
    var salary = $('#inputSalary').val();
    if (isEmail(email) && $.isNumeric(salary)) {
        var employee = {};
        employee.EmployeeCode = $('#inputEmployeeCode').val();
        employee.FullName = $('#inputFullName').val();
        employee.DateOfBirth = $('#inputDateOfBirth').val();
        employee.Gender = ($('#inputGenderName').attr('value') == "Nam") ? 1 : 0;
        employee.IdentityNumber = $('#inputIdentityNumber').val();
        employee.IdentityDate = $('#inputIdentityDate').val();
        employee.IdentityPlace = $('#inputIdentityPlace').val();
        employee.Email = $('#inputEmail').val();
        employee.PhoneNumber = $('#inputPhoneNumber').val();
        employee.PositionId = $('#inputPositionName').attr('value');
        employee.DepartmentId = $('#inputDepartmentName').attr('value');
        employee.PersonalTaxCode = $('#inputPersonalTaxCode').val();
        employee.Salary = $('#inputSalary').val();
        employee.JoinDate = $('#inputJoinDate').val();
        employee.WorkStatus = ($('#inputWorkStatus').attr('value') == "Đang làm việc") ? 1 : 0;

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
        setTimeout(loadData, 2000);
        $('.dialog').css("visibility", "hidden");
    } else {
        alert("Vui lòng kiểm tra lại email và lương!");
    }

}

// Hàm xử lý sự kiện chỉnh sửa Form

function handleEventEditForm(){
    status = 1;  //set Status = 1 -> Edit Form
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
        $('#employee_idnumber').val(response.IdentityNumber);
        $('#employee__idplace').val(response.IdentityPlace);
        $('#employee__iddate').val(formatDate(response.IdentityDate));
        $('#employee__taxcode').val(response.PersonalTaxCode);
        $('#employee__email').val(response.Email);
        $('#employee__phone').val(response.PhoneNumber);
        $('#employee__position').val(response.PositionId);
        $('#employee__department').val(response.DepartmentId);
        $('#employee__basesalary').val(response.Salary);
        $('#workStatus').val(response.WorkStatus);
        $('#employee__joindate').val(formatDate(response.JoinDate));
    }).fail(function (response) {
        alert('Không lấy được!');
    }); 
    $('.dialog-edit-delete').hide();
}

// Hiểm kiểm tra Form hợp lệ

function checkIsValidForm(){
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    // Khởi biến isValid. Tác dung : Gán giá trị true/false để kiểm tra tính hợp lệ của atribute
    var isValid = true; 
    // Kiểm tra mã nhân viên : Trả về true : not null , trả về false : null
    if($('#employee__code').val()===""){
        $('#employee__code').attr('placeholder', "Nhập mã nhân viên !!");
        $('#employee__code').addClass('error');
        isValid = false;
    }
    // Kiểm tra số CMND : Trả về true : not null , trả về false : null
    if($('#employee_idnumber').val()==""){
        $('#employee_idnumber').attr('placeholder', "Bắt buộc");
        $('#employee_idnumber').addClass('error');
        isValid = false;
    }
    // Kiểm tra Email : Trả về true : not null , trả về false : null
    if($('#employee__email').val()==""){
        $('#employee__email').attr('placeholder', "Bắt buộc");
        $('#employee__email').addClass('error');
        isValid = false;
    }
    // Kiểm tra định dạng email : Trả về true : Đúng định dạng , trả về false : sai định dạng
    if(!regex.test($('#employee__email').val())){
        $('#employee__email').attr('placeholder', "Nhập sai định dạng email");
        $('#employee__email').addClass('error');
        $('#employee__email').val("")
        isValid = false;
    }
    // Kiểm tra họ và tên : Trả về true : not null , trả về false : null
    if($('#employee__fullname').val()==""){
        $('#employee__fullname').attr('placeholder', "Bắt buộc");
        $('#employee__fullname').addClass('error');
        isValid = false;
    }
    // Kiểm tra số điện thoại :  Trả về true : not null , trả về false : null
    if($('#employee__phone').val()==""){
        $('#employee__phone').attr('placeholder', "Bắt buộc");
        $('#employee__phone').addClass('error');
        isValid = false;
    }
    // Kiểm tra : 
    //    + Trả về True : Nếu các trường trong form hợp lệ.
    //    + Trả về False : Nếu một trong các trường trong form hợp lệ.
    if(!isValid){
        alert('Nhập lại form');
    }
    else {
        return true;
    }
    return false;
}
/* ************************************ END : CÁC HÀM XỬ LÝ SỰ KIỆN ************************************ */