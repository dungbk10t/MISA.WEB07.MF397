$(document).ready(function () {
    $('.employee-dialog').hide();
    $('.dialog-delete').hide();
    $('.dialog-edit-delete').hide();
    $('.dialog-delete strong span').remove();
    $('.dialog-edit-delete .title span').remove();
    new EmployeeJS();
});

class EmployeeJS extends baseJS {
    constructor() {
        super("employee__table", "Employee", "http://cukcuk.manhnv.net/v1/Employees");
        this.loadDropdown();
    }
    loadDropdown() {
        /* Bind dữ liệu lên các dropdown */
        // 1. Dropdown position form
        this.loadPositionData("employee__position");
        // 2. Dropdown department form
        this.loadDepartmentData("employee__department");
        // 3. Dropdown position table
        this.loadPositionData("employee__position_1");
        // 4. Dropdown  position table
        this.loadDepartmentData("employee__department_1");
    }
    loadPositionData(id) {
        try {
            $.ajax({
                url: "http://cukcuk.manhnv.net/v1/Positions",
                method: 'GET',
                async: false
            }).done(res => {
                $("#" + id + " .dropdown-list").empty();
                res.forEach(e => {
                    const positionID = e["PositionId"];
                    const positionName = e["PositionName"];
                    // console.log(positionName);
                    // console.log(positionID);
                    let optionHTML = $(`<div class="dropdown-list-item pos-txt" value="${positionID}">
                                        <i class="fas fa-check"></i>
                                        <div class="dd-item-text">${positionName}</div>
                                    </div>`);
                    $("#" + id + " .dropdown-list").append(optionHTML);
                });
            }).fail(res => {
                alert("Không load được vị trí!!");
            });
        } catch (error) {

        }
    }
    loadDepartmentData(id) {
        try {
            $("#" + id + " .dropdown-list").empty();
            $.ajax({
                url: "http://cukcuk.manhnv.net/api/Department",
                method: 'GET',
                async: false
            }).done(res => {
                $("#" + id + " .dropdown-list").empty();
                res.forEach(e => {
                    const departmentID = e["DepartmentId"];
                    const departmentName = e["DepartmentName"];
                    // console.log(departmentID);
                    // console.log(departmentName);
                    let optionHTML = $(`<div class="dropdown-list-item depart-txt" value="${departmentID}">
                                        <i class="fas fa-check"></i>
                                        <div class="dd-item-text">${departmentName}</div>
                                    </div>`);
                    $("#" + id + " .dropdown-list").append(optionHTML);
                });
            }).fail(res => {
                alert("Không load được phòng ban!!");
            })
        } catch (error) {
            console.log(error);
        }
    }

    


}