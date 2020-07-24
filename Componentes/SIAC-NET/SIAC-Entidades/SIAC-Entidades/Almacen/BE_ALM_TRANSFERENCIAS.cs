using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_TRANSFERENCIAS
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_idclipro;
        private DateTime _d_fchdoc;
        private DateTime _d_fching;
        private int _n_idalmdest;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idalmorig;
        private int _n_anotra;
        private int _n_idmes;
        private string _c_obs;
        private int _n_idresp;
        public int _n_idtipope;
        private int _n_tipite;
        private string _c_alm_origdes;
        private string _c_alm_destdes;
        private string _c_numdocvis;
        private string _c_respdes;
        private List<BE_ALM_TRANSFERENCIASDET> _lst_items;

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
        public int n_idresp
        {
            get { return _n_idresp; }
            set { _n_idresp = value; }
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
        public int n_idalmdest
        {
            get { return _n_idalmdest; }
            set { _n_idalmdest = value; }
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
        public int n_idalmorig
        {
            get { return _n_idalmorig; }
            set { _n_idalmorig = value; }
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
        public string c_alm_origdes
        {
            get { return _c_alm_origdes; }
            set { _c_alm_origdes = value; }
        }
        public string c_alm_destdes
        {
            get { return _c_alm_destdes; }
            set { _c_alm_destdes = value; }
        }
        public string c_numdocvis
        {
            get { return _c_numdocvis; }
            set { _c_numdocvis = value; }
        }
        public string c_respdes
        {
            get { return _c_respdes; }
            set { _c_respdes = value; }
        }

        public List<BE_ALM_TRANSFERENCIASDET> lst_items
        {
            get { return _lst_items; }
            set { _lst_items = value; }
        }

        public BE_ALM_TRANSFERENCIAS()
        {
            _lst_items = new List<BE_ALM_TRANSFERENCIASDET>();
        }
    }
}
