using MISA.Core.Entities;
using MISA.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Lấy dữ liệu và lọc
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterString"></param>
        /// <param name="customerGroupId"></param>
        /// <returns></returns>
        FilterResponse GetByFilter(int pageSize, int pageNumber, string filterString, Guid? customerGroupId);

        /// <summary>
        /// Thêm nhiều bản ghi dữ liệu khách hàng
        /// </summary>
        /// <param name="customers">Thông tin các khách hàng</param>
        /// <returns></returns>
        int InsertMany(List<Customer> customers);

        /// <summary>
        /// Lấy thông tin về nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupName"></param>
        /// <returns></returns>
        CustomerGroup GetCustomerGroupInfo(string customerGroupName);
    }
}
