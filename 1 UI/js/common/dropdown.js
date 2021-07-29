$(document).ready(()=>{
    showDropDown("employee__gender");
    showDropDown("employee__position");
    showDropDown("employee__department");
    showDropDown("employee__workstatus");
    showDropDown("employee__department_1");
    showDropDown("employee__position_1");
    if ($('.dropdown').attr('id') == 'employee__department_1') {
        filterByDepartment();
    } else if ($('.dropdown').attr('id') == 'employee__position_1') {
        filterByPosition();
    }
})

function showDropDown(id){
    let ddSelect = $("#"+id+" .dropdown-select")
    let ddList = $("#"+id+" .dropdown-list");
    let ddItem = $("#"+id+" .dropdown-list .dropdown-list-item");
    let select = $("#"+id+" .dropdown-select .select")
    ddSelect.click(()=>{
        ddList.toggleClass("active");
    })
    ddItem.each((index, item) => {
        $(item).click(()=>{
            ddItem.removeClass("dropdown-item-check")
            $(item).toggleClass("dropdown-item-check")
            $("#"+id+" .fa-check").css("opacity", "0")
            $(item).children(".fa-check").css("opacity", "1");
            ddList.toggleClass("active");
            select.text($(item).text());
        })
    });
}

/**
 * lọc nhân viên theo phòng ban
 * */
 function filterByDepartment() {
    if ($('#employee__department_1').attr('value') == '') {
        myEmployee.loadData();
    } else {
        $('.tb-body table tbody').empty();
        var id = $('#employee__department_1').attr('value');
        console.log(id);
        $.ajax({
            url: "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=20&pageNumber=1&employeeFilter=nv&departmentId=" + id,
            method: 'GET'
        }).done(function (res) {
            console.log(res);
            myEmployee.bindingData(res);
        }).fail(res => {
            ToolTipJS.showMes('warning', 'Lỗi rồi!');
            myEmployee.loadData();
        })
    }
}

/**
 * lọc nhân viên theo vị trí
 * */
function filterByPosition() {
    if ($('#employee__position_1').attr('value') == '') {
        myEmployee.loadData();
    } else {
        $('.grid table tbody').empty();
        var id = $('#employee__position_1').attr('value');
        $.ajax({
            url: "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=20&pageNumber=1&employeeFilter=nv&positionId=" + id,
            method: 'GET'
        }).done(function (res) {
            console.log(res);
            myEmployee.bindingData(res);
        }).fail(res => {
            ToolTipJS.showMes('warning', 'Lỗi rồi!');
            myEmployee.loadData();
        })
    }
}





