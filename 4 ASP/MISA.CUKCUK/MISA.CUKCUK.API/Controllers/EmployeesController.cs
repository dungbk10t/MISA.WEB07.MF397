using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        // Tiem no vao
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository; 

        }
       
        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy thông tin tất cả các nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {

                // Truy cập vào Database :
                // 1. Khai báo thông tin database :
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User id = dev;" +
                    "Password = 12345678;";

                // 2. Khởi tạo đối tượng kết nói với Database :
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3. Lấy dữ liệu :
                var sqlCommand = $"SELECT * FROM Employee";
                var employees = dbConnection.Query<Employee>(sqlCommand);

                // 4. Trả về cho client
                if (employees.Count() > 0)
                {
                    var response = StatusCode(200, employees);
                    return response;
                }
                else
                {
                    return StatusCode(204, employees);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.EXCEPTION_ERR_MSG_500,
                    errorCode = Properties.Resources.ERROR_CODE_500,
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        /// <summary>
        /// Lấy thông tin tất cả các nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(Guid employeeId)
        {
            try
            {
                // Truy cập vào Database :

                // 1. Khai báo thông tin database :
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User id = dev;" +
                    "Password = 12345678;";

                // 2. Khởi tạo đối tượng kết nói với Database :
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3. Lấy dữ liệu :
                var sqlCommand = $"SELECT * FROM Employee WHERE employeeId = '{employeeId.ToString()}'";
                var employee = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand);

                // 4. Trả về cho client
                if(employee != null)
                {
                    var response = StatusCode(200, employee);
                    return response;
                }
                else
                {
                    return StatusCode(204, employee);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.EXCEPTION_ERR_MSG_500,
                    errorCode = Properties.Resources.ERROR_CODE_500,
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }
        /// <summary>
        /// Lưu thông tin nhân viên mới
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                //// Kiểm tra thông tin của khách hàng đã hợp lệ hay chưa ?

                //// 1. Mã nhân viên bắt buộc phải có
                //if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
                //{
                //    var errorObj = new
                //    {
                //        devMsg = "",
                //        userMsg = Properties.Resources.EXCEPTION_ERR_NULL_EMPLOYEECODE_MSG,
                //        errorCode = Properties.Resources.ERROR_CODE_400,
                //        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                //        traceId = ""
                //    };
                //    return StatusCode(500, errorObj);

                //}
                //// 2. Email phải đúng định dạng
                //var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                //var isMatch = Regex.IsMatch(emailFormat, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                //if (isMatch == false)
                //{
                //    var errorObj = new
                //    {
                //        userMsg = Properties.Resources.EXCEPTION_ERR_EMAIL_MSG,
                //        errorCode = Properties.Resources.ERROR_CODE_500,
                //        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                //        traceId = ""
                //    };
                //}

                

                // 4. Trả về cho client
                //var serviceResult = _employeeService.Add(employee);
                //if (serviceResult.IsValid == true)
                //{
                //    return StatusCode(201, serviceResult.Data);
                //}
                //else
                //{
                //    return StatusCode(204);
                //}


               
            }
            catch (Exception ex)
            {
                // Mã không được trùng với mã nhân viên
                if (ex.Message.Contains("Duplicate"))
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_EMPLOYEECODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
                // Lỗi hệ thống
                else
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_MSG_500,
                        errorCode = Properties.Resources.ERROR_CODE_500,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);
                }
                
            }

        }
        /// <summary>
        /// Xóa thông tin nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployeeById(Guid employeeId)
        {
            try
            {
                // Truy cập vào Database :
                // 1. Khai báo thông tin database :
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User id = dev;" +
                    "Password = 12345678;";

                // 2. Khởi tạo đối tượng kết nói với Database :
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3. Lấy dữ liệu :
                var sqlCommand = $"DELETE FROM Employee WHERE employeeId = @employeeIdParam";

                // Tránh lỗi SQL INJECTION
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@employeeIdParam", employeeId);

                var employee = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);

                // 4. Trả về cho client
                var response = StatusCode(200, employee);
                return response;

            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.EXCEPTION_ERR_MSG_500,
                    errorCode = Properties.Resources.ERROR_CODE_500,
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        /// <summary>
        /// Cập nhật thông tin nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployeeById(Guid employeeId, Employee employee)
        {
            try
            {
                // Kiểm tra thông tin của khách hàng đã hợp lệ hay chưa ?

                // 1. Mã khách hàng bắt buộc phải có
                if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.EXCEPTION_ERR_NULL_EMPLOYEECODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);

                }
                // 2. Email phải đúng định dạng
                var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                var isMatch = Regex.IsMatch(emailFormat, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isMatch == false)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.EXCEPTION_ERR_EMAIL_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                }
                // 3. Mã không được trùng với mã khách hàng

                // Truy cập vào Database :
                // 1. Khai báo thông tin database :
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User id = dev;" +
                    "Password = 12345678;";

                // 2. Khởi tạo đối tượng kết nói với Database :
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // Khai báo DynamicParameters 
                var dynamicParam = new DynamicParameters();

                // 3. Thêm dữ liệu vào trong Database
                var columnsUpdateParam = string.Empty;

                // Đọc từng property của object
                var properties = employee.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy tên value của prop
                    var propValue = prop.GetValue(employee);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParam.Add($"@{propName}", propValue);
                    columnsUpdateParam += $"{propName} = @{propName} ,";
                }

                columnsUpdateParam = columnsUpdateParam.Remove(columnsUpdateParam.Length - 1, 1);
                var sqlCommand = $"UPDATE Employee SET {columnsUpdateParam} WHERE employeeId = @employeeId";
                dynamicParam.Add("@employeeId", employeeId);

                var rowsEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);

                // 4. Trả về cho client  
                var response = StatusCode(200, rowsEffects);
                return response;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate"))
                {
                    // Mã không được trùng với nhân viên
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_EMPLOYEECODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
                // Lỗi server
                else
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_MSG_500,
                        errorCode = Properties.Resources.ERROR_CODE_500,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);
                }
            }

        }
    }
}
