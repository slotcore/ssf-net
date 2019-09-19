using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_LETRASDOC
    {
        private int _n_idlet;
        private int _n_iddoc;
        private double _n_impfin;
        public int n_idlet
        {
            get { return _n_idlet; }
            set { _n_idlet = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
        public double n_impfin
        {
            get { return _n_impfin; }
            set { _n_impfin = value; }
        }
    }
}
