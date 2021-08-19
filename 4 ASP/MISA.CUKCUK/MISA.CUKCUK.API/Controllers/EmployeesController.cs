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
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeesController :BaseController<Employee>
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Contructors
        public EmployeesController(IEmployeeService employeeService):base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region GET requets
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                _serviceResult = _employeeService.GetNewEmployeeCode();
                return StatusCode(200, _serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.EXCEPTION_ERR_MSG,
                    errorCode = Properties.Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet("employeeFilter")]
        public IActionResult GetEmployeeByFilter(int pageSize, int pageNumber, string filterString, Guid? departmentId, Guid? positionId)
        {
            try
            {
                _serviceResult = _employeeService.GetByFilter(pageSize, pageNumber, filterString, departmentId, positionId);
                if(!_serviceResult.IsValid)
                {
                    _serviceResult.Messenger = Properties.Resources.NULLDATA_MSG;
                    return Ok(_serviceResult);
                }
                return StatusCode(200, _serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.EXCEPTION_ERR_MSG,
                    errorCode = Properties.Resources.CODE_500,
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }
        #endregion
    }
}
