using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENTAS
    {
        private Int64 _n_id;
        private int _n_idemp;
        private int _n_anotra;
        private int _n_idmes;
        private int _n_idlib;
        private string _c_numreg;
        private int _n_idtippro;
        private int _n_idcli;
        private int _n_idpunvencli;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchreg;
        private DateTime _d_fchdoc;
        private DateTime _d_fchven;
        private int _n_idconpag;
        private int _n_idmon;
        private double _n_impbru;
        private double _n_impbru2;
        private double _n_impbru3;
        private double _n_impinaf;
        private double _n_impigv;
        private double _n_impisc;
        private double _n_impotr;
        private double _n_imptotven;
        private double _n_tc;
        private double _n_impsal;
        private int _n_idven;
        private double _n_tasaigv;
        private string _c_glosa;
        private int _n_oriitem;
        private int _n_anulado;
        private int _n_idtipven;
        private int _n_idtipdocref;
        private int _n_iddocref;
        private string _c_serdocref = "";
        private string _c_numdocref = "";
        private int _n_idtipdes;
        private double _n_impdes;
        private string _c_nomcli;
        private string _c_dircli;
        private int _n_idpue;
        private double _n_impsubtot;
        private double _n_pordsc;
        private int _n_idtipdocmod;
        private int _n_iddocmod;
        private int _n_idtipmot;
        private int _n_idtipope;
        private string _c_numlet;
        private string _c_motnc;
        private int _n_idforpag;
        private int _n_idtarcre;
        private int _n_iddocven;

        public Int64 n_id
        {
            get { return _n_id;}
            set { _n_id = value;}
        }
        public int n_idemp
        {
            get { return _n_idemp;}
            set { _n_idemp = value;}
        }
        public int n_anotra
        {
            get { return _n_anotra;}
            set { _n_anotra = value;}
        }
        public int n_idmes
        {
            get { return _n_idmes;}
            set { _n_idmes = value;}
        }
        public int n_idlib
        {
            get { return _n_idlib;}
            set { _n_idlib = value;}
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
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public int n_idpunvencli
        {
            get { return _n_idpunvencli; }
            set { _n_idpunvencli = value; }
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
        public DateTime d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public int n_idconpag
        {
            get { return _n_idconpag; }
            set { _n_idconpag = value; }
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
        public double n_imptotven
        {
            get { return _n_imptotven; }
            set { _n_imptotven = value; }
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
        public int n_idven
        {
            get { return _n_idven; }
            set { _n_idven = value; }
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
        public int n_oriitem
        {
            get { return _n_oriitem; }
            set { _n_oriitem = value; }
        }
        public int n_anulado
        {
            get { return _n_anulado; }
            set { _n_anulado = value; }
        }
        public int n_idtipven
        {
            get { return _n_idtipven; }
            set { _n_idtipven = value; }
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
        public string c_serdocref
        {
            get { return _c_serdocref; }
            set { _c_serdocref = value; }
        }
        public string c_numdocref
        {
            get { return _c_numdocref; }
            set { _c_numdocref = value; }
        }
        public int n_idtipdes
        {
            get { return _n_idtipdes; }
            set { _n_idtipdes = value; }
        }
        public double n_impdes
        {
            get { return _n_impdes; }
            set { _n_impdes = value; }
        }
        public string c_nomcli
        {
            get { return _c_nomcli; }
            set { _c_nomcli = value; }
        }
        public string c_dircli
        {
            get { return _c_dircli; }
            set { _c_dircli = value; }
        }
        public int n_idpue
        {
            get { return _n_idpue; }
            set { _n_idpue = value; }
        }
        public double n_impsubtot
        {
            get { return _n_impsubtot; }
            set { _n_impsubtot = value; }
        }
        public double n_pordsc
        {
            get { return _n_pordsc; }
            set { _n_pordsc = value; }
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
        public int n_idtipmot
        {
            get { return _n_idtipmot; }
            set { _n_idtipmot = value; }
        }
        public int n_idtipope
        {
            get { return _n_idtipope; }
            set { _n_idtipope = value; }
        }
        public string c_numlet
        {
            get { return _c_numlet; }
            set { _c_numlet = value; }
        }
        public string c_motnc
        {
            get { return _c_motnc; }
            set { _c_motnc = value; }
        }
        public int n_idforpag
        {
            get { return _n_idforpag; }
            set { _n_idforpag = value; }
        }
        public int n_idtarcre
        {
            get { return _n_idtarcre; }
            set { _n_idtarcre = value; }
        }
        public int n_iddocven
        {
            get { return _n_iddocven; }
            set { _n_iddocven = value; }
        }
        
    }
}
