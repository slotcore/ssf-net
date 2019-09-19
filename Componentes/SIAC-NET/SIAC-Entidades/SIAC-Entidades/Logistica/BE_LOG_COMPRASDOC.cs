using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Logistica
{
    public class BE_LOG_COMPRASDOC
    {
        int _n_idcom;
        int _n_idtipdoc;
        int _n_iddoc;

        public int n_idcom
        {
            get { return _n_idcom; }
            set { _n_idcom = value; }
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
