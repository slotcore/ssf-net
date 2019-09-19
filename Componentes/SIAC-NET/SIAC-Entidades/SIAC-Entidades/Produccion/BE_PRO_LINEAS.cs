using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_LINEAS
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idaux;
        private int _n_idrec;
        private string _c_des;
        private int _n_idunimed;
        private double _n_can;
        private double _n_efi;
        private double _n_numope;
        private double _n_kghor;
        private int _n_activo;
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
        public int n_idaux
        {
            get { return _n_idaux; }
            set { _n_idaux = value; }
        }
        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
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
        public double n_efi
        {
            get { return _n_efi; }
            set { _n_efi = value; }
        }
        public double n_numope
        {
            get { return _n_numope; }
            set { _n_numope = value; }
        }
        public double n_kghor
        {
            get { return _n_kghor; }
            set { _n_kghor = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
    }
}
