using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IEmployeeService: IBaseService<Employee>
    {

        /// <summary>
        /// Lấy và lọc dữ liệu nhân viên
        /// </summary>
        /// <param name="pageSize">Kích thước trang - Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="filterString">Dữ liệu lọc</param>
        /// <param name="departmentId">Mã phòng ban</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult GetByFilter(int pageSize, int pageNumber, string filterString, Guid? departmentId, Guid? positionId);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult GetNewCode();

    }
}
