using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấp và lọc dữ liệu khách hàng
        /// </summary>
        /// <param name="pageSize">Kích thước trang - Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="filterString">Dữ liệu lọc</param>
        /// <param name="customerGroupId">Mã nhóm khách hàng</param>
        /// <returns>ServiceResult -  kết quả xử lý nghiệp vụ</returns>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        ServiceResult GetByFilter(int pageSize, int pageNumber, string filterString, Guid? customerGroupId);

        /// <summary>
        /// Import dữ liễu vào file
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken);

        /// <summary>
        /// Thêm nhiều bản ghi dữ liệu khách hàng
        /// </summary>
        /// <param name="customers">Thông tin về dữ liệu bản ghi khách hàng</param>
        /// <returns></returns>
        ServiceResult InsertMany(List<Customer> customers);

    }

}   
