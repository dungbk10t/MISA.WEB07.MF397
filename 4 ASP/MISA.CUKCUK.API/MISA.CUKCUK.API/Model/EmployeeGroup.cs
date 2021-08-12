using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Model
{
    public class EmployeeGroup : BaseEntity
    {
        #region Property
        public Guid EmployeeGroupId { get; set; }
        public string EmployeeGroupName { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
