using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_MOVIMIENTOS
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_idtipmov;
        private int _n_idclipro;
        private DateTime _d_fchdoc;
        private DateTime _d_fching;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idalm;
        private int _n_anotra;
        private int _n_idmes;
        private string _c_obs;
        public int _n_idtipope;
        private int _n_tipite;
        private int ?_n_docrefidtipdoc;
        private string _c_docrefnumser;
        private string _c_docrefnumdoc;
        private int _n_perid;
        private int ?_n_docrefiddocref;
        private int _n_prolog;
        private int ?_n_iddoclog;
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
        public int n_idtipmov
        {
            get { return _n_idtipmov; }
            set { _n_idtipmov = value; }
        }
        public int n_idclipro
        {
            get { return _n_idclipro; }
            set { _n_idclipro = value; }
        }
        public DateTime d_fchdoc
        {
            get { return _d_fchdoc; }
            set { _d_fchdoc = value; }
        }
        public DateTime d_fching
        {
            get { return _d_fching; }
            set { _d_fching = value; }
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
        public int n_idalm
        {
            get { return _n_idalm; }
            set { _n_idalm = value; }
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
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_idtipope
        {
            get { return _n_idtipope; }
            set { _n_idtipope = value; }
        }
        public int n_tipite
        {
            get { return _n_tipite; }
            set { _n_tipite = value; }
        }
        public int ?n_docrefidtipdoc
        {
            get { return _n_docrefidtipdoc; }
            set { _n_docrefidtipdoc = value; }
        }
        public string c_docrefnumser
        {
            get { return _c_docrefnumser; }
            set { _c_docrefnumser = value; }
        }
        public string c_docrefnumdoc
        {
            get { return _c_docrefnumdoc; }
            set { _c_docrefnumdoc = value; }
        }
        public int n_perid
        {
            get { return _n_perid; }
            set { _n_perid = value; }
        }
        public int ?n_docrefiddocref
        {
            get { return _n_docrefiddocref; }
            set { _n_docrefiddocref = value; }
        }
        public int n_prolog
        {
            get { return _n_prolog; }
            set { _n_prolog = value; }
        }
        public int ?n_iddoclog
        {
            get { return _n_iddoclog; }
            set { _n_iddoclog = value; }
        }
    }
}
