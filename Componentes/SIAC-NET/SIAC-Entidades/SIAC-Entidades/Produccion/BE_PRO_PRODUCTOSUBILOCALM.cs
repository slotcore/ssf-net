using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCTOSUBILOCALM
    {
        private int _n_idpro;
        private int _n_idloc;
        private int _n_idalm;
        private double _n_canalmmin;
        private double _n_canalmmax;

        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idloc
        {
            get { return _n_idloc; }
            set { _n_idloc = value; }
        }
        public int n_idalm
        {
            get { return _n_idalm; }
            set { _n_idalm = value; }
        }
        public double n_canalmmin
        {
            get { return _n_canalmmin; }
            set { _n_canalmmin = value; }
        }
        public double n_canalmmax
        {
            get { return _n_canalmmax; }
            set { _n_canalmmax = value; }
        }
    }
}
