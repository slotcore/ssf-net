using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_COSTOPRODUCCION
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_anotra;
        private int _n_idmes;
        private int _n_idconfigval;
        private int _n_idresp;
        private string _c_numser;
        private string _c_numdoc;
        private string _c_obs;
        private double _n_costomod;
        private double _n_costoCif;
        private string _c_desconfigval;
        private string _c_desresp;
        private List<BE_CON_COSTOPRODUCCIONDET> _lst_items;

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
        public int n_idconfigval
        {
            get { return _n_idconfigval; }
            set { _n_idconfigval = value; }
        }

        public int n_idresp
        {
            get { return _n_idresp; }
            set { _n_idresp = value; }
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
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }

        public double n_costomod
        {
            get { return _n_costomod; }
            set { _n_costomod = value; }
        }
        public double n_costoCif
        {
            get { return _n_costoCif; }
            set { _n_costoCif = value; }
        }
        public string c_desconfigval
        {
            get { return _c_desconfigval; }
            set { _c_desconfigval = value; }
        }

        public string c_desresp
        {
            get { return _c_desresp; }
            set { _c_desresp = value; }
        }


        public List<BE_CON_COSTOPRODUCCIONDET> lst_items
        {
            get { return _lst_items; }
            set { _lst_items = value; }
        }

        public BE_CON_COSTOPRODUCCION()
        {
            _lst_items = new List<BE_CON_COSTOPRODUCCIONDET>();
        }
    }
}
