using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class BaseEntity
    {
        public DateTime? createdDate { get; set; } 
        public string createdBy { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string modifiedBy { get; set; }
    }
}
