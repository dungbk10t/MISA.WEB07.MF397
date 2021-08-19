using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{

    public class BaseController<MISAEntity> : ControllerBase
    {
        #region Fields
        protected IBaseService<MISAEntity> _service;
        protected ServiceResult _serviceResult;
        #endregion

        #region Constructors
        public BaseController(IBaseService<MISAEntity> service)
        {
            _service = service;
        }
        #endregion

        #region Get request
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _serviceResult = _service.GetAll();
                if (_serviceResult.IsValid == false)
                {
                    _serviceResult.Messenger = Properties.Resources.NULLDATA_MSG;
                    return StatusCode(200, _serviceResult);
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
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            // Lấy dữ liệu và phản hồi cho client
           
            try
            {
                _serviceResult = _service.GetById(entityId);
                if(_serviceResult.IsValid == false)
                {
                    _serviceResult.Messenger = Properties.Resources.NULLDATA_MSG;
                    return StatusCode(200, _serviceResult);
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

        #region Post request

        [HttpPost]
        public IActionResult Add(MISAEntity entity)
        {
            try
            {
                _serviceResult = _service.Add(entity);
                if(_serviceResult.IsValid == false)
                {
                    return StatusCode(400, _serviceResult);
                }
                _serviceResult.Messenger = Properties.Resources.ADD_SUCCESS_MSG;
                return StatusCode(201, _serviceResult);
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

        #region Put request
        [HttpPut("{entityId}")]
        public IActionResult Update(Guid entityId, MISAEntity entity)
        {
            // Thực thi truy vấn và trả về kết quả cho client
            try
            {
                var serverResult = _service.Update(entity, entityId);
                if (serverResult.IsValid == false)
                {
                    return StatusCode(400, serverResult);
                }
                serverResult.Messenger = Properties.Resources.UPDATE_SUCCESS_MSG;
                return StatusCode(200, serverResult);
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

        #region Delete Request

        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                _serviceResult = _service.Delete(entityId);
                if(!_serviceResult.IsValid)
                {
                    _serviceResult.Messenger = Properties.Resources.EXCEPTION_ERR_MSG;
                    return StatusCode(400, _serviceResult);
                }
                _serviceResult.Messenger = Properties.Resources.DELETE_SUCCESS_MSG;
                return StatusCode(200, _serviceResult);
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
        [HttpDelete]
        public IActionResult DeleteMany([FromBody] List<Guid> entityIds)
        {
            try
            {
                _serviceResult = _service.DeleteMany(entityIds);
                if (!_serviceResult.IsValid)
                {
                    _serviceResult.Messenger = Properties.Resources.EXCEPTION_ERR_MSG;
                    return StatusCode(400, _serviceResult);
                }
                _serviceResult.Messenger = Properties.Resources.DELETE_SUCCESS_MSG;
                return StatusCode(200, _serviceResult);

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
