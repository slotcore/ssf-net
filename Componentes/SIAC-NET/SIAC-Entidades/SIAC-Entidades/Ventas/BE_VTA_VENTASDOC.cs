using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENTASDOC
    {
        private Int64 _n_idvta;
        private int _n_idtipdoc;
        private int _n_iddoc;
        public Int64 n_idvta
        {
            get { return _n_idvta; }
            set { _n_idvta = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
    }
}
