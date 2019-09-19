using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PROGRAMADET
    {
        private int _n_idpro;
        private int _n_idordpro;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idordpro
        {
            get { return _n_idordpro; }
            set { _n_idordpro = value; }
        }
    }
}