using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {

        #region Fields
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult2;
        #endregion

        #region Constructors
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _serviceResult2 = new ServiceResult();
        }
        #endregion

        #region GET methods of Employee
        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <param name="filterString">Dữ liệu lọc</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="positionId">Id vị trí trức vụ </param>
        /// <returns></returns>
        public ServiceResult GetByFilter(int pageSize, int pageNumber, string filterString, Guid? departmentId, Guid? positionId)
        {
            _serviceResult.Data = _employeeRepository.GetByFilter(pageSize, pageNumber, filterString, departmentId, positionId);
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetNewCode()
        {
            _serviceResult.Data = _employeeRepository.GetNewCode();
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }
        #endregion
    }
}
