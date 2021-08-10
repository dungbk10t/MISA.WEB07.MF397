using System.Collections.Generic;

namespace MISA.CukCuk.API.Controllers
{
    internal class EmployeeFilterResponse
    {
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public List<object> Data { get; set; }
    }
}