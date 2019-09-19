using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_LOCALSETUP
    {
        private int _n_idloc;
        private int _n_idtipcob;
        private string _c_numserfac;
        private string _c_numserbol;
        private string _c_numsertik;
        private int _n_iddocdef;
        private int _n_idserdef;

        public int n_idloc
        {
            get { return _n_idloc; }
            set { _n_idloc = value; }
        }
        public int n_idtipcob
        {
            get { return _n_idtipcob; }
            set { _n_idtipcob = value; }
        }
        public string c_numserfac
        {
            get { return _c_numserfac; }
            set { _c_numserfac = value; }
        }
        public string c_numserbol
        {
            get { return _c_numserbol; }
            set { _c_numserbol = value; }
        }
        public string c_numsertik
        {
            get { return _c_numsertik; }
            set { _c_numsertik = value; }
        }
        public int n_iddocdef
        {
            get { return _n_iddocdef; }
            set { _n_iddocdef = value; }
        }
        public int n_idserdef
        {
            get { return _n_idserdef; }
            set { _n_idserdef = value; }
        }
    }
}
