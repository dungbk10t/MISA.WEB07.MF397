using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.API.Models;
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
    public class PositionsController : ControllerBase
    {
        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy thông tin tất cả các vị trí 
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet]
        public IActionResult GetPositions()
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
                var sqlCommand = $"SELECT * FROM Position";
                var positions = dbConnection.Query<Position>(sqlCommand);

                // 4. Trả về cho client
                if (positions.Count() > 0)
                {
                    var response = StatusCode(200, positions);
                    return response;
                }
                else
                {
                    return StatusCode(204, positions);
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
        /// Lấy thông tin tất cả các vị trí theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpGet("{positionId}")]
        public IActionResult GetPositionById(Guid positionId)
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
                var sqlCommand = $"SELECT * FROM Position WHERE positionId = '{positionId.ToString()}'";
                var position = dbConnection.QueryFirstOrDefault<Position>(sqlCommand);

                // 4. Trả về cho client

                if (position != null)
                {
                    return StatusCode(200, position);
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
        /// Lưu thông tin vị trí mới
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPost]
        public IActionResult InsertPosition(Position position)
        {
            try
            {
                // Kiểm tra thông tin của vị trí đã hợp lệ hay chưa ?

                // 1. Mã vị trí bắt buộc phải có
                if (position.positionCode == "" || position.positionCode == null)
                {
                    var errorObj = new
                    {
                        userMsg = Properties.Resources.EXCEPTION_ERR_NULL_POSITIONCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);
                }

                // Truy cập vào Database :
                position.positionId = Guid.NewGuid();
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
                var propertise = position.GetType().GetProperties();
                // Duyệt từng property của object :
                foreach (var prop in propertise)
                {
                    // Lấy tên prop : 
                    var propName = prop.Name;
                    // Lấy value của prop : 
                    var propValue = prop.GetValue(position);
                    // Lấy kiểu dữ liệu của prop : 
                    var propType = prop.PropertyType;
                    // Thêm param tương ứng với mỗi property của đối tượng : 
                    dynamicParam.Add($"{propName}", propValue);
                    columnsName += $"{propName},";
                    columnsParam += $"@{propName},";
                }
                columnsName = columnsName.Remove(columnsName.Length - 1, 1);
                columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

                var sqlCommand = $"INSERT INTO Position({columnsName}) VALUES ({columnsParam})";
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
                if (ex.Message.Contains("Duplicate"))
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.Resources.EXCEPTION_ERR_DULICATE_POSITIONCODE_MSG,
                        errorCode = Properties.Resources.ERROR_CODE_400,
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
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
        /// Xóa thông tin vị trí theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpDelete("{positionId}")]
        public IActionResult DeletePositionById(Guid positionId)
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
                var sqlCommand = $"DELETE FROM Position WHERE positionId = @positionIdParam";

                // Tránh lỗi SQL INJECTION
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@positionIdParam", positionId);

                var position = dbConnection.QueryFirstOrDefault<Position>(sqlCommand, param: parameters);

                // 4. Trả về cho client
                var response = StatusCode(200, position);
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
        /// Cập nhật thông tin vị trí theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        /// CreatedBy: Pham Tuan Dung (12/8/2021)
        /// ModifiedBy: Pham Tuan Dung (12/8/2021)
        [HttpPut("{positionId}")]
        public IActionResult UpdatePositionById(Guid positionId, Position position)
        {
            try
            {
                // Kiểm tra thông tin của vị trí đã hợp lệ hay chưa ?

                // 1. Mã vị trí bắt buộc phải có
                if (position.positionCode == "" || position.positionCode == null)
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
                var properties = position.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy tên value của prop
                    var propValue = prop.GetValue(position);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParam.Add($"@{propName}", propValue);
                    columnsUpdateParam += $"{propName} = @{propName} ,";
                }

                columnsUpdateParam = columnsUpdateParam.Remove(columnsUpdateParam.Length - 1, 1);
                var sqlCommand = $"UPDATE Position SET {columnsUpdateParam} WHERE positionId = @positionId";
                dynamicParam.Add("@positionId", positionId);

                var rowsEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);

                // 4. Trả về cho client  
                var response = StatusCode(200, rowsEffects);
                return response;
            }
            catch (Exception ex)
            {
                // Mã không được trùng với mã vị trí
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
