using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_CLIENTESPLACAS
    {
        private int _n_idcli;
        private string _c_numpla;
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public string c_numpla
        {
            get { return _c_numpla; }
            set { _c_numpla = value; }
        }
    }
}
