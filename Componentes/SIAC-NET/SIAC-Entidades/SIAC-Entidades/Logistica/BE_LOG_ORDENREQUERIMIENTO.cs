using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SIAC_Entidades.Logistica
{
    public class BE_LOG_ORDENREQUERIMIENTO
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_anotra;
        private int _n_mestra;
        private DateTime ?_d_fchemi;
        private DateTime ?_d_fchent;
        private int _n_idloc;
        private int _n_idare;
        private int _n_idpersol;
        private int _n_idpri;
        private string _c_obs;
        private int _n_idest;
        private int _n_idmot;
        private int _n_idareadest;

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
        public DateTime ?d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public DateTime ?d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public int n_idloc
        {
            get { return _n_idloc; }
            set { _n_idloc = value; }
        }
        public int n_idare
        {
            get { return _n_idare; }
            set { _n_idare = value; }
        }
        public int n_idpersol
        {
            get { return _n_idpersol; }
            set { _n_idpersol = value; }
        }
        public int n_idpri
        {
            get { return _n_idpri; }
            set { _n_idpri = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_idest
        {
            get { return _n_idest; }
            set { _n_idest = value; }
        }
        public int n_idmot
        {
            get { return _n_idmot; }
            set { _n_idmot = value; }
        }

        public int n_idareadest
        {
            get { return _n_idareadest; }
            set { _n_idareadest = value; }
        }
    }
}
