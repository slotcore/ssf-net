using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_TC
    {
        private int _n_id;
        private DateTime _d_fecha;
        private int _n_idmon;
        private double _n_impcom;
        private double _n_impven;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public DateTime d_fecha
        {
            get { return _d_fecha; }
            set { _d_fecha = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public double n_impcom
        {
            get { return _n_impcom; }
            set { _n_impcom = value; }
        }
        public double n_impven
        {
            get { return _n_impven; }
            set { _n_impven = value; }
        }

    }
}
