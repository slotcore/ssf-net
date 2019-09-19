using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_DESTAJOUNI
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_anotra;
        private int _n_mestra;
        private DateTime _d_fchini;
        private DateTime _d_fchfin;
        private DateTime _d_fchreg;
        private int _n_numtra;
        private double _n_imp;
        private string _c_glo;
        private int _n_idres;
        private int _n_idlocal;
        private int _n_idpladesemp;

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
        public int n_anotra
        {
            get { return _n_anotra; }
            set { _n_anotra = value; }
        }
        public int n_mestra
        {
            get { return _n_mestra; }
            set { _n_mestra = value; }
        }
        public DateTime d_fchini
        {
            get { return _d_fchini; }
            set { _d_fchini = value; }
        }
        public DateTime d_fchfin
        {
            get { return _d_fchfin; }
            set { _d_fchfin = value; }
        }
        public DateTime d_fchreg
        {
            get { return _d_fchreg; }
            set { _d_fchreg = value; }
        }
        public int n_numtra
        {
            get { return _n_numtra; }
            set { _n_numtra = value; }
        }
        public double n_imp
        {
            get { return _n_imp; }
            set { _n_imp = value; }
        }
        public string c_glo
        {
            get { return _c_glo; }
            set { _c_glo = value; }
        }
        public int n_idres
        {
            get { return _n_idres; }
            set { _n_idres = value; }
        }
        public int n_idlocal
        {
            get { return _n_idlocal; }
            set { _n_idlocal = value; }
        }
        public int n_idpladesemp
        {
            get { return _n_idpladesemp; }
            set { _n_idpladesemp = value; }
        }
        
    }
}
