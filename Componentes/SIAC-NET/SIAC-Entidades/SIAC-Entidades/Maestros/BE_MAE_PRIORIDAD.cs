using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Maestros
{
    public class BE_MAE_PRIORIDAD
    {
        private int _n_id;
        private string _c_des;
        private int _n_idfor;
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
        public int n_idfor
        {
            get { return _n_idfor; }
            set { _n_idfor = value; }
        }
    }
}