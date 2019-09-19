using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_CARGOSCAB
    {
        private int _n_idemp;
        private int _n_idcar;
        private int _n_idsoc;
        private int _n_idsocpue;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchemi;
        private DateTime _d_fchven;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
        private string _c_glosa;
        private double _n_impsal;
        private int _n_anotra;
        private int _n_mestra;
        private int _n_id;
        private int _n_iddocpag;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idcar
        {
            get { return _n_idcar; }
            set { _n_idcar = value; }
        }
        public int n_idsoc
        {
            get { return _n_idsoc; }
            set { _n_idsoc = value; }
        }
        public int n_idsocpue
        {
            get { return _n_idsocpue; }
            set { _n_idsocpue = value; }
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
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public DateTime d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public double n_impbru
        {
            get { return _n_impbru; }
            set { _n_impbru = value; }
        }
        public double n_impigv
        {
            get { return _n_impigv; }
            set { _n_impigv = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public string c_glosa
        {
            get { return _c_glosa; }
            set { _c_glosa = value; }
        }
        public double n_impsal
        {
            get { return _n_impsal; }
            set { _n_impsal = value; }
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
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_iddocpag
        {
            get { return _n_iddocpag; }
            set { _n_iddocpag = value; }
        }
    }
}
