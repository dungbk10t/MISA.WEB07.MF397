using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// Tính hợp lệ của dữ liệu
        /// </summary>
        public bool IsValid { get; set; } = true;
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data  { get; set; }
        /// <summary>
        /// Thông điệp trả về
        /// </summary>
        public string Messenge { get; set; }
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Id truy vết lỗi
        /// </summary>
        public Guid? TraceId  { get; set; }
        /// <summary>
        /// List những thông báo về validate dữ liệu
        /// </summary>
        public List<string> InvalidMessage { get; set; }

    }
}
