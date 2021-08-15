using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        //Employee GetById(Guid employeeId);
        int Add(Employee employee);
        int Update(Employee employee, Guid employeeId);
        //int Delete(Guid employeeId);
    }
}
