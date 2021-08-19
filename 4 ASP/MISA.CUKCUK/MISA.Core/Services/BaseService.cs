using MISA.Core.Entities;
using MISA.Core.Validations;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Core.Exceptions;

namespace MISA.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        #region Fields
        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;
        string _entityName;
        #endregion

        #region Constructors
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
            _entityName = typeof(MISAEntity).Name;
        }
        #endregion
        /// <summary>
        /// Thiết lập Mode theo kiểu liệt kê (enum) với Add = 0, Update = 1
        /// </summary>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        #region Enums
        protected enum Mode
        {
            Add,
            Update
        }
        #endregion

        #region GET Methods :
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        public virtual ServiceResult GetAll()
        {
            _serviceResult.Data = _baseRepository.GetAll();
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }
        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        ///  Createdby : Phạm Tuấn Dũng (16/08/2021)
        public virtual ServiceResult GetById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.GetById(entityId);
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }
        #endregion

        #region Base Validations
        /// <summary>
        /// Validate dữ liệu cơ bản
        /// </summary>
        /// <param name="entity">Dữ liệu muốn thực hiện validate</param>
        /// <param name="mode">mode = 0 : Thêm mới ; mode = 1 : Cập nhật</param>
        /// <returns>true - dữ liệu hơp lệ ; false - dữ liệu không hợp lệ</returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        protected ServiceResult Validate(MISAEntity entity, int mode)
        {
            var email = typeof(MISAEntity).GetProperty("Email");
            var code = typeof(MISAEntity).GetProperty($"{_entityName}Code");
            var _entityNameUpper = _entityName.ToUpper();

            // Xử lý nghiệp vụ

            // 1. Xử lý mã rỗng
            if (code != null && !Validations.Validations.Required((string)code.GetValue(entity)))
            {
                return new ServiceResult
                {
                    IsValid = false,
                    Messenger = Resources.EXCEPTION_ERR_NULL_EMPLOYEECODE_MSG
                };
            }

            // 2. Xử lý mã trùng
            if (mode == (int)Mode.Add && code != null && CheckDuplicateEntityCode((string)code.GetValue(entity)))
            {
                return new ServiceResult
                {
                    IsValid = false,
                    Messenger = Resources.ResourceManager.GetString($"EXCEPTION_ERR_DULICATE_{_entityNameUpper}CODE_MSG")
                };
            }
            // 3. Xử lý định dạng email

            if (email != null && !Validations.Validations.ValidateEmail((string)email.GetValue(entity)))
            {
                return new ServiceResult
                {
                    IsValid = false,
                    Messenger = Resources.EXCEPTION_ERR_EMAIL_MSG
                };
            }
            return new ServiceResult
            {
                IsValid = true
            };
        }
        /// <summary>
        /// Kiểm tra sự trùng lặp mã của thực thể
        /// </summary>
        /// <param name="code">Mã của thực thể</param>
        /// <returns>true : Không trùng lặp ; false : Trùng lặp mã</returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        protected bool CheckDuplicateEntityCode(string entityCode)
        {
            var result = _baseRepository.GetByCode(entityCode);
            if (result != null)
                return true;
            return false;
        }

        #endregion

        #region Add method:
        /// <summary>
        /// Thêm bản ghi mới vào bảng khách hàng
        /// </summary>
        /// <param name="entity">Dữ liệu thêm mới</param>
        /// <returns></returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        public virtual ServiceResult Add(MISAEntity entity)
        {
            // Xử lý nghiệp vụ : Kiểm tra tính hợp lệ của dư liệu khi validate
            _serviceResult = Validate(entity, (int)Mode.Add);
            if(!_serviceResult.IsValid)
            {
                return _serviceResult;
            }

            // Kết nối tới Infrasturcture service làm việc với Database
            _serviceResult.Data = _baseRepository.Add(entity);
            if((int)_serviceResult.Data > 0)
            {
                _serviceResult.IsValid = true;
            }
            else
            {
                _serviceResult.IsValid = false;
                _serviceResult.Messenger = Resources.EXCEPTION_ERR_MSG;
            }
            return _serviceResult;
        }
        #endregion

        #region Update method:
        /// <summary>
        /// Cập nhật dữ liệu khách hàng
        /// </summary>
        /// <param name="entity">Dữ liệu cập nhật</param>
        /// <param name="entityId">Id của khác hàng</param>
        /// <returns></returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        public virtual ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            // Xử lý nghiệp vụ : Kiểm tra tính hợp lệ của dư liệu khi validate
            _serviceResult = Validate(entity, (int)Mode.Update);
            if (!_serviceResult.IsValid)
            {
                return _serviceResult;
            }

            // Lấy id của propertiy
            var id = typeof(MISAEntity).GetProperty($"{_entityName}Id");
            if(id != null)
            {
                id.SetValue(entity, entityId);
            }

            // Kết nối tới Infrasturcture service làm việc với Database
            _serviceResult.Data = _baseRepository.Update(entity, entityId);
            if((int)_serviceResult.Data > 0)
            {
                _serviceResult.IsValid = true;
            }
            else
            {
                _serviceResult.IsValid = false;
                _serviceResult.Messenger = Resources.EXCEPTION_ERR_MSG;
            }
            return _serviceResult;
        }
        #endregion

        #region Delete methods:
        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns></returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        public virtual ServiceResult Delete(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.Delete(entityId);
            _serviceResult.IsValid = (int)_serviceResult.Data > 0;
            return _serviceResult;
        }
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Các id của các bản ghi</param>
        /// <returns></returns>
        /// Createdby : Phạm Tuấn Dũng (16/08/2021)
        public virtual ServiceResult DeleteMany(List<Guid> entityIds)
        {
            _serviceResult.Data = _baseRepository.DeleteMany(entityIds);
            _serviceResult.IsValid = (int)_serviceResult.Data > 0;
            return _serviceResult;
        }
        #endregion
      
    }
}
