using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Validations
{
    public class Validations
    {
        /// <summary>
        /// Validate kiểm tra chuỗi rỗng hay không ?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool Required(string val)
        {
            return !(val == null || val.Equals(""));
        }
        /// <summary>
        /// Validate định dạng email hợp lệ hay không ?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidateEmail(string email)
        {
            var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            var isMatch = Regex.IsMatch(email, emailFormat, RegexOptions.IgnoreCase);
            return isMatch;
        }

       
    }
}
