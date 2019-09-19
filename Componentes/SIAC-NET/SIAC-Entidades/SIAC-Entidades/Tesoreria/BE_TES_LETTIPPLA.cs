using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_LETTIPPLA
    {
        private int _n_id;
        private string _c_des;
        private int _n_numdia;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public int n_numdia
        {
            get { return _n_numdia; }
            set { _n_numdia = value; }
        }
    }
}
