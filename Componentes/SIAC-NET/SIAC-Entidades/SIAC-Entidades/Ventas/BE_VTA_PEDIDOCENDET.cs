using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_PEDIDOCENDET
    {
        private int _n_idped;
        private string _c_coditecen;
        private double _n_canpro;
        private double _n_precio;
        private string _c_codunimedcen;
        public int n_idped
        {
            get { return _n_idped; }
            set { _n_idped = value; }
        }
        public string c_coditecen
        {
            get { return _c_coditecen; }
            set { _c_coditecen = value; }
        }
        public double n_canpro
        {
            get { return _n_canpro; }
            set { _n_canpro = value; }
        }
        public double n_precio
        {
            get { return _n_precio; }
            set { _n_precio = value; }
        }
        public string c_codunimedcen
        {
            get { return _c_codunimedcen; }
            set { _c_codunimedcen = value; }
        }
    }
}
