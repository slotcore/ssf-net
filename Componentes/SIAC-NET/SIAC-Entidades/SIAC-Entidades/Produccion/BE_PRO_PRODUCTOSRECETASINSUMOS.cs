using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCTOSRECETASINSUMOS
    {
        private int _n_idpro;
        private int _n_idrec;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_can;
        private int _n_inspri;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public int n_inspri
        {
            get { return _n_inspri; }
            set { _n_inspri = value; }
        }
    }
}
