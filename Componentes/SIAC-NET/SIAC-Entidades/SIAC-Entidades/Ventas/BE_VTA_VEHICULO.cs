using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VEHICULO
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_marca;
        private string _c_numpla;
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
        public string c_marca
        {
            get { return _c_marca; }
            set { _c_marca = value; }
        }
        public string c_numpla
        {
            get { return _c_numpla; }
            set { _c_numpla = value; }
        }
    }
}
