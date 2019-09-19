using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_CERRARMES
    {
        private int _n_idmod;
        private int _n_idmes;
        private int _n_estado;
        private int _n_idemp;
        public int n_idmod
        {
            get { return _n_idmod; }
            set { _n_idmod = value; }
        }
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public int n_estado
        {
            get { return _n_estado; }
            set { _n_estado = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
    }

}
