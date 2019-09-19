using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_INVENTARIOUNIMED_CONSULTA : BE_ALM_INVENTARIOUNIMED
    {
        private string _c_dessun;
        private string _c_abrsun;
        private string _c_desunimedbas;

        public string c_dessun
        {
            get { return _c_dessun; }
            set { _c_dessun = value; }
        }
        public string c_abrsun
        {
            get { return _c_abrsun; }
            set { _c_abrsun = value; }
        }
        public string c_desunimedbas
        {
            get { return _c_desunimedbas; }
            set { _c_desunimedbas = value; }
        }
    }
}
