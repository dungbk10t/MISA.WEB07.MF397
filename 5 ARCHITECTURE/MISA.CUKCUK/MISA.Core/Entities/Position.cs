using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Position : BaseEntity
    {
        #region Propeties
        /// <summary>
        /// Khóa chính vị trí 
        /// </summary>
        public Guid positionId { get; set; }
        /// <summary>
        /// Mã vị trí/ chức vụ
        /// </summary>
        public string positionCode { get; set; }
        /// <summary>
        /// Tên vị trí/ chức vụ
        /// </summary>
        public string positionName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Decription { get; set; }
        #endregion
    }
}
