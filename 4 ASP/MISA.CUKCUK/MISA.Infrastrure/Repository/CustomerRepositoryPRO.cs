using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastrure.Repository
{
    public class CustomerRepositoryPRO : ICustomerRepository
    {
        public int Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public List<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }

        public object Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
