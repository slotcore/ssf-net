using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_ORDENPRODUCCIONDET
    {
        private int _n_idord;
        private int _n_idpro;
        private int _n_idrec;
        private int _n_idunimed;
        private double _n_can;
        private string _c_obs;
        private DateTime ?_d_fchent;
        private int _n_numarm;
        public int n_idord
        {
            get { return _n_idord; }
            set { _n_idord = value; }
        }
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
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public DateTime ?d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public int n_numarm
        {
            get { return _n_numarm; }
            set { _n_numarm = value; }
        }
    }
}
