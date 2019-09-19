using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_RETENCION
    {
        private int _n_id;
        private string _c_des;
        private double _n_tasa;
        private int _n_defaul;
        private int _n_idcuecom;
        private int _n_idcueven;
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
        public int n_defaul
        {
            get { return _n_defaul; }
            set { _n_defaul = value; }
        }
        public int n_idcuecom
        {
            get { return _n_idcuecom; }
            set { _n_idcuecom = value; }
        }
        public int n_idcueven
        {
            get { return _n_idcueven; }
            set { _n_idcueven = value; }
        }
    }
}
