﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Sunat
{
    public class BE_SUN_TIPEXI
    {
        private int _n_id;
        private string _c_des;
        private string _c_codsun;
        private string _c_pre;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public string c_codsun
        {
            get { return _c_codsun; }
            set { _c_codsun = value; }
        }
        public string c_pre
        {
            get { return _c_pre; }
            set { _c_pre = value; }
        }
    }
}
