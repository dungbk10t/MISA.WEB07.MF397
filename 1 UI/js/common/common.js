/* ************************************ BEGIN : CÁC HÀM XỬ LÝ DỮ LIỆU ************************************ */

/** 
 * Hàm định dạng Ngày/Tháng/Năm
 * @date : 23.07.2021
 * @param {*} inputNumber 
 * @returns Number
 * @author: tuandung
 */
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
/**
 * Hàm định dạng lại ngày : Ngày/Tháng/Năm
 * @date : 23.07.2021
 * @param {*} inputDate 
 * @returns dd/mm/yyyy
 * @author: tuandung
 */
function formatDate(inputDate) {
    var now = new Date(inputDate);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day)+"/"+(month)+"/"+now.getFullYear();
    return today;
}
/**
 * Hàm định dạng trạng thái công việc
 * @date : 23.07.2021
 * @param {*} inputWorkStatus 
 * @returns workStatus
 * @author: tuandung
 */
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
/**
 * Hàm định dạng trạng giới tính
 * @date : 24.07.2021
 * @param {*} inputWorkStatus 
 * @returns workStatus
 * @author: tuandung
 */
function formatGenderName(dataAPI) {
    var result;
    if(dataAPI == "Nam") {
        result = "Nam";
    }
    else if (dataAPI == "Nam") {
        result = "Nữ"
    }
    else if (dataAPI == "Không xác định") {
        result = "Không xác định"
    }
    else {
        result = "";
    }
    return result;
} 
function formatPositionName(dataAPI) {
    var result;
    if(dataAPI == "Giám đốc") {
        result = "Giám đốc";
    }
    else if(dataAPI == "Nhân viên") {
        result = "Nhân viên";
    }
    else if(dataAPI == "Phó phòng") {
        result = "Phó phòng";
    }
    else if(dataAPI == "Trưởng phòng") {
        result = "Trưởng phòng";
    }
    else {
        result = "";
    }
    return result;
}
function formatDepartmentName(dataAPI) {
    var result;
    if(dataAPI == "Phòng Marketting") {
        result = "Phòng Marketting";
    }
    else if(dataAPI == "Phòng đào tạo") {
        result = "Phòng đào tạo";
    }
    else if(dataAPI == "Phòng Nhân sự") {
        result = "Phòng Nhân sự";
    }
    else if(dataAPI == "Phòng Công nghệ") {
        result = "Phòng Công nghệ";
    }
    else {
        result = "";
    }
    return result;
}
/**
 * Hàm kiểm tra 1 chuỗi có phải email hay không
 * @date : 23.07.2021
 * @param {*} email 
 * @returns Valid email format
 * @author: tuandung
 */
function validateEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
  }

/* ************************************ END : CÁC HÀM XỬ LÝ DỮ LIỆU ************************************ */