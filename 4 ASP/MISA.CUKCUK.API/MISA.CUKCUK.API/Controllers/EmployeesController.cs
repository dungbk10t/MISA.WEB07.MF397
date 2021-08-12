﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.API.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy thông tin tất cả các nhân viên
        /// </summary>
        /// <returns></returns>
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

                var response = StatusCode(200, employees);
                return response;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_errorMessage,
                    errorCode = "misa-001",
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

                var response = StatusCode(400, employee);
                return response;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_errorMessage,
                    errorCode = "misa-001",
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
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                // Kiểm tra thông tin của khách hàng đã hợp lệ hay chưa ?

                // 1. Mã nhân viên bắt buộc phải có
                if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.Exception_errorMsg01,
                        errorCode = "misa-001",
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);

                }
                // 2. Email phải đúng định dạng
                var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                var isMatch = Regex.IsMatch(emailFormat, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isMatch == false)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.Exception_errorMsg02,
                        errorCode = "misa-001",
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                }
                // 3. Mã không được trùng với mã nhân viên

                // Truy cập vào Database :
                employee.EmployeeId = Guid.NewGuid();
                // 1. Khai báo thông tin database :
                var connectionString = "Host = 47.241.69.179;" +
                     "Database = MISA.CukCuk_Demo_NVMANH;" +
                     "User id = dev;" +
                     "Password = 12345678;";

                // 2. Khởi tạo đối tượng kết nói với Database :
                IDbConnection dbConnection = new MySqlConnection(connectionString);
                // Khai báo DynamicParam : 
                var dynamicParam = new DynamicParameters();
                // 3. Thêm dữ liệu vào trong database :
                var columnsName = string.Empty;
                var columnsParam = string.Empty;

                // Đọc từng property của object : 
                var propertise = employee.GetType().GetProperties();
                // Duyệt từng property của object :
                foreach (var prop in propertise)
                {
                    // Lấy tên prop : 
                    var propName = prop.Name;
                    // Lấy value của prop : 
                    var propValue = prop.GetValue(employee);
                    // Lấy kiểu dữ liệu của prop : 
                    var propType = prop.PropertyType;
                    // Thêm param tương ứng với mỗi property của đối tượng : 
                    dynamicParam.Add($"{propName}", propValue);
                    columnsName += $"{propName},";
                    columnsParam += $"@{propName},";
                }
                columnsName = columnsName.Remove(columnsName.Length - 1, 1);
                columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

                var sqlCommand = $"INSERT INTO Employee({columnsName}) VALUES ({columnsParam})";
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);
                // 4. Trả về cho client

                var response = StatusCode(400, rowEffects);
                return response;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_errorMessage,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        /// <summary>
        /// Xóa thông tin nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
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
                var response = StatusCode(400, employee);
                return response;

            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_errorMessage,
                    errorCode = "misa-001",
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
                        userMsg = Properties.Resources.Exception_errorMsg01,
                        errorCode = "misa-001",
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);

                }
                // 2. Email phải đúng định dạng
                var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                var isMatch = Regex.IsMatch(emailFormat, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isMatch == false)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.Exception_errorMsg02,
                        errorCode = "misa-001",
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
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_errorMessage,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
    }
}
