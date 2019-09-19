using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_REGPERCEPCIONDET
    {
        private int _n_idper;
        private int _n_iddoc;
        private double _n_impdoc;
        private double _n_impper;
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
        public double n_impdoc
        {
            get { return _n_impdoc; }
            set { _n_impdoc = value; }
        }
        public double n_impper
        {
            get { return _n_impper; }
            set { _n_impper = value; }
        }
    }
}
