using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        public EmployeeService(IEmployeeGroupRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public EmployeeService()
        {
            _serviceResult = new ServiceResult();

        }
        public ServiceResult Add(Employee employee)
        {
            // Xử ký nghiệp vụ

            // Tương tác kết nối với Database :
            
            _serviceResult.Data = _employeeRepository.Add(employee);
            return _serviceResult;
        }

        public ServiceResult Update(Employee employee, Guid employeeId)
        {
            // Xử ký nghiệp vụ

            // Tương tác kết nối với Database :
            _serviceResult.Data = _employeeRepository.Update(employee, employeeId);
            return _serviceResult;
        }
    }
}
