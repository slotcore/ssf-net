using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_REGRETENCIONDET
    {
        
        private int _n_idret;
        private int _n_iddoc;
        private double _n_impcob;
        private double _n_impret;
        public int n_idret
        {
            get { return _n_idret; }
            set { _n_idret = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
        public double n_impcob
        {
            get { return _n_impcob; }
            set { _n_impcob = value; }
        }
        public double n_impret
        {
            get { return _n_impret; }
            set { _n_impret = value; }
        }
    }
}
