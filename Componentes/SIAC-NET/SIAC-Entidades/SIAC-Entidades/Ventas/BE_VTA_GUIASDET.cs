using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_GUIASDET
    {
        private int _n_idgui;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_canpro;
        private DateTime? _d_fchpro;
        private DateTime? _d_fchven;
        private string _c_numlot;
        private int _n_idtipexi;
        private int _n_iddocref;
        private double _n_preven;
        private double _n_candev;
        private double _n_preuni;
        public int n_idgui
        {
            get { return _n_idgui; }
            set { _n_idgui = value; }
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
        public double n_canpro
        {
            get { return _n_canpro; }
            set { _n_canpro = value; }
        }
        public DateTime? d_fchpro
        {
            get { return _d_fchpro; }
            set { _d_fchpro = value; }
        }
        public DateTime? d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public string c_numlot
        {
            get { return _c_numlot; }
            set { _c_numlot = value; }
        }
        public int n_idtipexi
        {
            get { return _n_idtipexi; }
            set { _n_idtipexi = value; }
        }
        public int n_iddocref
        {
            get { return _n_iddocref; }
            set { _n_iddocref = value; }
        }
        public double n_preven
        {
            get { return _n_preven; }
            set { _n_preven = value; }
        }
        public double n_candev
        {
            get { return _n_candev; }
            set { _n_candev = value; }
        }
        public double n_preuni
        {
            get { return _n_preuni; }
            set { _n_preuni = value; }
        }
    }
}
