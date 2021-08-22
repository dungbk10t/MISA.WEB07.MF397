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
using MISA.Core.MISAAttribute;
using System.Reflection;
using MISA.Core.Responses;
using Microsoft.Extensions.Configuration;

namespace MISA.Infrastrure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Các phương thức GET của riêng Employee
        public FilterResponse GetByFilter(int pageSize, int pageNumber, string filterString, Guid? departmentId, Guid? positionId)
        {
            
            /// <summary>
            /// Lọc dữ liệu theo chuỗi tìm kiếm hoặc nhóm nhân viên, kết hợp phân trang
            /// </summary>
            /// <param name="pageSize">Số bản ghi 1 trang</param>
            /// <param name="pageNumber">Chỉ số trang</param>
            /// <param name="filterString">Chuỗi tìm kiếm</param>
            /// <param name="employeeGroupId">Id nhóm nhân viên</param>
            /// <returns></returns>
            /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
            var sqlSelectCount = "SELECT COUNT(*) FROM Employee e ";

            var sqlQuery = $"SELECT e.*, d.DepartmentName, p.PositionName, CASE " +
                                $"WHEN e.Gender = 0 THEN 'Nữ' " +
                                $"WHEN e.Gender = 1 THEN 'Nam' " +
                                $"ELSE 'Không xác định' " +
                                $"END as GenderName " +
                                $"FROM Employee e LEFT JOIN Department d ON d.DepartmentId=e.DepartmentId " +
                                $"LEFT JOIN Position p ON p.PositionId=e.PositionId ";
            var parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageStart", pageNumber * pageSize);

            if (filterString == null) filterString = "";
            var sqlWhere = "WHERE ( UPPER(e.FullName) LIKE '@filter' " +
                        "OR UPPER(e.EmployeeCode) LIKE '@filter' " +
                        "OR e.PhoneNumber LIKE '@filter') ";

            parameters.Add("@filter", $"%{filterString.ToUpper()}%");

            // Lọc theo id phòng ban và vị trí
            if (departmentId != null)
            {
                sqlWhere += "AND e.DepartmentId=@departmentId ";
                parameters.Add("@departmentId", departmentId);
            }
            if (positionId != null)
            {
                sqlWhere += "AND e.PositionId=@positionId ";
                parameters.Add("@positionId", positionId);
            }

            sqlQuery += sqlWhere;
            sqlSelectCount += sqlWhere;

            // Sắp xếp theo chiều giảm dần mã nv
            sqlQuery += "ORDER BY e.EmployeeCode DESC ";

            // Phân trang cho kết quả truy vấn
            sqlQuery += (pageSize > 0) ? "LIMIT @pageStart, @pageSize;" : "";
            sqlSelectCount += "ORDER BY e.EmployeeId";

            // Thực hiện truy vấn lấy dữ liệu
            var employees = _dbConnection.Query<object>(sqlQuery, param: parameters);

            if (employees == null)
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
                Data = (List<object>)employees
            };
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : Phạm Tuấn Dũng (19/08/2021)
        public string GetNewCode()
        {
            var sqlQuery = "SELECT e.EmployeeCode FROM Employee e ORDER BY e.EmployeeCode DESC LIMIT 1";
            var employeeCode = _dbConnection.QueryFirstOrDefault<string>(sqlQuery);
            int employeeNumber;
            string newEmployeeCode;

            if (employeeCode != null)
            {
                employeeNumber = int.Parse(employeeCode.Split("-")[1]);
                employeeNumber++;
                newEmployeeCode = $"NV-{employeeNumber.ToString().PadLeft(5, '0')}";
            }
            else
            {
                newEmployeeCode = "NV00001";
            }

            return newEmployeeCode;
        }
    #endregion
    }
}
