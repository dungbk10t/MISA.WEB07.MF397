using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{
    //[Route("api/v1/customers")]
    //[ApiController]
    public class CustomersController : BaseController<Customer>
    {
        #region Fields
        private readonly ICustomerService _customerService;
        #endregion
        #region Constructors
        public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Get Requests
        /// <summary>
        /// Lọc danh sách khách hàng theo các tiêu chí : Phân trang, Tìm kiếm, Lọc theo nhóm khách hàng
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="filterString">Chuỗi lọc</param>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns></returns>
        /// Createdby : Phạm Tuấn Dũng (17/08/2021)
        [HttpGet("customerFilter")]
        public IActionResult GetCustomerByFilter(int pageSize, int pageNumber, string filterString, Guid? customerGroupId)
        {
            try
            {
                _serviceResult = _customerService.GetByFilter(pageSize, pageNumber, filterString, customerGroupId);
                if(_serviceResult.IsValid == false)
                {
                    _serviceResult.Messenge = Properties.Resources.NULLDATA_MSG;
                }
                // Trả về dữ liệu cho client 
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
        #endregion

        #region Import dữ liệu 
        [HttpPost("import")]
        /// <summary>
        /// Import bản ghi dữ liệu khách hàng từ file Excel rồi lưu vào Database
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IActionResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                _serviceResult = _customerService.Import(formFile, cancellationToken);
                return StatusCode(200, _serviceResult);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Resources.IMPORT_DATA_NOT_CORRECT_FORMAT_MSG,
                    errorCode = Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost("importMany")]
        /// <summary>
        /// Import nhiều bản ghi dữ liệu khách hàng từ file Excel rồi lưu vào Database
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public IActionResult ImportMany(List<Customer> customers)
        {
            try
            {
                _serviceResult = _customerService.InsertMany(customers);

                if (_serviceResult.IsValid == false)
                {
                    _serviceResult.Messenge = Resources.EXCEPTION_ERR_MSG;
                    return NoContent();
                }
                // Trả dữ liệu về cho client
                return StatusCode(200, _serviceResult);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Resources.IMPORT_DATA_NOT_CORRECT_FORMAT_MSG,
                    errorCode = Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        #endregion
    }
}

