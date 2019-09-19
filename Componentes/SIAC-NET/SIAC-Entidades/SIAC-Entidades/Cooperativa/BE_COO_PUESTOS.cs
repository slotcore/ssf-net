using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_PUESTOS
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_puesto;
        private int _n_idtippue;
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
        public string c_puesto
        {
            get { return _c_puesto; }
            set { _c_puesto = value; }
        }
        public int n_idtippue
        {
            get { return _n_idtippue; }
            set { _n_idtippue = value; }
        }
    }
}
