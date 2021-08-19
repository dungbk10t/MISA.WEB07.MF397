using MISA.Core.Entities;
using MISA.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy và lọc dữ liệu trả về
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageNumber"></param>
        /// <param name="filterString"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        FilterResponse GetByFilter(int pageSize, int PageNumber, string filterString, Guid? departmentId, Guid? positionId);
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        string GetNewCode();
    }
}
