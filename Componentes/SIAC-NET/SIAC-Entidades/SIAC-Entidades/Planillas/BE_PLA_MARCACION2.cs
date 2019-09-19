using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_MARCACION2
    {
        private int _n_id;
        private string _c_numdoc;
        private string _c_nomemp;
        private DateTime _d_fecha;
        private int _n_tipo;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_numdoc
        {
            get { return _c_numdoc; }
            set { _c_numdoc = value; }
        }
        public string c_nomemp
        {
            get { return _c_nomemp; }
            set { _c_nomemp = value; }
        }
        public DateTime d_fecha
        {
            get { return _d_fecha; }
            set { _d_fecha = value; }
        }
        public int n_tipo
        {
            get { return _n_tipo; }
            set { _n_tipo = value; }
        }
    }
}
