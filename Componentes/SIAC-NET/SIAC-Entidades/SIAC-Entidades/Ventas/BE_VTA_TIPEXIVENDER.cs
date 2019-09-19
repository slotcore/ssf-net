using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_TIPEXIVENDER
    {
        private int _n_idemp;
        private int _n_idtipexi;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idtipexi
        {
            get { return _n_idtipexi; }
            set { _n_idtipexi = value; }
        }
    }
}
