using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_CAJEROS
    {
        private int _n_idemp;
        private int _n_idloc;
        private int _n_id;
        private int _n_idtra;
        private int _n_idusu;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idloc
        {
            get { return _n_idloc; }
            set { _n_idloc = value; }
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
        public int n_idusu
        {
            get { return _n_idusu; }
            set { _n_idusu = value; }
        }
    }
}
