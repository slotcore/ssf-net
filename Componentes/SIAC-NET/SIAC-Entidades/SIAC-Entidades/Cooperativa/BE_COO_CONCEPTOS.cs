using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_CONCEPTOS
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_cod;
        private string _c_des;
        private double _n_imp;
        private int _n_afeigv;

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
        public string c_cod
        {
            get { return _c_cod; }
            set { _c_cod = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public double n_imp
        {
            get { return _n_imp; }
            set { _n_imp = value; }
        }
        public int n_afeigv
        {
            get { return _n_afeigv; }
            set { _n_afeigv = value; }
        }
    }
}
