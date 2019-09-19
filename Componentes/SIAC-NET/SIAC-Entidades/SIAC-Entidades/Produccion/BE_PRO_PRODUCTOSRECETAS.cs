using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCTOSRECETAS
    {
        private int _n_idpro;
        private int _n_id;
        private string _c_codrec;
        private string _c_des;
        private int _n_idunimed;
        private double _n_can;
        private int _n_prirec;
        private string _c_obs;
        private int _n_act;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_codrec
        {
            get { return _c_codrec; }
            set { _c_codrec = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
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
        public int n_prirec
        {
            get { return _n_prirec; }
            set { _n_prirec = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_act
        {
            get { return _n_act; }
            set { _n_act = value; }
        }
    }
}
