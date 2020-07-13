using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_CARGOS
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_des;
        private string _c_abr;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
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

        public string c_abr
        {
            get { return _c_abr; }
            set { _c_abr = value; }
        }

    }
}
