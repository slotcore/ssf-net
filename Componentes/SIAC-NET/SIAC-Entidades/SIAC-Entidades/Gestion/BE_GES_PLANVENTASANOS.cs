using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Gestion
{
    public class BE_GES_PLANVENTASANOS
    {
        private int _n_idplan;
        private int _n_idano;
        public int n_idplan
        {
            get { return _n_idplan; }
            set { _n_idplan = value; }
        }
        public int n_idano
        {
            get { return _n_idano; }
            set { _n_idano = value; }
        }
    }
}
