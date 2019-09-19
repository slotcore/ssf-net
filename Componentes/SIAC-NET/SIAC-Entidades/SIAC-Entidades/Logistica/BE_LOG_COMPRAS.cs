using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Logistica
{
    public class BE_LOG_COMPRAS
    {
        int _n_id;
        int _n_idemp;
        int _n_anotra;
        int _n_idmes;
        int _n_idlib;
        string _c_numreg;
        int _n_idtippro;
        int _n_idpro;
        int _n_idtipdoc;
        string _c_numser;
        string _c_numdoc;
        DateTime _d_fchdoc;
        DateTime _d_fchreg;
        int _n_idconpag;
        DateTime _d_fchven;
        int _n_idmon;
        double _n_impbru;
        double _n_impbru2;
        double _n_impbru3;
        double _n_impinaf;
        double _n_impigv;
        double _n_impigv2;
        double _n_impigv3;
        double _n_impisc;
        double _n_impotr;
        double _n_imptotcom;
        double _n_tc;
        double _n_impsal;
        double _n_tasaigv;
        string _c_glosa;
        int _n_estado;
        int _n_idtipdocref;
        int _n_iddocref;
        int _n_idtipope;
        int _n_idtipcom;
        double _n_tasa4ta;
        double _n_imp4ta;
        int _n_idtipdocmod;
        int _n_iddocmod;
        int _n_idmotnc;
        int _n_idmotnd;
        int _n_tipcom;
        int _n_idordpro;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_anotra
        {
            get { return _n_anotra; }
            set { _n_anotra = value; }
        }
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public int n_idlib
        {
            get { return _n_idlib; }
            set { _n_idlib = value; }
        }
        public string c_numreg
        {
            get { return _c_numreg; }
            set { _c_numreg = value; }
        }
        public int n_idtippro
        {
            get { return _n_idtippro; }
            set { _n_idtippro = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
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
        public DateTime d_fchdoc
        {
            get { return _d_fchdoc; }
            set { _d_fchdoc = value; }
        }
        public DateTime d_fchreg
        {
            get { return _d_fchreg; }
            set { _d_fchreg = value; }
        }
        public int n_idconpag
        {
            get { return _n_idconpag; }
            set { _n_idconpag = value; }
        }
        public DateTime d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public double n_impbru
        {
            get { return _n_impbru; }
            set { _n_impbru = value; }
        }
        public double n_impbru2
        {
            get { return _n_impbru2; }
            set { _n_impbru2 = value; }
        }
        public double n_impbru3
        {
            get { return _n_impbru3; }
            set { _n_impbru3 = value; }
        }
        public double n_impinaf
        {
            get { return _n_impinaf; }
            set { _n_impinaf = value; }
        }
        public double n_impigv
        {
            get { return _n_impigv; }
            set { _n_impigv = value; }
        }
        public double n_impigv2
        {
            get { return _n_impigv2; }
            set { _n_impigv2 = value; }
        }
        public double n_impigv3
        {
            get { return _n_impigv3; }
            set { _n_impigv3 = value; }
        }
        public double n_impisc
        {
            get { return _n_impisc; }
            set { _n_impisc = value; }
        }
        public double n_impotr
        {
            get { return _n_impotr; }
            set { _n_impotr = value; }
        }
        public double n_imptotcom
        {
            get { return _n_imptotcom; }
            set { _n_imptotcom = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
        public double n_impsal
        {
            get { return _n_impsal; }
            set { _n_impsal = value; }
        }
        public double n_tasaigv
        {
            get { return _n_tasaigv; }
            set { _n_tasaigv = value; }
        }
        public string c_glosa
        {
            get { return _c_glosa; }
            set { _c_glosa = value; }
        }
        public int n_estado
        {
            get { return _n_estado; }
            set { _n_estado = value; }
        }
        public int n_idtipdocref
        {
            get { return _n_idtipdocref; }
            set { _n_idtipdocref = value; }
        }
        public int n_iddocref
        {
            get { return _n_iddocref; }
            set { _n_iddocref = value; }
        }
        public int n_idtipope
        {
            get { return _n_idtipope; }
            set { _n_idtipope = value; }
        }
        public int n_idtipcom
        {
            get { return _n_idtipcom; }
            set { _n_idtipcom = value; }
        }
        public double n_tasa4ta
        {
            get { return _n_tasa4ta; }
            set { _n_tasa4ta = value; }
        }
        public double n_imp4ta
        {
            get { return _n_imp4ta; }
            set { _n_imp4ta = value; }
        }
        public int n_idtipdocmod
        {
            get { return _n_idtipdocmod; }
            set { _n_idtipdocmod = value; }
        }
        public int n_iddocmod
        {
            get { return _n_iddocmod; }
            set { _n_iddocmod = value; }
        }
        public int n_idmotnc
        {
            get { return _n_idmotnc; }
            set { _n_idmotnc = value; }
        }
        public int n_idmotnd
        {
            get { return _n_idmotnd; }
            set { _n_idmotnd = value; }
        }
        public int n_tipcom
        {
            get { return _n_tipcom; }
            set { _n_tipcom = value; }
        }
        public int n_idordpro
        {
            get { return _n_idordpro; }
            set { _n_idordpro = value; }
        }
    }
}
