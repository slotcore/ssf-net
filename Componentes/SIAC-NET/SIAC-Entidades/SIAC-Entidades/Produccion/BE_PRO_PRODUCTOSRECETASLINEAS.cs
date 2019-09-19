using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCTOSRECETASLINEAS
    {
        private int _n_idpro;
        private int _n_idrec;
        private int _n_id;
        private string _c_codlin;
        private string _c_deslin;
        private int _n_idunimed;
        private int _n_idite;
        private double _n_can;
        private double _n_numope;
        private double _n_efi;
        private double _n_tiepro;
        private double _n_prehorjor;
        private int _n_act;
        private string _c_obs;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_codlin
        {
            get { return _c_codlin; }
            set { _c_codlin = value; }
        }
        public string c_deslin
        {
            get { return _c_deslin; }
            set { _c_deslin = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public double n_numope
        {
            get { return _n_numope; }
            set { _n_numope = value; }
        }
        public double n_efi
        {
            get { return _n_efi; }
            set { _n_efi = value; }
        }
        public double n_tiepro
        {
            get { return _n_tiepro; }
            set { _n_tiepro = value; }
        }
        public double n_prehorjor
        {
            get { return _n_prehorjor; }
            set { _n_prehorjor = value; }
        }
        public int n_act
        {
            get { return _n_act; }
            set { _n_act = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
    }
}
