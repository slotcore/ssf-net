using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_BOLETARESUMEN
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_ano;
        private int _n_mes;
        private DateTime _d_fchdoc;
        private DateTime _d_fchven;
        private int _n_idtipdoc;
        private int _n_numdoc;
        private double _n_importe;
        private int _n_numarc;

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
        public int n_ano
        {
            get { return _n_ano; }
            set { _n_ano = value; }
        }
        public int n_mes
        {
            get { return _n_mes; }
            set { _n_mes = value; }
        }
        public DateTime d_fchdoc
        {
            get { return _d_fchdoc; }
            set { _d_fchdoc = value; }
        }
        public DateTime d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public int n_numdoc
        {
            get { return _n_numdoc; }
            set { _n_numdoc = value; }
        }
        public double n_importe
        {
            get { return _n_importe; }
            set { _n_importe = value; }
        }
        public int n_numarc
        {
            get { return _n_numarc; }
            set { _n_numarc = value; }
        }
    }
}
