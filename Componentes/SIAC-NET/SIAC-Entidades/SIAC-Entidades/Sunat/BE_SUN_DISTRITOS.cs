using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Sunat
{
    public class BE_SUN_DISTRITOS
    {
        private int _n_iddep;
        private int _n_idpro;
        private int _n_id;
        private string _c_des;
        private string _c_codsun;
        public int n_iddep
        {
            get { return _n_iddep; }
            set { _n_iddep = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
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
        public string c_codsun
        {
            get { return _c_codsun; }
            set { _c_codsun = value; }
        }
    }
}
