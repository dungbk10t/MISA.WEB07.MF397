using Dapper;
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
    public class PositionsController : ControllerBase
    {
        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy thông tin tất cả các vị trí 
        /// </summary>
        /// <returns></returns>
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

                var response = StatusCode(200, positions);
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
        /// Lấy thông tin tất cả các vị trí theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
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

                var response = StatusCode(400, position);
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
        /// Lưu thông tin vị trí mới
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
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
                        devMsg = "",
                        userMsg = Properties.Resources.Exception_errorMsg01,
                        errorCode = "misa-001",
                        moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(500, errorObj);

                }
               
                // 2. Mã không được trùng với mã vị trí

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
        /// Xóa thông tin vị trí theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
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
                var response = StatusCode(400, position);
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
        /// Cập nhật thông tin vị trí theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
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
                // 3. Mã không được trùng với mã vị trí

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
