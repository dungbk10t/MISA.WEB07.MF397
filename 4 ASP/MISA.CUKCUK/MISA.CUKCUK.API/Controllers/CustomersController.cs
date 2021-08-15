﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
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
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;

        }
        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy thông tin tất cả các khách hàng 
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet]
        public IActionResult GetCustomers()
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
                var sqlCommand = $"SELECT * FROM Customer";
                var customers = dbConnection.Query<Customer>(sqlCommand);

                // 4. Trả về cho client
                if(customers.Count() > 0)
                {
                    var response = StatusCode(200, customers);
                    return response;
                }
                else
                {
                    return  StatusCode(204, customers);
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
        /// Lấy thông tin tất cả các khách hàng theo id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
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
                var sqlCommand = $"SELECT * FROM Customer WHERE customerId = '{customerId.ToString()}'";
                var customer = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand);

                // 4. Trả về cho client
                if (customer != null)
                {
                    return StatusCode(200, customer);
                }
                else
                {
                    return StatusCode(204);
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
        /// Lưu thông tin khách hàng mới
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPost]
        public IActionResult InsertCustomer(Customer customer)
        {
            try
            {
                // 4. Trả về cho client
                var serviceResult = _customerService.Add(customer);
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(204);
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Duplicate"))
                {
                    // Mã không được trùng với mã khách hàng
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_CUSTOMERCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
                else
                {
                    // Lỗi hệ thống 
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
        /// Xóa thông tin khách hàng theo id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomerById(Guid customerId)
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
                var sqlCommand = $"DELETE FROM Customer WHERE customerId = @customerIdParam";

                // Tránh lỗi SQL INJECTION
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@customerIdParam", customerId);

                var customer = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand, param: parameters);

                // 4. Trả về cho client
                var response = StatusCode(200, customer);
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
        /// Cập nhật thông tin khách hàng theo id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomerById(Guid customerId, Customer customer)
        {
            try
            {
                // Kiểm tra thông tin của khách hàng đã hợp lệ hay chưa ?

                // 1. Mã khách hàng bắt buộc phải có
                if (customer.CustomerCode == "" || customer.CustomerCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.EXCEPTION_ERR_NULL_CUSTOMERCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_500,
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
                        userMsg = Properties.Resources.EXCEPTION_ERR_EMAIL_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_500,
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
                var properties = customer.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy tên value của prop
                    var propValue = prop.GetValue(customer);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParam.Add($"@{propName}", propValue);
                    columnsUpdateParam += $"{propName} = @{propName} ,";
                }

                columnsUpdateParam = columnsUpdateParam.Remove(columnsUpdateParam.Length - 1, 1);
                var sqlCommand = $"UPDATE Customer SET {columnsUpdateParam} WHERE customerId = @customerId";
                dynamicParam.Add("@customerId", customerId);

                var rowsEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);

                // 4. Trả về cho client  
                var response = StatusCode(200, rowsEffects);
                return response;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate"))
                {
                    // Mã không được trùng với mã khách hàng
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_CUSTOMERCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
                else
                {
                    // Lỗi hệ thống 
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

