using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_TAREA
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_cod;
        private string _c_des;
        private int _n_idunimed;
        private int _n_div;
        private string _c_abr;
        private string _c_obs;
        private double _n_pre;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_cod
        {
            get { return _c_cod; }
            set { _c_cod = value; }
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
        public int n_div
        {
            get { return _n_div; }
            set { _n_div = value; }
        }
        public string c_abr
        {
            get { return _c_abr; }
            set { _c_abr = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public double n_pre
        {
            get { return _n_pre; }
            set { _n_pre = value; }
        }
    }
}
