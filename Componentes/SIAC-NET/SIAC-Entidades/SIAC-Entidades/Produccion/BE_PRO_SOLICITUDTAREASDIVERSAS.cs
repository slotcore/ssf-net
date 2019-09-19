using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_SOLICITUDTAREASDIVERSAS
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idmes;
        private int _n_anotra;
        private int _n_idpro;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchreg;
        private int _n_idsol;
        private DateTime? _d_fchejetar;
        private string _c_obs;
        private int _n_idlocal;
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
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public int n_anotra
        {
            get { return _n_anotra; }
            set { _n_anotra = value; }
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
        public DateTime? d_fchejetar
        {
            get { return _d_fchejetar; }
            set { _d_fchejetar = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_idlocal
        {
            get { return _n_idlocal; }
            set { _n_idlocal = value; }
        }
    }
}
