$(document).ready(()=>{
    showDropDown("dd1");
    showDropDown("dd2");
    showDropDown("dd4");
    showDropDown("dd5");
    showDropDown("dd6");
    showDropDown("dd7");
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
            // console.log($(item).attr("value"))
            $("#"+id+" .fa-check").css("opacity", "0")
            $(item).children(".fa-check").css("opacity", "1");
            ddList.toggleClass("active");
            select.text($(item).text())
        })
    });
}


