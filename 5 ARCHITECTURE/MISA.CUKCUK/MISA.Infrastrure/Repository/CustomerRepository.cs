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
using MISA.Core.MISAAttribute;

namespace MISA.Infrastrure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        
        #region GetByFilter Method : Phương thức lấy và lọc dữ liệu khách hàng (Phương thức riêng)
        /// <summary>
        /// Lọc dữ liệu theo chuỗi tìm kiếm hoặc nhóm khách hàng, kết hợp phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <param name="filterString">Dữ liệu lọc</param>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns></returns>
        public FilterResponse GetByFilter(int pageSize, int pageNumber, string filterString, Guid? customerGroupId)
        {
            var sqlSelectCount = "SELECT COUNT(*) FROM Customer c";
            var sqlQuery = $"SELECT c.*, cg.CustomerGroupName, CASE " +
                           $" WHEN c.Gender = 0 THEN 'Nữ' " +
                           $" WHEN c.Gender = 1 THEN 'Nam' " +
                           $" ELSE 'Không xác định' " +
                           $" END as GenderName" +
                           $" FROM Customer c LEFT JOIN CustomerGroup cg ON cg.CustomerGroupId=c.CustomerGroupId ";
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageStart", pageNumber * pageSize);
            if (filterString == null) filterString = "";
            var sqlQueryWhere = " WHERE ( UPPER(c.FullName) LIKE CONCAT('%',@filter,'%') " +
                            " OR UPPER(c.CustomerCode) LIKE CONCAT('%',@filter,'%') " +
                            " OR c.PhoneNumber LIKE CONCAT('%',@filter,'%') )";
            parameters.Add("@filter", filterString.ToUpper());

            // Lọc theo id nhóm khách hàng
            if (customerGroupId != null)
            {
                sqlQueryWhere += " AND c.CustomerGroupId=@CustomerGroupId ";
                parameters.Add("@CustomerGroupId", customerGroupId);
            }
            sqlQuery += sqlQueryWhere;
            sqlSelectCount += sqlQueryWhere;

            // Sắp xếp theo chiều giảm dần mã khách hàng
            sqlQuery += " ORDER BY c.CustomerCode DESC";

            // Phân trang cho kết quả truy vấn
            sqlQuery += (pageSize > 0) ? " LIMIT @pageStart, @pageSize;" : "";
            sqlSelectCount += " ORDER BY c.CustomerId";
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
        /// <summary>
        /// Lấy dữ liệu nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupName">Thông tin dữ liệu về nhóm khách hàng</param>
        /// <returns></returns>
        public CustomerGroup GetCustomerGroupInfo(string customerGroupName)
        {
            // Lấy dữ liệu
            var sqlQuery = $"SELECT * FROM {_entityName}Group WHERE {_entityName}GroupName = @group";
            // Khai báo DynamicParam :
            DynamicParameters dynamicParam = new DynamicParameters();
            // Thêm param tương ứng với mỗi property của đối tượng :
            dynamicParam.Add("@group", customerGroupName);
            // Lấy dữ liệu và phản hồi cho client : 
            _dbConnection.Open();
            var customerGroup = _dbConnection.QueryFirstOrDefault<CustomerGroup>(sqlQuery, param: dynamicParam);
            _dbConnection.Close();
            return customerGroup;
        }

        #endregion

        #region Import Data từ file
        /// <summary>
        /// Thêm nhiều bản ghi dữ liệu khách hàng vào file
        /// </summary>
        /// <param name="customers">Thông tin các khách hàng</param>
        /// <returns></returns>
        public int InsertMany(List<Customer> customers)
        {
            var procName = "Proc_ImportCustomer";
            var rowAffects = 0;

            _dbConnection.Open();

            foreach (var customer in customers)
            {
                // Nếu customer ko hợp lệ thì bỏ qua
                if (customer.InValids != null) continue;

                using (var trans = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Đọc từng property của object
                        var properties = customer.GetType().GetProperties();
                        var parameters = new DynamicParameters();

                        foreach (var prop in properties)
                        {
                            var propAttr = prop.GetCustomAttributes(typeof(MISAColumnForImport), false);

                            // nếu k phải cột lấy từ file thì bỏ qua
                            if (propAttr.Length == 0) continue;

                            // Tên thuộc tính
                            var propName = prop.Name;

                            // Giá tri thuộc tính
                            var propValue = prop.GetValue(customer);

                            parameters.Add($"@{propName}", propValue);
                        }

                        rowAffects += _dbConnection.Execute(
                            procName,
                            customer,
                            commandType: System.Data.CommandType.StoredProcedure,
                            transaction: trans
                        );
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            _dbConnection.Close();

            return rowAffects;
        }
        #endregion
    }
}
