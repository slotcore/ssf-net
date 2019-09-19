using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Sistema
{
    public class BE_SYS_USUARIOSINGRESOS
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_idusu;
        private DateTime _d_fchreg;
        private string _c_idpc;
        private string _c_horreg;
        private int _n_tipmov;
        private int _n_ord;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idusu
        {
            get { return _n_idusu; }
            set { _n_idusu = value; }
        }
        public DateTime d_fchreg
        {
            get { return _d_fchreg; }
            set { _d_fchreg = value; }
        }
        public string c_idpc
        {
            get { return _c_idpc; }
            set { _c_idpc = value; }
        }
        public string c_horreg
        {
            get { return _c_horreg; }
            set { _c_horreg = value; }
        }
        public int n_tipmov
        {
            get { return _n_tipmov; }
            set { _n_tipmov = value; }
        }
        public int n_ord
        {
            get { return _n_ord; }
            set { _n_ord = value; }
        }
    }
}
