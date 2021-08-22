using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _serviceResult = _service.GetAll();
                if (_serviceResult.IsValid == false)
                {
                    _serviceResult.Messenge = Properties.Resources.NULLDATA_MSG;
                    return StatusCode(204, _serviceResult);
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
        /// <summary>
        /// Lấy dữ liệu ứng với id
        /// </summary>
        /// <param name="entityId">Id thực thể</param>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            // Lấy dữ liệu và phản hồi cho client
           
            try
            {
                _serviceResult = _service.GetById(entityId);
                if(_serviceResult.IsValid == false)
                {
                    _serviceResult.Messenge = Properties.Resources.NULLDATA_MSG;
                    return StatusCode(204, _serviceResult);
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
        #endregion

        #region Post request
        /// <summary>
        /// Thêm dữ liệu mới vào Database
        /// </summary>
        /// <param name="entity">Dữ liệu (dạng Object) muốn thêm vào</param>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
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
                _serviceResult.Messenge = Properties.Resources.ADD_SUCCESS_MSG;
                return StatusCode(201, _serviceResult);
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

        #region Put request
        /// <summary>
        /// Cập nhật thông tin 1 bản ghi dữ liệu
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <param name="entity">Dữ liệu (object) muốn cập nhật</param>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        
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
                serverResult.Messenge = Properties.Resources.UPDATE_SUCCESS_MSG;
                return StatusCode(200, serverResult);
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

        #region Delete Request
        /// <summary>
        /// Xóa bản ghi dữ liệu
        /// </summary>
        /// <param name="entityId">Id của bản ghi dữ liệu muốn xóa</param>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                _serviceResult = _service.Delete(entityId);
                if(!_serviceResult.IsValid)
                {
                    _serviceResult.Messenge = Resources.EXCEPTION_ERR_MSG;
                    return StatusCode(400, _serviceResult);
                }
                _serviceResult.Messenge = Resources.DELETE_SUCCESS_MSG;
                return StatusCode(200, _serviceResult);
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
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Mảng chứa các id ứng với các bản ghi cần xóa</param>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        [HttpDelete]
        public IActionResult DeleteMany([FromBody] List<Guid> entityIds)
        {
            try
            {
                _serviceResult = _service.DeleteMany(entityIds);
                if (!_serviceResult.IsValid)
                {
                    _serviceResult.Messenge = Resources.EXCEPTION_ERR_MSG;
                    return StatusCode(400, _serviceResult);
                }
                _serviceResult.Messenge = Properties.Resources.DELETE_SUCCESS_MSG;
                return StatusCode(200, _serviceResult);

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
