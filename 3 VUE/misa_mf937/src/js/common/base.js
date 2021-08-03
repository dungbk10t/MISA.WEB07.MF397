class baseJS {
    tableId = null;
    formMode = null;
    eId = null;
    type = null;
    link = null;
    validEmail = null;
    validPhoneNumber = null;
    constructor(TableId, Type, Link) {
        this.tableId = TableId
        this.type = Type
        this.link = Link
        this.loadData();
        this.initEvent();
    }
    loadData() {
        $(`#${this.tableId} table tbody`).empty();
        $.ajax({
            url: this.link,
            method: "GET",  
            // async: false
        }).done((res)=> {
            console.log(res)
            var here = this
            res.forEach((e) => {
                let columns = $(`#${here.tableId} table th`)
                let trHTML = $(`<tr employeeId = ${e.EmployeeId}></tr>`)
                $.each(columns, (index, col)=>{
                    let tdHTML = $('<td></td>')
                    // Để tạm fix sau  -> Chinh index = 0 thì append checkbox, index = 1 thì append số STT
                    if(index == 0){
                        tdHTML.append($('<input onclick="checkBox($(this))" type="checkbox" style="width:46px; height:22px;">'));
                    }
                    // else if(index == 1) {
                    //     tdHTML.append('<div style="width:46px; height:24px;">1</div>');
                    // }
                    // Để tạm fix sau  
                    else {
                        let format = $(col).attr("format")
                        let propName = $(col).attr("fieldName")
                        let value = e[propName] 
                        switch(format){
                            case "dmy":
                                value = here.dateFormat(value)
                                break                              
                            case "money":
                                value = JSON.stringify(value)
                                value = here.salaryFormat(value)
                                break 
                            case "workStatus":
                                if(value == 0){
                                    value = "Đang làm việc"
                                    break
                                } 
                                if(value == 1){
                                    value = "Đang thử việc"
                                    break;
                                } 
                                if(value == 2){
                                    value = "Đã nghỉ việc"
                                    break
                                } 
                                else {
                                    value = ""
                                    break;
                                }                        
                            default: 
                        }    
                        tdHTML.append(value)
                    } 
                    trHTML.append(tdHTML)
                })
                $(`#${here.tableId} table tbody`).append(trHTML);   
            });
        }).fail(function(res){
    
        })
    }

    initEvent(){
        var here = this
        $("#btn-add").click(here.btnAddOnClick.bind(here));
        //xử lý sự kiện nút xóa nhân viên
        $("#btn-delete").click(here.btnOnClickDelete.bind(here));
        //Xử lý sựu kiện khi nhấn đúp chuột vào 1 row của table
        $(".tb-body").on("dblclick", "tbody tr", function () {
            alert("Click được rồi");
            here.formMode = 1;
            let eForm = $(".employee-dialog");
            eForm.show();
            here.eId = $(this).attr("employeeId");
            $.ajax({
                url: here.link + `/${here.eId}`,
                method: "GET",
            }).done(function (res) {
                console.log(res)
                var fieldList = $('.dialog-content-right .small-info')
                $.each(fieldList, (i, f) => {
                    let textField = $(f).find('input[type=text]')
                    let dateField = $(f).find('input[type=date]')
                    if (textField.length == 1) {
                        textField.val(res[$(f).attr('fieldName')])
                    }
                    if (dateField.length == 1) {
                        let _dob = res[$(f).attr('fieldName')];
                        _dob = new Date(_dob)
                        let date = _dob.getDate()
                        let month = _dob.getMonth() + 1
                        let year = _dob.getFullYear()
                        if (date < 10) {
                            date = "0" + date
                        }
                        if (month < 10) {
                            month = "0" + month
                        }
                        var dob = year + "-" + month + "-" + date;
                        dateField.val(dob)
                    }
                    else {
                        var selectedItem = $(f).find(`[value="${res[$(f).attr('fieldName')]}"]`)
                        $(f).find(".select").text(selectedItem.text().trim())
                        var itemList = $(f).find(".dropdown-list .dropdown-list-item")
                        $.each(itemList, (index, item) => {
                            $(item).removeClass("dropdown-item-check")
                            $(item).children(".fa-check").css("opacity", "0")
                        })
                        selectedItem.toggleClass("dropdown-item-check")
                        selectedItem.children(".fa-check").css("opacity", "1")
                    }
                })

            }).fail(function (res) {
                alert("Không thể binding dữ liệu lên form");
            })
        });
        //Xử lý dữ liệu cho nút Lưu
        $("#btn-save").click(here.btnSaveOnClick.bind(here));
        
        $("input").focus(function(){
            $(this).css("border", "2px solid #01b075");
        });
        $("input").blur(function(){
            $(this).css("border", "1px solid #bbbbbb");
        });

        $("input[required]").blur(function(){
            var val = $(this).val();
            if(val == ""){
                $(this).css("border", "2px solid red");
                $(this).attr("title","Đây là thông tin bắt buộc")
            }else {
                $(this).css("border", "1px solid #bbbbbb");
            }    
        });
        $('#btn-close').click(here.btnCloseOnclick.bind(here));
        $('#btn-close1').click(here.btnCloseOnclick.bind(here));
        $('#btn-close2').click(here.btnCloseOnclick.bind(here));
        $('#btn-cancel').click(here.btnCancelOnClick.bind(here));
    }
    dateFormat(_dob){
        let dob = null
        if(_dob != null){
            let __dob = new Date(_dob);
            let date = __dob.getDate();
            let month =__dob.getMonth()+1;
            let year = __dob.getFullYear();
            dob = `${date}/${month}/${year}`;
        }
        return dob;
    }

    salaryFormat(str){
        if(str != "null"){
            var length = 3,
              separator = ".",
              count = 0,
              result = str.split('').reduceRight((a, c) => {
                if (count === length) {
                  a.push(separator);
                  count = 1;
                } else count++;
                a.push(c);
                return a;
              }, []).reverse().join('');
              
              return result;
        }
        else return ""
    }
    btnAddOnClick() {
        // alert("OK rồi !!");
        this.formMode = 0
        $(".employee-dialog input").val(null);
        $("input[required]").css("border", "1px solid #bbbbbb");
        $(".employee-dialog").show();
    }
    btnOnClickDelete() {    
        let delete_success = 0;
        let deleteEntityList = $(".tb-body tbody tr input[type=checkbox]:checked");       
        let needDelete= $(".tb-body tbody tr input[type=checkbox]:checked").length;
        $.each(deleteEntityList, (index, e)=>{
            let deleteEntity = $(e).parent().parent().attr("employeeId")
            $.ajax({
                url: this.link+`/${deleteEntity}`,
                method: "DELETE"
            }).done((res)=> {
                delete_success = delete_success + 1;
                if(delete_success == needDelete){
                    alert("Xóa tất cả thành công")
                    this.loadData()
                }
            }).fail(function(res){
                alert('Không xóa được')
            })
        })
    }
    btnSaveOnClick () {
        var fieldList = $('.dialog-content-right .small-info')
        var entity = {}
        $.each(fieldList, (i,f)=>{
            let textField = $(f).find('input[type=text]') 
            let dateField = $(f).find('input[type=date]');
            console.log(textField);
            console.log(textField.length);
            if(textField.length == 1){
                entity[$(f).attr("fieldName")] = textField.val();
            }
            else {
                if(dateField.length == 1){
                    entity[$(f).attr("fieldName")] = dateField.val() + "T00:00:00";
                }           
                else {
                    entity[$(f).attr("fieldName")] = $(f).find(".dropdown-item-check").attr("value");
                }   
            }
        });
        console.log(entity)
        if(this.formMode == 0){
            console.log(0)
            $.ajax({
                url: this.link,
                method: "POST", 
                data: JSON.stringify(entity),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
            }).done((res)=> {
                alert('Thêm mới thành công')
                this.loadData()
            }).fail(function(res){
                alert('Không thêm được')
            })
        }
        if(this.formMode == 1) {
            console.log(1)
            $.ajax({
                url: this.link+`/${this.eId}`,
                method: "PUT", 
                data: JSON.stringify(entity),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
            }).done((res)=> {
                alert('Sửa thành công')
                this.loadData()
            }).fail(function(res){
                alert('Không sửa được')
            })
        }
    }
    btnCloseOnclick() {
        $('.employee-dialog').hide();
        $('.dialog-delete').hide();
        $('.dialog-edit-delete').hide();
        $('.dialog-delete strong span').remove();
        $('.dialog-edit-delete .title span').remove();
    }
    btnCancelOnClick() {
        $('.employee-dialog').hide();
        $('.dialog-delete').hide();
        $('.dialog-edit-delete').hide();
        $('.dialog-delete strong span').remove();
        $('.dialog-edit-delete .title span').remove();
    }

}
