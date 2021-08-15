﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
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
