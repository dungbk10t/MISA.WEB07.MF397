// BEGIN DECLARE VARIABLES

// COMBOBOX SEX (GIOI TINH) 
var comboboxSex = document.querySelector('.combobox--sex');
var comboboxDropdownSex = document.querySelector('.combobox__dropdown--sex');
var comboboxListSex = document.querySelector('.combobox__list--sex');
var comboboxInputSex = document.querySelector('.combobox__input--sex');

// COMBOBOX POSITION
var comboboxPosition = document.querySelector('.combobox--position');
var comboboxDropdownPosition = document.querySelector('.combobox__dropdown--position');
var comboboxListPosition = document.querySelector('.combobox__list--position');
var comboboxInputPosition = document.querySelector('.combobox__input--position');

// COMBOBOX DEPARTMENT
var comboboxDepartment = document.querySelector('.combobox--department');
var comboboxDropdownDepartment = document.querySelector('.combobox__dropdown--department');
var comboboxListDepartment = document.querySelector('.combobox__list--department');
var comboboxInputDepartment = document.querySelector('.combobox__input--department');

// COMBOBOX JOBSTATUS
var comboboxJobstatus = document.querySelector('.combobox--jobstatus');
var comboboxDropdownJobstatus = document.querySelector('.combobox__dropdown--jobstatus');
var comboboxListJobstatus = document.querySelector('.combobox__list--jobstatus');
var comboboxInputJobstatus = document.querySelector('.combobox__input--jobstatus');

// STATUS
var statusComboboxList = false;

// DATA
var comboboxDataPosition = [
    'Giám đốc',
    'Thu ngân',
    'Nhân viên marketing'
];

var comboboxDataRestaurant = [
    'Nam',
    'Nữ',
    'Không xác định'
];

var comboboxDataDepartment = [
    'Phòng Công nghệ',
    'Phòng Đào tạo',
    'Phòng Marketing',
    'Phòng Công nghệ'
];

var comboboxDataJobstatus = [
    'Đang làm việc',
    'Đang thử việc',
    'Đã nghỉ hưu',
    'Đã nghỉ việc'
];

var currentValue = 0;


// END DECLARE VARIABLES


// BEGIN MAIN PROGRAM

renderCustomCombobox(comboboxInputSex, comboboxListSex, comboboxDataRestaurant, comboboxSex, comboboxDropdownSex);

renderCustomCombobox(comboboxInputPosition, comboboxListPosition, comboboxDataPosition, comboboxPosition, comboboxDropdownPosition);

renderCustomCombobox(comboboxInputDepartment, comboboxListDepartment, comboboxDataDepartment, comboboxDepartment, comboboxDropdownDepartment);

renderCustomCombobox(comboboxInputJobstatus, comboboxListJobstatus, comboboxDataJobstatus, comboboxJobstatus, comboboxDropdownJobstatus);

// END MAIN PROGRAM



// BEGIN FUNCTIONS

function renderCustomCombobox(comboboxInput, comboboxList, comboboxData, combobox, comboboxDropdown) {
    // RENDER COMBOBOX
    renderCombobox(comboboxInput, comboboxList, comboboxData);

    var comboboxInputCancelSub = combobox.querySelector('.combobox__input-cancel');


    // BEGIN HANDLE EVENTS
    comboboxList.addEventListener('click', function () {
        combobox.classList.remove('show');
    });

    comboboxInput.addEventListener('focus', function () {
        combobox.classList.remove('error');
        combobox.classList.add('show');
    });


    comboboxInput.addEventListener('input', function () {
        console.log('oninput');
        renderInput(comboboxInput, comboboxList, comboboxData);
    });

    comboboxInputCancelSub.addEventListener('click', function () {
        comboboxInput.value = '';
        comboboxInput.focus();
        renderInput(comboboxInput, comboboxList, comboboxData);
    });

    window.addEventListener('keydown', function (e) {
        if (e.code == "Enter") {
            displayDropdown(comboboxList, combobox, comboboxDropdown);
            comboboxInput.blur();
            resolveInputValue(comboboxInput, comboboxData, combobox, comboboxDropdown);
        }
    });

    comboboxDropdown.addEventListener('click', function () {
        displayDropdown(comboboxList, combobox, comboboxDropdown);
    });

    // FINISH HANDLE EVENTS
}

function renderCombobox(comboboxInput, comboboxList, comboboxData) {
    var comboboxListHTML = '';
    for (var i = 0; i < comboboxData.length; i++) {
        if (i == currentValue) {
            comboboxListHTML += `<li data-id=${i} class="combobox__item combobox__item--active"><i class="fas fa-check combobox__check"></i> ${comboboxData[i]}</li>`;
        } else {
            comboboxListHTML += `<li data-id=${i} class="combobox__item"><i class="fas fa-check combobox__check"></i> ${comboboxData[i]}</li>`;
        }
    }
    comboboxInput.value = comboboxData[currentValue];
    comboboxList.innerHTML = comboboxListHTML;

    var comboboxItems = comboboxList.querySelectorAll('li');

    comboboxItems.forEach(function (comboboxItem) {
        comboboxItem.addEventListener('click', function () {
            currentValue = comboboxItem.getAttribute('data-id');
            renderCombobox(comboboxInput, comboboxList, comboboxData);
        });
    });
}

// RENDER BY SEARCHING TEXT
function renderInput(comboboxInput, comboboxList, comboboxData) {
    var comboboxListHTML = '';
    var inputValue = comboboxInput.value;
    var inputValueLowercase = inputValue.toLowerCase().trim();

    for (var i = 0; i < comboboxData.length; i++) {
        var comboboxDataLowerCase = comboboxData[i].toLowerCase().trim();
        if (comboboxDataLowerCase.includes(inputValueLowercase)) {
            if (i == currentValue) {
                comboboxListHTML += `<li data-id=${i} class="combobox__item combobox__item--active"><i class="fas fa-check combobox__check"></i> ${comboboxData[i]}</li>`;
            } else {
                comboboxListHTML += `<li data-id=${i} class="combobox__item"><i class="fas fa-check combobox__check"></i> ${comboboxData[i]}</li>`;
            }
        }
    }
    comboboxList.innerHTML = comboboxListHTML;

    var comboboxItems = comboboxList.querySelectorAll('li');

    comboboxItems.forEach(function (comboboxItem) {
        comboboxItem.addEventListener('click', function () {
            currentValue = comboboxItem.getAttribute('data-id');
            renderCombobox(comboboxInput, comboboxList, comboboxData);
        });
    });
}

// RESOLVE INPUT VALUE
function resolveInputValue(comboboxInput, comboboxData, combobox, comboboxDropdown) {
    var inputValue = comboboxInput.value;
    var check = comboboxData.find(function (data) {
        return data == inputValue;
    });

    if (check == undefined) {
        combobox.classList.add('error');
    }
}


// SHOW DROPDOWN
function displayDropdown(comboboxList, combobox, comboboxDropdown) {
    combobox.classList.remove('error');
    combobox.classList.toggle('show');
}
// END FUNCTIONS