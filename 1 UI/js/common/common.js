/* ************************************ BEGIN : CÁC HÀM XỬ LÝ DỮ LIỆU ************************************ */

/** 
 * Hàm định dạng Ngày/Tháng/Năm
 * @date : 23.07.2021
 * @param {*} inputNumber 
 * @returns Number
 * @author: tuandung
 */
function fomatSalary(inputData) {
    if(inputData == null) {
        return '';
    }
    else {
        inputData += '';
        x = inputData.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + '.' + '$2');
        }
        return x1 + x2;
    }
}
/**
 * Hàm định dạng lại ngày : Ngày/Tháng/Năm
 * @date : 23.07.2021
 * @param {*} inputDate 
 * @returns dd/mm/yyyy
 * @author: tuandung
 */
function formatDate(inputDate) {
    if(inputDate == null) {
        return "";
    }
    else {
        var now = new Date(inputDate);
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var dateString = (day)+"/"+(month)+"/"+now.getFullYear();
        return dateString;
    }
}
/**
 * Hàm định dạng trạng thái công việc
 * @date : 23.07.2021
 * @param {*} inputwStatusStr 
 * @returns wStatusStr
 * @author: tuandung
 */
function formatworkStatus(inputwStatusStr){
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
            wStatusStr = ""
            break;
    }
    return wStatusStr;
}
/**
 * Hàm định dạng dữ liệu NULL
 * @date : 24.07.2021
 * @param {*} dataAPI
 * @returns "" <=> dataAPI == NULL , else return dataAPI
 * @author: tuandung
 */

function formatDataAPI(dataAPI) {
    return (dataAPI == null) ? "" : dataAPI;
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

function formatDateToValue(data) {
    if (data != null) {
        var date = new Date(data);
        var day = date.getDate();
        day = (day < 10) ? '0' + day : day;
        var month = date.getMonth() + 1;
        month = (month < 10) ? '0' + month : month;
        var year = date.getFullYear();
        return year+'-'+month+'-'+day;
    }
    else {
        return '';
    }
}

function matchItemDropdown(res, id, attr) {
    console.log(id);
	var itemId = $("#"+id).find(`[value='${res+"."+attr}']`);
	var itemList = $("#"+id+" .dropdown-list .dropdown-list-item");
	$.each(itemList, function(index, item) {
		$(item).removeClass("dropdown-item-check");
		$(item). $(item).children(".fa-check").css("opacity", "0");
	});
	itemId.toggleClass("dropdown-item-check")
       	itemId.children(".fa-check").css("opacity", "1")
    if(id == "employee__workstatus") {
        $("#"+id+" .select").text(itemId.text().trim());
    }
    else {
        $("#"+id+" .select").text(itemId.text());
    }
}

