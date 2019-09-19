using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_PROVICIONESDET
    {
        private int _n_idpro;
        private int _n_idcuecon;
        private int _n_tipo;
        private double _n_impsol;
        private double _n_impdol;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idcuecon
        {
            get { return _n_idcuecon; }
            set { _n_idcuecon = value; }
        }
        public int n_tipo
        {
            get { return _n_tipo; }
            set { _n_tipo = value; }
        }
        public double n_impsol
        {
            get { return _n_impsol; }
            set { _n_impsol = value; }
        }
        public double n_impdol
        {
            get { return _n_impdol; }
            set { _n_impdol = value; }
        }
    }
}
