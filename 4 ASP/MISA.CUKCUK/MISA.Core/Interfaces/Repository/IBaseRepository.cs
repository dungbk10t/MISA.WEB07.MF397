using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {

        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        List<MISAEntity> GetAll();
        /// <summary>
        ///  /// Lấy dữ liệu theo ID
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        MISAEntity GetById(Guid entityId);
        /// <summary>
        /// Lấy dữ liệu theo mã
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        MISAEntity GetByCode(string entityCode);
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(MISAEntity entity);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Update(MISAEntity entity, Guid entityId);
        /// <summary>
        /// Xóa đơn dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Delete(Guid entityId);
        /// <summary>
        /// Xóa đa dữ liệu theo id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        int DeleteMany(List<Guid> entityIds);

    }
}
