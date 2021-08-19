using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Core.Responses;
using Microsoft.Extensions.Configuration;

namespace MISA.Infrastrure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        //public CustomerRepository(IConfiguration configuration):base(configuration)
        //{

        //}
        #region GetByFilter Method : Phương thức lấy và lọc dữ liệu khách hàng (Phương thức riêng)
        public FilterResponse GetByFilter(int pageSize, int pageNumber, string filterString, Guid? customerGroupId)
        {
            var sqlSelectCount = "SELECT COUNT(*) FROM Customer c";
            var sqlQuery = $"SELECT c.*, cg.CustomerGroupName, CASE " +
                           $"WHEN c.Gender = 0 THEN 'Nữ' " +
                           $"WHEN c.Gender = 1 THEN 'Nam' " +
                           $"ELSE 'Không xác định' " +
                           $"END as GenderName" +
                           $"FROM Customer c LEFT JOIN CustomerGroup cg ON cg.CustomerGroupId=c.CustomerGroupId";
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageStart", pageNumber * pageSize);
            if (filterString == null) filterString = "";
            var sqlQueryWhere = "WHERE ( UPPER(c.FullName) LIKE CONCAT('%',@filter,'%') " +
                            "OR UPPTER(c.CustomerCode) LIKE CONCAT('%',@filter,'%') " +
                            "OR c.PhoneNumber LIKE CONCAT('%',@filter,'%') )";
            parameters.Add("@filter", filterString.ToUpper());

            // Lọc theo id nhóm khách hàng
            if (customerGroupId != null)
            {
                sqlQueryWhere += "AND c.CustomerGroupId=@CustomerGroupId ";
                parameters.Add("@CustomerGroupId", customerGroupId);
            }
            sqlQuery += sqlQueryWhere;
            sqlSelectCount += sqlQueryWhere;

            // Sắp xếp theo chiều giảm dần mã khách hàng
            sqlQuery += "ORDER BY c.CustomerCode DESC";

            // Phân trang cho kết quả truy vấn
            sqlQuery += (pageSize > 0) ? "LIMIT @pageStart, @pageSize;" : "";
            sqlSelectCount += "ORDER BY c.CustomerId";
            // Thực hiện truy vấn dữ liệu
            var customers = _dbConnection.Query<object>(sqlQuery, param: parameters);

            if (customers == null)
            {
                return new FilterResponse
                {
                    TotalRecord = 0,
                    TotalPage = 0,
                    Data = null
                };
            }

            var totalRecord = _dbConnection.QueryFirstOrDefault<int>(sqlSelectCount, param: parameters);
            var totalPage = (int)(totalRecord / pageSize) + ((totalRecord % pageSize != 0) ? 1 : 0);
            return new FilterResponse
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = (List<object>)customers
            };
        }
        #endregion
    }
}
