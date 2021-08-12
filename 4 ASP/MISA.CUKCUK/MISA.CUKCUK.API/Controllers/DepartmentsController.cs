using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using MISA.CUKCUK.API.Models;
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
    public class DepartmentsController : ControllerBase
    {
        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy thông tin tất cả các phòng ban
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet]
        public IActionResult GetDeparments()
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
                var sqlCommand = $"SELECT * FROM Deparment";
                var departments = dbConnection.Query<Department>(sqlCommand);

                // 4. Trả về cho client
                if (departments.Count() > 0)
                {
                    var response = StatusCode(200, departments);
                    return response;
                }
                else
                {
                    return StatusCode(204, departments);
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
        /// Lấy thông tin tất cả các phòng ban theo id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("{departmentId}")]
        public IActionResult GetDeparmentById(Guid departmentId)
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
                var sqlCommand = $"SELECT * FROM Deparment WHERE departmentId = '{departmentId.ToString()}'";
                var department = dbConnection.QueryFirstOrDefault<Department>(sqlCommand);

                // 4. Trả về cho client
                if (department != null)
                {
                    return StatusCode(200, department);
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
        /// Lưu thông tin phòng ban mới
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPost]
        public IActionResult InsertDeparment(Department department)
        {
            try
            {
                // Kiểm tra thông tin của phòng ban đã hợp lệ hay chưa ?

                // 1. Mã phòng ban bắt buộc phải có
                if (department.departmentCode == "" || department.departmentCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.EXCEPTION_ERR_NULL_DEPARTMENTCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);

                }
                // Truy cập vào Database :
                department.departmentId = Guid.NewGuid();
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
                var propertise = department.GetType().GetProperties();
                // Duyệt từng property của object :
                foreach (var prop in propertise)
                {
                    // Lấy tên prop : 
                    var propName = prop.Name;
                    // Lấy value của prop : 
                    var propValue = prop.GetValue(department);
                    // Lấy kiểu dữ liệu của prop : 
                    var propType = prop.PropertyType;
                    // Thêm param tương ứng với mỗi property của đối tượng : 
                    dynamicParam.Add($"{propName}", propValue);
                    columnsName += $"{propName},";
                    columnsParam += $"@{propName},";
                }
                columnsName = columnsName.Remove(columnsName.Length - 1, 1);
                columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

                var sqlCommand = $"INSERT INTO Deparment({columnsName}) VALUES ({columnsParam})";
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);
                // 4. Trả về cho client
                if (rowEffects > 0)
                {
                    return StatusCode(201, rowEffects);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                // Mã không được trùng với mã phòng ban
                if (ex.Message.Contains("Duplicate"))
                {
                    // Mã không được trùng với mã khách hàng
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_DEPARTMENTCODE_MSG,
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
        /// Xóa thông tin phòng ban theo id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDeparmentById(Guid departmentId)
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
                var sqlCommand = $"DELETE FROM Deparment WHERE departmentId = @departmentIdParam";

                // Tránh lỗi SQL INJECTION
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@departmentIdParam", departmentId);

                var department = dbConnection.QueryFirstOrDefault<Department>(sqlCommand, param: parameters);

                // 4. Trả về cho client
                var response = StatusCode(200, department);
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
        /// Cập nhật thông tin phòng ban theo id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPut("{departmentId}")]
        public IActionResult UpdateDeparmentById(Guid departmentId, Department department)
        {
            try
            {
                // Kiểm tra thông tin của phòng ban đã hợp lệ hay chưa ?

                // 1. Mã phòng ban bắt buộc phải có
                if (department.departmentCode == "" || department.departmentCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_POSITIONCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);

                }

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
                var properties = department.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy tên value của prop
                    var propValue = prop.GetValue(department);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParam.Add($"@{propName}", propValue);
                    columnsUpdateParam += $"{propName} = @{propName} ,";
                }

                columnsUpdateParam = columnsUpdateParam.Remove(columnsUpdateParam.Length - 1, 1);
                var sqlCommand = $"UPDATE Deparment SET {columnsUpdateParam} WHERE departmentId = @departmentId";
                dynamicParam.Add("@departmentId", departmentId);

                var rowsEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);

                // 4. Trả về cho client  
                var response = StatusCode(200, rowsEffects);
                return response;
            }
            catch (Exception ex)
            {
                // Mã không được trùng với mã phòng ban
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
