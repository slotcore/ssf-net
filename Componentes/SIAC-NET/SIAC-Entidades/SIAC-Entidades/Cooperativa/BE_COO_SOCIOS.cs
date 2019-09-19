using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_SOCIOS
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_ideidtipdoc;
        private string _c_idenumdoc;
        private string _c_ape1;
        private string _c_ape2;
        private string _c_nom1;
        private string _c_nom2;
        private string _c_apenom;
        private string _c_dir;
        private int _n_iddis;
        private DateTime ?_d_fchnac;
        private string _c_numtel;
        private string _c_numcel;
        private string _c_email;
        private int _n_idsex;
        private int _n_idtipsoc;
        private DateTime? _d_fching;
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
        public int n_ideidtipdoc
        {
            get { return _n_ideidtipdoc; }
            set { _n_ideidtipdoc = value; }
        }
        public string c_idenumdoc
        {
            get { return _c_idenumdoc; }
            set { _c_idenumdoc = value; }
        }
        public string c_ape1
        {
            get { return _c_ape1; }
            set { _c_ape1 = value; }
        }
        public string c_ape2
        {
            get { return _c_ape2; }
            set { _c_ape2 = value; }
        }
        public string c_nom1
        {
            get { return _c_nom1; }
            set { _c_nom1 = value; }
        }
        public string c_nom2
        {
            get { return _c_nom2; }
            set { _c_nom2 = value; }
        }
        public string c_apenom
        {
            get { return _c_apenom; }
            set { _c_apenom = value; }
        }
        public string c_dir
        {
            get { return _c_dir; }
            set { _c_dir = value; }
        }
        public int n_iddis
        {
            get { return _n_iddis; }
            set { _n_iddis = value; }
        }
        public DateTime ?d_fchnac
        {
            get { return _d_fchnac; }
            set { _d_fchnac = value; }
        }
        public string c_numtel
        {
            get { return _c_numtel; }
            set { _c_numtel = value; }
        }
        public string c_numcel
        {
            get { return _c_numcel; }
            set { _c_numcel = value; }
        }
        public string c_email
        {
            get { return _c_email; }
            set { _c_email = value; }
        }
        public int n_idsex
        {
            get { return _n_idsex; }
            set { _n_idsex = value; }
        }
        public int n_idtipsoc
        {
            get { return _n_idtipsoc; }
            set { _n_idtipsoc = value; }
        }
        public DateTime? d_fching
        {
            get { return _d_fching; }
            set { _d_fching = value; }
        }
    }
}
