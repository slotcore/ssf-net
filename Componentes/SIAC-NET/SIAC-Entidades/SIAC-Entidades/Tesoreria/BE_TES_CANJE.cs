using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_CANJE
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idlib;
        private int _n_ano;
        private int _n_mes;
        private DateTime _d_fchreg;
        private string _c_numreg;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchemi;
        private int _n_idpro;
        private int _n_idcli;
        private int _n_idmon;
        private double _n_impcan;
        private string _c_glosa;
        private double _n_tc;
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
        public int n_idlib
        {
            get { return _n_idlib; }
            set { _n_idlib = value; }
        }
        public int n_ano
        {
            get { return _n_ano; }
            set { _n_ano = value; }
        }
        public int n_mes
        {
            get { return _n_mes; }
            set { _n_mes = value; }
        }
        public DateTime d_fchreg
        {
            get { return _d_fchreg; }
            set { _d_fchreg = value; }
        }
        public string c_numreg
        {
            get { return _c_numreg; }
            set { _c_numreg = value; }
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
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public double n_impcan
        {
            get { return _n_impcan; }
            set { _n_impcan = value; }
        }
        public string c_glosa
        {
            get { return _c_glosa; }
            set { _c_glosa = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
    }
}
