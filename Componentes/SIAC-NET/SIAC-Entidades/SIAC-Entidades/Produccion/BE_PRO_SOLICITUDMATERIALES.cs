using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_SOLICITUDMATERIALES
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_anotra;
        private int _n_mestra;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchreg;
        private int _n_idsol;
        private int _n_idprogra;
        private int _n_idordpro;
        private int _n_idite;
        private int _n_idrec;
        private string _c_obs;
        private int _n_idalm;
        private double _n_can;
        private DateTime _d_fchent;
        private int _n_idpro;
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
        public int n_idsol
        {
            get { return _n_idsol; }
            set { _n_idsol = value; }
        }
        public int n_idprogra
        {
            get { return _n_idprogra; }
            set { _n_idprogra = value; }
        }
        public int n_idordpro
        {
            get { return _n_idordpro; }
            set { _n_idordpro = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_idalm
        {
            get { return _n_idalm; }
            set { _n_idalm = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public DateTime d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
    }
}
