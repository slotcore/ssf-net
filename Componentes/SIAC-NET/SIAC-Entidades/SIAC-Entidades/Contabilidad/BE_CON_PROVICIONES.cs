using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_PROVICIONES
    {
        private int _n_id;
        private int _n_idlib;
        private int _n_idsublib;
        private int _n_ano;
        private int _n_mes;
        private DateTime _d_fchreg;
        private DateTime _d_fchdoc;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idcli;
        private string _c_nomcli;
        private int _n_idmon;
        private double _n_imp;
        private string _c_glosa;
        private string _c_numreg;
        private double _n_tc;
        private int _n_ajuste;
        private int _n_idemp;

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
        public int n_idsublib
        {
            get { return _n_idsublib; }
            set { _n_idsublib = value; }
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
        public DateTime d_fchdoc
        {
            get { return _d_fchdoc; }
            set { _d_fchdoc = value; }
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
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public string c_nomcli
        {
            get { return _c_nomcli; }
            set { _c_nomcli = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public double n_imp
        {
            get { return _n_imp; }
            set { _n_imp = value; }
        }
        public string c_glosa
        {
            get { return _c_glosa; }
            set { _c_glosa = value; }
        }
        public string c_numreg
        {
            get { return _c_numreg; }
            set { _c_numreg = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_ajuste
        {
            get { return _n_ajuste; }
            set { _n_ajuste = value; }
        }
    }
}
