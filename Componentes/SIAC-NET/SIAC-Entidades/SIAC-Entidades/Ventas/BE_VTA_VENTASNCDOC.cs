using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENTASNCDOC
    {
        private Int64 _n_idvta;
        private int _n_iddoc;
        private double _n_saldo;
        private double _n_acuenta;
        private double _n_nuevosaldo;
        public Int64 n_idvta
        {
            get { return _n_idvta; }
            set { _n_idvta = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
        public double n_saldo
        {
            get { return _n_saldo; }
            set { _n_saldo = value; }
        }
        public double n_acuenta
        {
            get { return _n_acuenta; }
            set { _n_acuenta = value; }
        }
        public double n_nuevosaldo
        {
            get { return _n_nuevosaldo; }
            set { _n_nuevosaldo = value; }
        }
    }
}
