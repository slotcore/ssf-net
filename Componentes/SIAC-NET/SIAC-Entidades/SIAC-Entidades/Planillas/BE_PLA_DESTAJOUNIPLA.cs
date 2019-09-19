using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_DESTAJOUNIPLA
    {
        private int _n_idpla;
        private int _n_idplaori;

        public int n_idpla
        {
            get { return _n_idpla; }
            set { _n_idpla = value; }
        }
        public int n_idplaori
        {
            get { return _n_idplaori; }
            set { _n_idplaori = value; }
        }
    }
}
