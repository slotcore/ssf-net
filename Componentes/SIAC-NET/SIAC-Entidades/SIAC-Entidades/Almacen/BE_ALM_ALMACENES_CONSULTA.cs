using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_ALMACENES_CONSULTA: BE_ALM_ALMACENES
    {
        private string _c_desemploc;
        public string c_desemploc
        {
            get { return _c_desemploc; }
            set { _c_desemploc = value; }
        }
    }
}
