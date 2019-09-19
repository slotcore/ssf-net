using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_REGRETENCION
    {
        private int _n_idemp;
        private int _n_ano;
        private int _n_mes;
        private string _c_numreg;
        private int _n_id;
        private int _n_idlib;
        private int _n_idtipdoc;
        private int _n_idret;
        private double _n_tas;
        private int _n_tip;
        private int _n_idcli;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchemi;
        private DateTime _d_fchreg;
        private int _n_idmon;
        private double _n_impret;
        private string _c_glo;
        private double _n_tc;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
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
        public string c_numreg
        {
            get { return _c_numreg; }
            set { _c_numreg = value; }
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
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public int n_idret
        {
            get { return _n_idret; }
            set { _n_idret = value; }
        }
        public double n_tas
        {
            get { return _n_tas; }
            set { _n_tas = value; }
        }
        public int n_tip
        {
            get { return _n_tip; }
            set { _n_tip = value; }
        }
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
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
        public DateTime d_fchreg
        {
            get { return _d_fchreg; }
            set { _d_fchreg = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public double n_impret
        {
            get { return _n_impret; }
            set { _n_impret = value; }
        }
        public string c_glo
        {
            get { return _c_glo; }
            set { _c_glo = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
    }
}
