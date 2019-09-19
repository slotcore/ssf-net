using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_ORDENPRODUCCION
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private DateTime _d_fchemi;
        private int _n_anotra;
        private int _n_mestra;
        private int _n_idres;
        private int _n_idtipdocref;
        private int _n_iddocref;
        private int _n_idpri;
        private string _c_obs;
        private DateTime _d_fchent;
        private int _n_idest;

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
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
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
        public int n_idres
        {
            get { return _n_idres; }
            set { _n_idres = value; }
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
        public DateTime d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public int n_idest
        {
            get { return _n_idest; }
            set { _n_idest = value; }
        }
    }
}
