using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_SERVICIOSDET
    {
        private int _n_idser;
        private int _n_idpue;
        private string _c_numlecini;
        private string _c_numlecfin;
        private double _n_impcon;
        private string _c_obs;
        public int n_idser
        {
            get { return _n_idser; }
            set { _n_idser = value; }
        }
        public int n_idpue
        {
            get { return _n_idpue; }
            set { _n_idpue = value; }
        }
        public string c_numlecini
        {
            get { return _c_numlecini; }
            set { _c_numlecini = value; }
        }
        public string c_numlecfin
        {
            get { return _c_numlecfin; }
            set { _c_numlecfin = value; }
        }
        public double n_impcon
        {
            get { return _n_impcon; }
            set { _n_impcon = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
    }
}
