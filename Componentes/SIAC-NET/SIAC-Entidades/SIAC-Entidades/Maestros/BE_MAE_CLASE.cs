using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Maestros
{
    public class BE_MAE_CLASE
    {
        private int _n_idemp;
        private int _n_idtipexi;
        private int _n_idfam;
        private int _n_id;
        private string _c_des;
        private string _c_pre;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idtipexi
        {
            get { return _n_idtipexi; }
            set { _n_idtipexi = value; }
        }
        public int n_idfam
        {
            get { return _n_idfam; }
            set { _n_idfam = value; }
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
        public string c_pre
        {
            get { return _c_pre; }
            set { _c_pre = value; }
        }
    }
}
