﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISANotMap: Attribute
    {
    }
    public class MISAColumnForImport : Attribute
    {
    }
}
