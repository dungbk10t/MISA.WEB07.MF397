//DROPDOWN 1
// var dropdown = document.querySelector(".dropdown");
var dropdownList1 = document.querySelector(".dropdown-list");
var dropdownValue1 = document.querySelector(".dropdown-value");

//DROPDOWN 2
// var dropdown2 = document.querySelector(".dropdown-2");
var dropdownList2 = document.querySelector(".dropdown-list-2");
var dropdownValue2 = document.querySelector(".dropdown-value-2");


var state = false;

var currVal1 = 0;
var currVal2 = 0;

$(document).ready(function () {
    $.ajax({
        url: 'http://cukcuk.manhnv.net/api/Department',
        method: 'GET'
    }).done(function (res) {
        renderDropdownAPI(dropdownValue1, dropdownList1, dropdown1, res, currVal1, "DepartmentName");

    }).fail(function (res) {
        console.log('fail to get department data')
    });

    // $.ajax({
    //     url: 'http://cukcuk.manhnv.net/v1/Positions',
    //     method: 'GET'
    // }).done(function (res) {
    //     console.log(res);
    //     renderDropdownAPI(filterValuePosition, filterListPosition, currValPosition, res, "PositionName");
    // }).fail(function (res) {
    //     console.log('fail to get position data');
    // });
})

var dropdown1, dropdown2;
dropdown1 = [
    "Tất cả phòng ban",
    "Phòng đào tạo",
    "Phòng công nghệ",
    "Phòng marketing",
    "Phòng nhân sự"
]
dropdown2 = [
    "Tất cả vị trí",
    "Giám đốc",
    "Thu ngân",
    "Nhân viên marketing"
]



function renderDropdown(dropdownValue, dropdownList, dropdownData, currVal) {
    render();
    function render() {
        var dropdownListHTML = '';
        dropdownValue.innerText = dropdownData[currVal];
        for (var i = 0; i < dropdownData.length; i++) {
            if (i != currVal) {
                dropdownListHTML += `<li data-id=${i} class="dropdown-item dropdown-item--active"><i class="fas fa-check dropdown-item__icon"></i> ${dropdownData[i]} </li>`;
            } else {
                dropdownListHTML += `<li data-id=${i} class="dropdown-item"><i class="fas fa-check dropdown-item__icon"></i> ${dropdownData[i]} </li>`;
            }
        }
        dropdownList.innerHTML = dropdownListHTML;

        var items = dropdownList.querySelectorAll('.dropdown-item');

        items.forEach((item) => {
            item.addEventListener('click', () => {
                var dataId = item.getAttribute('data-id');
                currVal = dataId;
                render();
            });
        });
    }
}
function renderDropdownAPI(dropdownValue, dropdownList, dropdownData, currVal, type) {
    renderAPI();
    function renderAPI() {
        var dropdownListHTML = '';
        dropdownValue.innerText = dropdownData[currVal];
        console.log(currVal[0]);
        console.log(dropdownData[currVal]);
        console.log(dropdownData)
        for (var i = 0; i < dropdownData.length; i++) {
            if (i != currVal) {
                dropdownListHTML += `<li data-id=${i} class="dropdown-item dropdown-item--active"><i class="fas fa-check dropdown-item__icon"></i> ${dropdownData[i]} </li>`;
            } else {
                dropdownListHTML += `<li data-id=${i} class="dropdown-item"><i class="fas fa-check dropdown-item__icon"></i> ${dropdownData[i]} </li>`;
            }
        }
        dropdownList.innerHTML = dropdownListHTML;

        var items = dropdownList.querySelectorAll('.dropdown-item');

        items.forEach((item) => {
            item.addEventListener('click', () => {
                var dataId = item.getAttribute('data-id');
                currVal = dataId;
                renderAPI();
            });
        });
    }
}

renderDropdown(dropdownValue1, dropdownList1, dropdown1, currVal1);

renderDropdown(dropdownValue2, dropdownList2, dropdown2, currVal2);



var dropdowns = document.querySelectorAll(".dropdown");

dropdowns.forEach((dropdown) => {
    dropdown.addEventListener('click', function () {
        dropdown.querySelector('.dropdown-list').classList.toggle('show');
        dropdown.querySelector('.icon-up').classList.toggle('show');
        dropdown.querySelector('.icon-down').classList.toggle('show');
    });
});