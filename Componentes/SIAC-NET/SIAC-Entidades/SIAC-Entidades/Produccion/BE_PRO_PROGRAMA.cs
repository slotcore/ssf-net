using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PROGRAMA
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idpro;
        private int _n_anotra;
        private int _n_mestra;
        private DateTime _d_fchini;
        private DateTime _d_fchfin;
        private DateTime _d_fchemi;
        private string _c_obs;
        private int _n_numhordia;
        private int _n_numdiapro;
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
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
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
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_numhordia
        {
            get { return _n_numhordia; }
            set { _n_numhordia = value; }
        }
        public int n_numdiapro
        {
            get { return _n_numdiapro; }
            set { _n_numdiapro = value; }
        }
    }
}
