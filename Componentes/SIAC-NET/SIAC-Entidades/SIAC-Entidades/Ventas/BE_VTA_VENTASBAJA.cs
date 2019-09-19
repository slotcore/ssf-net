using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENTASBAJA
    {
        private int _n_idemp;
		private int _n_idvta;
		private int _n_cor;
		private string _c_arcbaj;
        private DateTime _d_fchbaj;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idvta
        {
            get { return _n_idvta; }
            set { _n_idvta = value; }
        }
        public int n_cor
        {
            get { return _n_cor; }
            set { _n_cor = value; }
        }
        public string c_arcbaj
        {
            get { return _c_arcbaj; }
            set { _c_arcbaj = value; }
        }
        public DateTime d_fchbaj
        {
            get { return _d_fchbaj; }
            set { _d_fchbaj = value; }
        }
    }
}
