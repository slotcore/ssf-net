using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_REVISION
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idtipdocref;
        private int _n_iddocref;
        private string _c_numdocref;
        private int _n_idpro;
        private int _n_idite;
        private int _n_idrec;
        private string _c_numlot;
        private double _n_canpro;
        private int _n_idunimed;
        private DateTime _d_fchini;
        private DateTime _d_fchfin;
        private string _h_horini;
        private string _h_horfin;
        private double _n_canprocon;
        private double _n_canpronocon;
        private string _c_obsprocon;
        private string _c_obspronocon;
        private int _n_idperrev;
        private DateTime _d_fchrev;
        private string _h_horrev;
        private int _n_procesado;

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
        public string c_numdocref
        {
            get { return _c_numdocref; }
            set { _c_numdocref = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
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
        public string c_numlot
        {
            get { return _c_numlot; }
            set { _c_numlot = value; }
        }
        public double n_canpro
        {
            get { return _n_canpro; }
            set { _n_canpro = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
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
        public string h_horini
        {
            get { return _h_horini; }
            set { _h_horini = value; }
        }
        public string h_horfin
        {
            get { return _h_horfin; }
            set { _h_horfin = value; }
        }
        public double n_canprocon
        {
            get { return _n_canprocon; }
            set { _n_canprocon = value; }
        }
        public double n_canpronocon
        {
            get { return _n_canpronocon; }
            set { _n_canpronocon = value; }
        }
        public string c_obsprocon
        {
            get { return _c_obsprocon; }
            set { _c_obsprocon = value; }
        }
        public string c_obspronocon
        {
            get { return _c_obspronocon; }
            set { _c_obspronocon = value; }
        }
        public int n_idperrev
        {
            get { return _n_idperrev; }
            set { _n_idperrev = value; }
        }
        public DateTime d_fchrev
        {
            get { return _d_fchrev; }
            set { _d_fchrev = value; }
        }
        public string h_horrev
        {
            get { return _h_horrev; }
            set { _h_horrev = value; }
        }
        public int n_procesado
        {
            get { return _n_procesado; }
            set { _n_procesado = value; }
        }
    }
}
