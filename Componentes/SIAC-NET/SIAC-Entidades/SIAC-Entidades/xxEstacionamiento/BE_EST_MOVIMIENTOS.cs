using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_MOVIMIENTOS
    {
        private int _n_idemp;
        private int _n_idloc;
        private int _n_id;
        private int _n_idcaj;
        private int _n_idcli;
        private DateTime _d_fchdoc;
        private string _c_horini;
        private string _c_horfin;
        private string _c_tieuit;
        private double _n_tieuti;
        private int _n_idser;
        private double _n_importe;
        private double _n_impser;
        private int _n_idtipdoc;
        private string _c_numdoc;
        private int _n_iddocven;
        private string _c_numpla;
        private int _n_idtipcli;
        private int _n_finalizado;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idloc
        {
            get { return _n_idloc; }
            set { _n_idloc = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idcaj
        {
            get { return _n_idcaj; }
            set { _n_idcaj = value; }
        }
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public DateTime d_fchdoc
        {
            get { return _d_fchdoc; }
            set { _d_fchdoc = value; }
        }
        public string c_horini
        {
            get { return _c_horini; }
            set { _c_horini = value; }
        }
        public string c_horfin
        {
            get { return _c_horfin; }
            set { _c_horfin = value; }
        }
        public string c_tieuit
        {
            get { return _c_tieuit; }
            set { _c_tieuit = value; }
        }
        public double n_tieuti
        {
            get { return _n_tieuti; }
            set { _n_tieuti = value; }
        }
        public int n_idser
        {
            get { return _n_idser; }
            set { _n_idser = value; }
        }
        public double n_importe
        {
            get { return _n_importe; }
            set { _n_importe = value; }
        }
        public double n_impser
        {
            get { return _n_impser; }
            set { _n_impser = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public string c_numdoc
        {
            get { return _c_numdoc; }
            set { _c_numdoc = value; }
        }
        public int n_iddocven
        {
            get { return _n_iddocven; }
            set { _n_iddocven = value; }
        }
        public string c_numpla
        {
            get { return _c_numpla; }
            set { _c_numpla = value; }
        }
        public int n_idtipcli
        {
            get { return _n_idtipcli; }
            set { _n_idtipcli = value; }
        }
        public int n_finalizado
        {
            get { return _n_finalizado; }
            set { _n_finalizado = value; }
        }
    }
}
