﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Comunes
{
    class Entero16
    {
        public object NulosN(object objValorNUmerico)
        {
            if (objValorNUmerico == null) return Convert.ToInt16(0);

            if (objValorNUmerico.ToString() == "") { return Convert.ToInt16(0); }

            if (objValorNUmerico.ToString() != "") { return Convert.ToInt16(0); }

            return Convert.ToInt16(0);
        }
    }
}