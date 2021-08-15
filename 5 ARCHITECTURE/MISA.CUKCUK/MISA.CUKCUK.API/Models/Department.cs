using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Models
{
    public class Department : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính phòng ban
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

        public string Decription { get; set; }
        #endregion
    }
}