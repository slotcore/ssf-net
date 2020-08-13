using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_COSTOPRODUCCIONDET
    {
        private int _n_id;
        private int _n_idcostoprod;
        private int _n_idparteprod;
        private int _n_idmov;
        private double _n_factdist;
        private double _n_costomp;
        private double _n_costomod;
        private double _n_costocif;
        private double _n_can;
        private int _n_idprod;
        private string _c_desparteprod;
        private int _n_idrec;
        private string _c_desprod;
        private string _c_desrec;
        private string _c_desunimed;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idcostoprod
        {
            get { return _n_idcostoprod; }
            set { _n_idcostoprod = value; }
        }
        public int n_idparteprod
        {
            get { return _n_idparteprod; }
            set { _n_idparteprod = value; }
        }
        public int n_idmov
        {
            get { return _n_idmov; }
            set { _n_idmov = value; }
        }
        public double n_factdist
        {
            get { return _n_factdist; }
            set { _n_factdist = value; }
        }
        public double n_costomp
        {
            get { return _n_costomp; }
            set { _n_costomp = value; }
        }
        public double n_costomod
        {
            get { return _n_costomod; }
            set { _n_costomod = value; }
        }
        public double n_costocif
        {
            get { return _n_costocif; }
            set { _n_costocif = value; }
        }
        public double n_costoprimo
        {
            get { return _n_costomp + _n_costomod; }
        }
        public double n_costototal
        {
            get { return _n_costomp + _n_costomod + _n_costocif; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public int n_idprod
        {
            get { return _n_idprod; }
            set { _n_idprod = value; }
        }
        public string c_desparteprod
        {
            get { return _c_desparteprod; }
            set { _c_desparteprod = value; }
        }
        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public string c_desprod
        {
            get { return _c_desprod; }
            set { _c_desprod = value; }
        }
        public string c_desrec
        {
            get { return _c_desrec; }
            set { _c_desrec = value; }
        }

        public string c_desunimed
        {
            get { return _c_desunimed; }
            set { _c_desunimed = value; }
        }
    }
}
