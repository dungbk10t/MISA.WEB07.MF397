using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : ICustomerService
    {
        ServiceResult _serviceResult;
        ICustomerRepository _customerRepository;

        public object Properties { get; private set; }

        public CustomerService(ICustomerGroupRepository customerRepository)
        {
            _serviceResult = new ServiceResult();
            _customerRepository = customerRepository;
        } 
        public CustomerService() 
        {
            _serviceResult = new ServiceResult();
        }
        public ServiceResult Add(Customer customer)
        {
            // Xử ký nghiệp vụ :

            // Kiểm tra thông tin của khách hàng đã hợp lệ hay chưa ?
            // 1. Mã khách hàng bắt buộc phải có
            if (customer.CustomerCode == "" || customer.CustomerCode == null)
            {
                var errorObj = new
                {
                    userMsg = Exceptions.Resources.EXCEPTION_ERR_NULL_CUSTOMERCODE_MSG,
                    errorCode = Exceptions.Resources.ERROR_CODE_400,
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false; 
                _serviceResult.Data = errorObj;
                return _serviceResult;
            }
            // 2. Email phải đúng định dạng
            var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            var isMatch = Regex.IsMatch(emailFormat, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isMatch == false)
            {
                var errorObj = new
                {
                    userMsg = Exceptions.Resources.EXCEPTION_ERR_EMAIL_MSG,
                    errorCode = Exceptions.Resources.ERROR_CODE_400,
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult;
            }
            _serviceResult.Data = _customerRepository.Add(customer);
            return _serviceResult;
        }
        

        public ServiceResult Update(Customer customer, Guid customerId)
        {
            // Xử ký nghiệp vụ

            // Tương tác kết nối với Database :
            _serviceResult.Data = _customerRepository.Update(customer, customerId);
            return _serviceResult; 
        }
    }
}
    