//DROPDOWN 1
// var dropdown = document.querySelector(".dropdown");
var dropdownList = document.querySelector(".dropdown-list");
var dropdownValue = document.querySelector(".dropdown-value");

//DROPDOWN 2
// var dropdown2 = document.querySelector(".dropdown-2");
var dropdownList2 = document.querySelector(".dropdown-list-2");
var dropdownValue2 = document.querySelector(".dropdown-value-2");

// var iconDown = document.querySelector(".icon-down");
// var iconUp = document.querySelector(".icon-up");

var state = false;

var currVal = 0;

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

function renderDropdown(dropdownValue, dropdownList, dropdown1) {
    render();
    function render() {
        var dropdownListHTML = '';
        dropdownValue.innerText = dropdown1[currVal];
        for (var i = 0; i < dropdown1.length; i++) {
            if (i == currVal) {
                dropdownListHTML += `<li data-id=${i} class="dropdown-item dropdown-item--active"><i class="fas fa-check dropdown-item__icon"></i> ${dropdown1[i]} </li>`;
            } else {
                dropdownListHTML += `<li data-id=${i} class="dropdown-item"><i class="fas fa-check dropdown-item__icon"></i> ${dropdown1[i]} </li>`;
            }
        }
        dropdownList.innerHTML = dropdownListHTML;

        var items = dropdownList.querySelectorAll('.dropdown-item');

        items.forEach((item) => {
            item.addEventListener('click', () => {
                var dataId = item.getAttribute('data-id');
                this.currVal = dataId;
                render();
            });
        });
    }
}

renderDropdown(dropdownValue, dropdownList, dropdown1);

renderDropdown(dropdownValue2, dropdownList2, dropdown2);

// renderDropdown(dropdownValue3, dropdownList3, dropdownGender);

// CÁCH 1
// dropdown.addEventListener('click', function () {
//     if (state == false) {
//         dropdownList.style.display = "block";
//         iconDown.style.display = "none";
//         iconUp.style.display = "inline";
//         state = true;
//     } else {
//         dropdownList.style.display = "none";
//         iconDown.style.display = "inline";
//         iconUp.style.display = "none";
//         state = false;
//     }
// });


// CÁCH 2:
// dropdown.addEventListener('click', function () {
//     if (dropdownList.classList.contains('show')) {
//         dropdownList.classList.remove('show');
//         iconDown.classList.add('show');
//         iconUp.classList.remove('show');
//     } else {
//         dropdownList.classList.add('show');
//         iconDown.classList.remove('show');
//         iconUp.classList.add('show');
//     }
// });

// CÁCH 3: 
// dropdown.addEventListener('click', function () {
//     dropdownList.classList.toggle('show');
//     iconUp.classList.toggle('show');
//     iconDown.classList.toggle('show');
// });

// dropdown2.addEventListener('click', function () {
//     dropdownList2.classList.toggle('show');
//     iconUp.classList.toggle('show');
//     iconDown.classList.toggle('show');
// });

var dropdowns = document.querySelectorAll(".dropdown");

// dropdowns[0].addEventListener('click', function () {
//     dropdowns[0].querySelector('.dropdown-list').classList.toggle('show');
//     iconUp.classList.toggle('show');
//     iconDown.classList.toggle('show');
// })

// dropdowns[1].addEventListener('click', function () {
//     dropdowns[1].querySelector('.dropdown-list').classList.toggle('show');
//     iconUp.classList.toggle('show');
//     iconDown.classList.toggle('show');
// })

dropdowns.forEach((dropdown) => {
    dropdown.addEventListener('click', function () {
        dropdown.querySelector('.dropdown-list').classList.toggle('show');
        dropdown.querySelector('.icon-up').classList.toggle('show');
        dropdown.querySelector('.icon-down').classList.toggle('show');
    });
});

