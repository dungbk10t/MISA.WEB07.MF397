using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Responses
{
    public class FilterResponse
    {
        /// <summary>
        /// Tổng số trang
        /// </summary>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        public int TotalRecord { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        public int TotalPage { get; set; }
        /// <summary>
        /// Dữ liệu
        /// </summary>
        /// CreatedBy : Phạm Tuấn Dũng (16/08/2021) 
        public List<Object> Data { get; set; }
    }
}
