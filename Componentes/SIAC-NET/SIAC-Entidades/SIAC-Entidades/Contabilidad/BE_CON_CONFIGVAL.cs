using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_CONFIGVAL
    {
        private int _n_id;
        private int _n_idemp;
        private string _c_descrip;
        private string _c_obs;
        private string _c_metval;
        private string _c_factdist;
        private string _c_tipdist;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }

        public string c_descrip
        {
            get { return _c_descrip; }
            set { _c_descrip = value; }
        }

        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }

        public string c_metval
        {
            get { return _c_metval; }
            set { _c_metval = value; }
        }
        public string c_factdist
        {
            get { return _c_factdist; }
            set { _c_factdist = value; }
        }
        public string c_tipdist
        {
            get { return _c_tipdist; }
            set { _c_tipdist = value; }
        }
    }
}
