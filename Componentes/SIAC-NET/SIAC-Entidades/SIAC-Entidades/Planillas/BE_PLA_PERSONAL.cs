using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_PERSONAL
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idtra;
        private int _n_idcargo;
        private int _n_activo;
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
        public int n_idtra
        {
            get { return _n_idtra; }
            set { _n_idtra = value; }
        }
        public int n_idcargo
        {
            get { return _n_idcargo; }
            set { _n_idcargo = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
    }
}
