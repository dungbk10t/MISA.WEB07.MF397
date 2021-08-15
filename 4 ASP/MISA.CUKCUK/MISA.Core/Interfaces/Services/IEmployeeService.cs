using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Thêm mới khách hàng 
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (13/08/2021) 
        ServiceResult Add(Employee employee);
        /// <summary>
        /// Cập nhật khách hàng 
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (13/08/2021) 
        ServiceResult Update(Employee employee, Guid employeeId);
    }
}
