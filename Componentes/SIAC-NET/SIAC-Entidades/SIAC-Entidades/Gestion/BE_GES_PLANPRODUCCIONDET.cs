using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Gestion
{
    public class BE_GES_PLANPRODUCCIONDET
    {
        int _n_idplapro;
        int _n_idite;
        int _n_idmes;
        int _n_idtipite;
        double _n_can;
        public int n_idplapro
        {
            get { return _n_idplapro; }
            set { _n_idplapro = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public int n_idtipite
        {
            get { return _n_idtipite; }
            set { _n_idtipite = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
    }
}
