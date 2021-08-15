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

namespace MISA.Infrastrure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int Add(Employee employee)
        {
            // Khởi tạo Id mới cho đối tượng : 
            employee.EmployeeId = Guid.NewGuid();
            // 1. Khai báo thông tin database :
            var connectionString = "Host = 47.241.69.179;" +
                 "Database = MISA.CukCuk_Demo_NVMANH;" +
                 "User id = dev;" +
                 "Password = 12345678;";

            // 2. Khởi tạo đối tượng kết nói với Database :
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Khai báo DynamicParam : 
            var dynamicParam = new DynamicParameters();
            // 3. Thêm dữ liệu vào trong database :
            var columnsName = string.Empty;
            var columnsParam = string.Empty;

            // Đọc từng property của object : 
            var propertise = employee.GetType().GetProperties();
            // Duyệt từng property của object :
            foreach (var prop in propertise)
            {
                
                // Lấy tên prop : 
                var propName = prop.Name;
                // Lấy value của prop : 
                var propValue = prop.GetValue(employee);
                // Lấy kiểu dữ liệu của prop : 
                var propType = prop.PropertyType;
                // Thêm param tương ứng với mỗi property của đối tượng : 
                dynamicParam.Add($"{propName}", propValue);
                columnsName += $"{propName},";
                columnsParam += $"@{propName},";
            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

            // C2 : 

            var sqlCommand = $"INSERT INTO Employee({columnsName}) VALUES ({columnsParam})";
            var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam, commandType:CommandType.Text);
            //var storeName = "Proc_InsertEmployee";
            return rowEffects;
        }

        public int Delete(Guid employeeId)
        {
            throw new NotImplementedException();    
        }
        public List<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public int Update(Employee employee, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public object Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
