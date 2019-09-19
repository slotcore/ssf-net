using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_IMPUESTOS
    {
        private int _n_id;
        private string _c_des;
        private double _n_tasa;
        private int _n_idcue;
        private int _n_idcuevta;
        private string _c_abr;
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
        public int n_idcue
        {
            get { return _n_idcue; }
            set { _n_idcue = value; }
        }
        public int n_idcuevta
        {
            get { return _n_idcuevta; }
            set { _n_idcuevta = value; }
        }
        public string c_abr
        {
            get { return _c_abr; }
            set { _c_abr = value; }
        }
    }
}
