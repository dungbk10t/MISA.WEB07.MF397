$(document).ready(()=>{
    showDropDown("employee__gender");
    showDropDown("employee__position");
    showDropDown("employee__department");
    showDropDown("employee__workstatus");
    showDropDown("employee__department_1");
    showDropDown("employee__position_1");
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
            select.text($(item).text())
        })
    });
}


