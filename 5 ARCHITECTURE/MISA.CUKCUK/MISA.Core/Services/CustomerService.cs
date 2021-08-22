using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {

        #region Fields
        ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Constructors
        public  CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region GetbyFilter Method :
        /// <summary>
        /// Lấy và lọc dữ liệu
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="filterString">Chuỗi lọc</param>
        /// <param name="customerGroupId">Id của nhóm khách hàng</param>
        /// <returns></returns>
        /// CreateBy : Phạm Tuấn Dũng (17/08/2021)
        public ServiceResult GetByFilter(int pageSize, int pageNumber, string filterString, Guid? customerGroupId)
        {
            _serviceResult.Data = _customerRepository.GetByFilter(pageSize, pageNumber, filterString, customerGroupId);
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }
        #endregion

        #region Nhập khẩu từ file Excel
        /// <summary>
        /// Import data from file to Database
        /// </summary>
        /// <param name="formfile"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            // Kiểm tra file rỗng
            if(formFile == null || formFile.Length <= 0)
            {
                return new ServiceResult
                {
                    IsValid = false,
                    Messenge = Resources.FILE_EMPTY_MSG,
                };
            }

            // Kiểm tra định dạng file có là xls, xlsx 
            if (!(Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(formFile.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)))
            {
                return new ServiceResult
                {
                    IsValid = false,
                    Messenge = Resources.NOT_SUPPORT_FILE_EXT_MSG
                };
            }
            var list = new List<Customer>();
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for(int row = 2; row <= rowCount; row++)
                    {
                        var obj = GetCustomerFromExcelRow(worksheet, row);

                        // Validate chung : 1. Kiểm tra mã khách hàng trống  2. Kiểm tra mã khách hàng trống 3. Kiểm tra định dạng email
                        _serviceResult = Validate(obj, (int)Mode.Add);
                        // Validate cho khách hàng : Nhóm khách hàng không có trong database  2. Trùng mã khách hàng trong file / database  3. Trùng số điện thoại trong file / database
                        _serviceResult = ValidateForImport(obj, worksheet, row);
                        //Kết thúc việc validate dữ liệu
                        if(!_serviceResult.IsValid)
                        {
                            obj.InValids = _serviceResult.InvalidMessage;
                        }
                        list.Add(obj);
                    }
                }
            }
            var rowAffects = _customerRepository.InsertMany(list);

            return new ServiceResult
            {
                IsValid = rowAffects > 0,
                Data = new
                {
                    RowAffects = rowAffects,
                    Result = list,
                }
            };
        }
        /// <summary>
        /// Thêm nhiều bản ghi trong một request
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public ServiceResult InsertMany(List<Customer> customers)
        {
            _serviceResult.Data = _customerRepository.InsertMany(customers);
            _serviceResult.IsValid = (int)_serviceResult.Data > 0;
            return _serviceResult;
        }
        

        /// <summary>
        /// Lấy data từ một hàng trong file để tạo đối tượng Customer
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected Customer GetCustomerFromExcelRow(ExcelWorksheet worksheet, int row)
        {
            var customerCode = worksheet.Cells[row, 1].Value;
            var fullName = worksheet.Cells[row, 2].Value;
            var memberCardCode = worksheet.Cells[row, 3].Value;
            var customerGroupName = worksheet.Cells[row, 4].Value;
            var phoneNumber = worksheet.Cells[row, 5].Value;
            var dateOfBirth = worksheet.Cells[row, 6].Value;
            var companyName = worksheet.Cells[row, 7].Value;
            var companyTaxCode = worksheet.Cells[row, 8].Value;
            var email = worksheet.Cells[row, 9].Value;
            var address = worksheet.Cells[row, 10].Value;

            var sCustomerCode = customerCode.ToString().Trim();
            var sFullName = fullName == null ? null : fullName.ToString().Trim();
            var sMemberCardCode = memberCardCode == null ? null : memberCardCode.ToString().Trim();
            var sCustomerGroupName = customerGroupName == null ? null : memberCardCode.ToString().Trim();
            var sPhoneNumber = phoneNumber == null ? null : phoneNumber.ToString().Trim().Replace(".", "");
            var sDateOfBirth = dateOfBirth == null ? null : dateOfBirth.ToString().Trim();
            var sCompanyName = companyName == null ? null : companyName.ToString().Trim();
            var sCompanyTaxCode = companyTaxCode == null ? null : companyTaxCode.ToString().Trim();
            var sEmail = email == null ? null : email.ToString().Trim();
            var sAddress = address == null ? null : address.ToString().Trim();

            DateTime dob;
            var isNullDob = false;
            string[] formats = { "dd/MM/yyyy", "MM/yyyy", "yyyy" };
            if(!DateTime.TryParseExact(sDateOfBirth, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                isNullDob = true;
            }

            return new Customer
            {
                CustomerCode = sCustomerCode,
                FullName = sFullName,
                MemberCardCode = sMemberCardCode,
                CustomerGroupName = sCustomerGroupName,
                PhoneNumber = sPhoneNumber,
                DateOfBirth = isNullDob ? null : dob,
                CompanyName = sCompanyName,
                CompanyTaxCode = sCompanyTaxCode,
                Email = sEmail,
                Address = sAddress
            };
        }
        #endregion

        #region Validate riêng của Customer
        /// <summary>
        /// Validate khi nhập khẩu từ file
        /// </summary>
        /// <param name="obj">Đối tượng chứa dữ liệu cần validate</param>
        /// <param name="worksheet">Data 1 sheet của file excel</param>
        /// <param name="row">Chỉ số dòng trong sheet</param>
        /// <returns></returns>
        protected ServiceResult ValidateForImport(Customer obj, ExcelWorksheet worksheet, int row)
        {
            // Nhóm khách hàng chưa có trong cơ sở dữ liệu
            if(_customerRepository.GetCustomerGroupInfo(obj.CustomerGroupName) == null)
            {
                _serviceResult.IsValid = false;
                _serviceResult.InvalidMessage.Add(Resources.CUS_GROUP_NOT_EXIST_MSG);
            }

            // Mã khách hàng đã tồn tại trong file
            if (IsFoundValueInFile(worksheet, 1, row, obj.CustomerCode))
            {
                _serviceResult.IsValid = false;
                _serviceResult.InvalidMessage.Add(Resources.CUS_CODE_EXIST_IN_FILE_MSG);
            }

            // Số điện thoại đã tồn tại trong cơ sở dữ liệu
            if (!string.IsNullOrEmpty(obj.PhoneNumber) && _customerRepository.GetByPhoneNumber(obj.PhoneNumber) != null)
            {
                _serviceResult.IsValid = false;
                _serviceResult.InvalidMessage.Add(Resources.PHONE_NUMBER_DULICATE_MSG);
            }
            // Số điện thoại đã tồn tại trong file
            if (!string.IsNullOrEmpty(obj.PhoneNumber) && IsFoundValueInFile(worksheet, 5, row, obj.PhoneNumber))
            {
                _serviceResult.IsValid = false;
                _serviceResult.InvalidMessage.Add(Resources.PHONE_NUMBER_EXIST_IN_FILE_MSG);
            }

            return _serviceResult;
        }
        /// <summary>
        /// Tìm kiếm một giá trị trong file excel theo cột
        /// </summary>
        /// <param name="worksheet">Chỉ số cột muốn tìm</param>
        /// <param name="column">Dòng hiện tại, chỉ tìm kiếm các dòng khác dòng này</param>
        /// <param name="currentRow">Giá trị cần tìm</param>
        /// <param name="compareVal">True- Giá trị cần tìm xuất hiện nhiều hơn 1 lần trong sheet</param>
        /// <returns></returns>
        protected bool IsFoundValueInFile(ExcelWorksheet worksheet, int column, int currentRow, string compareVal)
        {
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                if (row == currentRow) continue;
                var valObj = worksheet.Cells[row, column].Value;
                string val = valObj == null ? null : valObj.ToString().Trim();

                if (!string.IsNullOrEmpty(val) && val.Equals(compareVal)) return true;
            }
            return false;
        }

        #endregion

    }
}
    