using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary> 
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        ServiceResult GetAll();
        /// <summary>
        /// Lấy dữ liệu theo id 
        /// </summary>
        /// <param name="entityId">Id thực thể</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult GetById(Guid entityId);
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult Add(MISAEntity entity);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <param name="entityId">Id thực thể</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult Update(MISAEntity entity, Guid entityId);
        /// <summary>
        /// Xóa đơn dữ liệu theo id
        /// </summary>
        /// <param name="entityId">Id thực thể</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult Delete(Guid entityId);
        /// <summary>
        /// Xóa đa dữ liệu theo id
        /// </summary>
        /// <param name="entityIds">Id thực thể</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult DeleteMany(List<Guid> entityIds);
    }
}
