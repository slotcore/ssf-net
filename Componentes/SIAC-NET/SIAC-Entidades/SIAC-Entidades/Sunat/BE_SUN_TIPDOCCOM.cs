using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Sunat
{
    public class BE_SUN_TIPDOCCOM
    {
        private int _n_id;
        private string _c_codsun;
        private string _c_des;
        private string _c_abr;
        private int _n_idtipo;
        private int _n_essal;
        private int _n_esent;
        private int _n_seimp;
        private int _n_numfil;
        private int _n_activo;
        private int _c_preser;
        private int _n_escom;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_codsun
        {
            get { return _c_codsun; }
            set { _c_codsun = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public string c_abr
        {
            get { return _c_abr; }
            set { _c_abr = value; }
        }
        public int n_idtipo
        {
            get { return _n_idtipo; }
            set { _n_idtipo = value; }
        }
        public int n_essal
        {
            get { return _n_essal; }
            set { _n_essal = value; }
        }
        public int n_esent
        {
            get { return _n_esent; }
            set { _n_esent = value; }
        }
        public int n_seimp
        {
            get { return _n_seimp; }
            set { _n_seimp = value; }
        }
        public int n_numfil
        {
            get { return _n_numfil; }
            set { _n_numfil = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
        public int c_preser
        {
            get { return _c_preser; }
            set { _c_preser = value; }
        }
        public int n_escom
        {
            get { return _n_escom; }
            set { _n_escom = value; }
        }
    }
}
