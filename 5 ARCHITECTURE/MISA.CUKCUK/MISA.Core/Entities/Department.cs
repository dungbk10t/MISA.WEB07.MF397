using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Department : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid departmentId { get; set; }
        /// <summary>
        /// Mã phòng ban 
        /// </summary>
        public string departmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string departmentName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get; set; }

        #endregion
    }
}
