using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_TESORERIAORIDET
    {
        private int _n_idtes;
        private int _n_idori;
        private int _n_idtipper;
        private int _n_idmod;
        private int _n_iddoc;
        private int _n_idper;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private double _n_imp;
        private double _n_sal;
        private double _n_acuenta;
        private DateTime _d_fchdoc;
        private string _c_glo;
        private int _n_cor;
        private int _n_idmon;
        private int _n_idmedpag;
        public int n_idtes
        {
            get { return _n_idtes; }
            set { _n_idtes = value; }
        }
        public int n_idori
        {
            get { return _n_idori; }
            set { _n_idori = value; }
        }
        public int n_idtipper
        {
            get { return _n_idtipper; }
            set { _n_idtipper = value; }
        }
        public int n_idmod
        {
            get { return _n_idmod; }
            set { _n_idmod = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public string c_numser
        {
            get { return _c_numser; }
            set { _c_numser = value; }
        }
        public string c_numdoc
        {
            get { return _c_numdoc; }
            set { _c_numdoc = value; }
        }
        public double n_imp
        {
            get { return _n_imp; }
            set { _n_imp = value; }
        }
        public double n_sal
        {
            get { return _n_sal; }
            set { _n_sal = value; }
        }
        public double n_acuenta
        {
            get { return _n_acuenta; }
            set { _n_acuenta = value; }
        }
        public DateTime d_fchdoc
        {
            get { return _d_fchdoc; }
            set { _d_fchdoc = value; }
        }
        public string c_glo
        {
            get { return _c_glo; }
            set { _c_glo = value; }
        }
        public int n_cor
        {
            get { return _n_cor; }
            set { _n_cor = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public int n_idmedpag
        {
            get { return _n_idmedpag; }
            set { _n_idmedpag = value; }
        }
    }
}
