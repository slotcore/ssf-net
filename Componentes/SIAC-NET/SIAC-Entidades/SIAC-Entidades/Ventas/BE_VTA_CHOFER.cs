using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_CHOFER
    {
        private int _n_idemp;
        private int _n_idper;
        private int _n_id;
        private string _c_cat;
        private string _c_numbre;
        private int _n_idveh;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_cat
        {
            get { return _c_cat; }
            set { _c_cat = value; }
        }
        public string c_numbre
        {
            get { return _c_numbre; }
            set { _c_numbre = value; }
        }
        public int n_idveh
        {
            get { return _n_idveh; }
            set { _n_idveh = value; }
        }

    }
}
