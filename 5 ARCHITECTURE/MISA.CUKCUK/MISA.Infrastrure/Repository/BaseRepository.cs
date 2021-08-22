using Dapper;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Core.MISAAttribute;
using Microsoft.Extensions.Configuration;

namespace MISA.Infrastrure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region Fields
        protected readonly string _connectionString;
        protected readonly IDbConnection _dbConnection;
        protected readonly string _entityName;
        #endregion

        #region Constructors

        public BaseRepository(/*IConfiguration configuration*/)
        {
            // Khai báo thông tin trên Database :
            /*configuration.GetConnectionString("CukCukDatabase");*/
            _connectionString = MISA.Core.Exceptions.Resources.connectionString;
            //  Khởi tạo đối tượng kết nói với Database :
            _dbConnection = new MySqlConnection(_connectionString);

            _entityName = typeof(MISAEntity).Name;
        }
        #endregion

        #region GET methods : Lấy dữ liệu
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public virtual List<MISAEntity> GetAll()
        {
            var sqlCommand = $"SELECT * FROM {_entityName}";
            _dbConnection.Open();
            var entities = (List<MISAEntity>)_dbConnection.Query<MISAEntity>(sqlCommand);
            _dbConnection.Close();
            return entities;
        }
        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="entityId">Id của bản ghi dữ liệu (nhân viên/ khách hàng)</param>
        /// <returns></returns>
        public virtual MISAEntity GetById(Guid entityId)
        {
            // Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM {_entityName} WHERE {_entityName}Id = @entityId";
            // Khai báo DynamicParam : 
            DynamicParameters dynamicParam = new DynamicParameters();
            // Thêm param tương ứng với mỗi property của đối tượng : 
            dynamicParam.Add($"@entityId", entityId);
            // Lấy dữ liệu và phản hồi cho client : 
            _dbConnection.Open();
            return _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: dynamicParam);
        }
        /// <summary>
        /// Lấy dữ liệu theo mã Code
        /// </summary>
        /// <param name="entityCode">Mã của bản ghi dữ liệu</param>
        /// <returns></returns>
        public MISAEntity GetByCode(string entityCode)
        {
            // Lấy dữ liệu : 
            var sqlCommand = $"SELECT * FROM {_entityName} WHERE {_entityName}Code = @entityCode";
            // Khai báo DynamicParam :
            DynamicParameters dynamicParam = new DynamicParameters();
            // Thêm param tương ứng với mỗi property của đối tượng :
            dynamicParam.Add("@entityCode", entityCode);
            // Lấy dữ liệu và phản hồi cho client : 
            _dbConnection.Open();
            var res = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: dynamicParam);
            _dbConnection.Close();
            return res;
        }
        /// <summary>
        /// Lấy dữ liệu theo số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns></returns>
        public MISAEntity GetByPhoneNumber(string phoneNumber)
        {
            // Lấy dữ liệu
            var sqlQuery = $"SELECT * FROM {_entityName} WHERE PhoneNumber = @phone";
            // Khai báo DynamicParam :
            DynamicParameters dynamicParam = new DynamicParameters();
            // Thêm param tương ứng với mỗi property của đối tượng :
            dynamicParam.Add("@phone", phoneNumber);
            // Lấy dữ liệu và phản hồi cho client : 
            _dbConnection.Open();
            var res = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlQuery, param: dynamicParam);
            _dbConnection.Close();
            return res;
        }
        #endregion

        #region POST Method : Thêm mới
        /// <summary>
        /// Thêm mới một bản ghi dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin về bản ghi dữ liệu sẽ được thêm mới</param>
        /// <returns></returns>
        public virtual int Add(MISAEntity entity)
        {
            // 1. Thêm dữ liệu vào trong database :
            // Khai báo Name và Param của các cột
            var columnsName = new List<string>();
            var columnsParam = new List<string>();
            // Khai báo DynamicParam : 
            DynamicParameters dynamicParam = new DynamicParameters();
            // Đọc từng property của object : 
            var properties = entity.GetType().GetProperties();
            // Duyệt từng property của object :
            foreach (var prop in properties)
            {
                // Kiểm tra : 
                var propAttr = prop.GetCustomAttributes(typeof(MISANotMap), false);
                if (propAttr.Length != 0) continue;
                // Lấy ra tên  của property :
                var propName = prop.Name;
                // Lấy giá trị của property :
                var propValue = prop.GetValue(entity);
                // Lấy ra kiểu dữ liệu của property : 
                var propType = prop.PropertyType;
                // Tạo id mới
                if (propName.Equals($"{_entityName}Id") && propType == typeof(Guid))
                {
                    propValue = Guid.NewGuid();
                }
                // Thêm tham số tương ứng với mỗi thuộc tính của đối tượng :
                dynamicParam.Add($"{propName}", propValue);
                columnsName.Add(propName);
                columnsParam.Add($"@{propName}");
            }
            // Truy vấn Database thêm dữ liệu mới
            var sqlCommand = $"INSERT INTO {_entityName} ({String.Join(", ", columnsName.ToArray())})" +
                             $"VALUES({String.Join(", ", columnsParam.ToArray())})";
            _dbConnection.Open();
            var transition = _dbConnection.BeginTransaction();
            var res = _dbConnection.Execute(sqlCommand, param: dynamicParam, transaction: transition);
            transition.Commit();
            _dbConnection.Close();
            return res;
        }
        #endregion

        #region PUT Method : Cập nhật
        /// <summary>
        /// Cập nhật bản ghi dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin bản ghi dữ liệu</param>
        /// <param name="entityId">Id của bản ghi dữ liệu</param>
        /// <returns></returns>
        public virtual int Update(MISAEntity entity, Guid entityId)
        {
            // Khai báo queryLine 
            var queryLine = new List<string>();
            // Khai báo DynamicParam : 
            var dynamicParam = new DynamicParameters();
            // Đọc từng property của object : 
            var properties = entity.GetType().GetProperties();
            // Duyệt từng property của object :
            foreach (var prop in properties)
            {
                // Kiểm tra : 
                var propAttr = prop.GetCustomAttributes(typeof(MISANotMap), false);
                if (propAttr.Length != 0) continue;
                // Lấy ra tên  của property :
                var propName = prop.Name;
                // Lấy giá trị của property :
                var propValue = prop.GetValue(entity);
                //// Lấy ra kiểu dữ liệu của property : 
                //var propType = prop.PropertyType;
                // 
                if (propName.Equals($"{_entityName}Id"))
                {
                    propValue = entityId;
                }
                // Thêm tham số tương ứng với mỗi thuộc tính của đối tượng :
                queryLine.Add($"{propName} = @{propName}");
                dynamicParam.Add($"{propName}", propValue);
            }
            // Truy vấn Database cập nhật dữ liệu bản ghi: 
            dynamicParam.Add("@oldEntityId", entityId);
            var sqlCommand = $"UPDATE {_entityName} SET {String.Join(", ", queryLine.ToArray())} " +
                $"WHERE {_entityName}Id = @oldEntityId";

            _dbConnection.Open();
            var transition = _dbConnection.BeginTransaction();
            var res = _dbConnection.Execute(sqlCommand, param: dynamicParam, transaction: transition);
            transition.Commit();
            _dbConnection.Close();
            return res;
        }
        #endregion

        #region DELETE Methods : Xóa dữ liệu
        /// <summary>
        /// Xóa 1 bản ghi dữ liệu
        /// </summary>
        /// <param name="entityId">Id của bản ghi dữ liệu cần xóa</param>
        /// <returns></returns>
        public virtual int Delete(Guid entityId)
        {
            // Khai báo Paramters : 
            DynamicParameters parameters = new DynamicParameters();
            // Truy vấn Database xóa dữ liệu : 
            var sqlCommand = $"DELETE FROM {_entityName} WHERE {_entityName}Id = @entityId";
            // Thêm tham số tương ứng với mỗi thuộc tính của đối tượng :
            parameters.Add("@entityId", entityId);

            _dbConnection.Open();
            var transition = _dbConnection.BeginTransaction();
            var res = _dbConnection.Execute(sqlCommand, param: parameters, transaction: transition);
            transition.Commit();
            _dbConnection.Close();
            return res;
        }
        /// <summary>
        /// Xóa nhiều bản ghi dữ liệu 
        /// </summary>
        /// <param name="entityIds">List danh sách các id của các bản ghi cần xóa</param>
        /// <returns></returns>
        public virtual int DeleteMany(List<Guid> entityIds)
        {
            // Khai báo Paramters : 
            DynamicParameters parameters = new DynamicParameters();
            // Khai báo Param name :
            var paramName = new List<string>();
            // Duyệt vòng lặp thêm tham số tương ứng với mỗi thuộc tính của đối tượng :
            for (int i = 0; i < entityIds.Count; i++)
            {
                var id = entityIds[i];
                paramName.Add($"@id{i}");
                parameters.Add($"@id{i}", id.ToString());
            }
            // Truy vấn Database xóa nhiều bản ghi dữ liệu : 
            var sqlCommand = $"DELETE FROM {_entityName} WHERE {_entityName}Id IN ({String.Join(", ", paramName.ToArray())})";
            _dbConnection.Open();
            var transition = _dbConnection.BeginTransaction();
            var res = _dbConnection.Execute(sqlCommand, param: parameters, transaction: transition);
            transition.Commit();
            _dbConnection.Close();
            return res;
        }
        #endregion
    }
}
