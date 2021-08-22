using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{
    //[Route("api/v1/employees")]
    //[ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Contructors
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region GET requets
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                _serviceResult = _employeeService.GetNewCode();
                return StatusCode(200, _serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Resources.EXCEPTION_ERR_MSG,
                    errorCode = Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Lọc và phân trang theo các tiêu chí : Tìm kiếm, Id phòng ban, Id vị trí/chức vụ
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="filterString">Chuỗi lọc tìm kiếm</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="positionId">Id vị trí</param>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        [HttpGet("employeeFilter")]
        public IActionResult GetEmployeeByFilter(int pageSize, int pageNumber, string filterString, Guid? departmentId, Guid? positionId)
        {
            try
            {
                _serviceResult = _employeeService.GetByFilter(pageSize, pageNumber, filterString, departmentId, positionId);
                if (!_serviceResult.IsValid)
                {
                    _serviceResult.Messenge = Properties.Resources.NULLDATA_MSG;
                    return Ok(_serviceResult);
                }
                return StatusCode(200, _serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Resources.EXCEPTION_ERR_MSG,
                    errorCode = Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }
        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                var employees = new List<Employee>();
                if (formFile == null)
                {
                    var response = new
                    {
                        devMsg = "Null File.",
                        userMsg = "Vui lòng chọn tệp nhập khẩu",
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        traceId = Guid.NewGuid().ToString()
                    };
                    return StatusCode(400, response);
                }
                // Check file có hợp lệ hay không (file phải có định dạng xls, xlsx)

                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                       
                        for (int row = 3; row <= rowCount; row++)
                        {
                            var employee = new Employee();
                            var EmployeeCode = worksheet.Cells[row, 1].Value;
                            
                            // Thực hiện validate dữ liệu : 
                            if(employee.EmployeeCode == "")
                            {
                                var errorMessage = "Mã khách hàng không được phép để trống ";
                                employee.ImportError.Add(errorMessage);

                            }
                            employees.Add(employee);

                            // Thực hiện check trùng mã : 
                            employee.ImportError.Add("Mã khách hàng đã tồn tại trong hệ thống");
                        }
                    }
                }
                // Check độ lớn của file (Giới hạn 100MB) :

                // Thực hiện đọc dữ liệu trong tệp Excel:


                return Ok();
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Resources.EXCEPTION_ERR_MSG,
                    errorCode = Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }
        #endregion
    }
}
