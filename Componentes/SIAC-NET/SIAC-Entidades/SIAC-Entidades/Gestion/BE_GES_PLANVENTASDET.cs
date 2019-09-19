using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Gestion
{
    public class BE_GES_PLANVENTASDET
    {
        int _n_idplan;
        int _n_idite;
        int _n_idmes;
        double _n_canite;
        int _n_orden;
        public int n_idplan
        {
            get { return _n_idplan; }
            set { _n_idplan = value; }
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
        public Double n_canite
        {
            get { return _n_canite; }
            set { _n_canite = value; }
        }
        public int n_orden
        {
            get { return _n_orden; }
            set { _n_orden = value; }
        }
    }
}
