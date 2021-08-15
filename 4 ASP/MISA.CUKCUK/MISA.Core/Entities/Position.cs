using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Position : BaseEntity
    {
        /// <summary>
        /// Khóa chính vị trí 
        /// </summary>
        public Guid positionId { get; set; }
        /// <summary>
        /// Mã phòng ban 
        /// </summary>
        public string positionCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string positionName { get; set; }

        public string Decription { get; set; }
    }
}
