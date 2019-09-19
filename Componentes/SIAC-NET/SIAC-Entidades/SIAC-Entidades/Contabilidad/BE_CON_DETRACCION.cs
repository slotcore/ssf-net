using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_DETRACCION
    {
        private int _n_id;
        private string _c_des;
        private double _n_tasa;
        private double _n_impbase;
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
        public double n_tasa
        {
            get { return _n_tasa; }
            set { _n_tasa = value; }
        }
        public double n_impbase
        {
            get { return _n_impbase; }
            set { _n_impbase = value; }
        }
    }
}
