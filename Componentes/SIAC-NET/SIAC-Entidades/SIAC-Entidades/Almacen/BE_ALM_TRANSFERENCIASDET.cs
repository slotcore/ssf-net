using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_TRANSFERENCIASDET
    {
        private int _n_id;
        private int _n_idtrans;
        private int _n_idite;
        private int _n_idpre;
        private double _n_can;
        private int _n_idalm;
        private string _c_numlot;
        private int _n_idtippro;
        private DateTime ?_d_fchpro;
        private DateTime ?_d_fchven;
        private int ?_n_iddep;
        private int ?_n_idpro;
        private int ?_n_iddis;
        private string _c_desori;
        private string _c_marca;
        private string _h_horing;
        private string _h_horsal;
        private int _n_estpro;
        private double _n_preuni;
        private double _n_pretot;

        private string _c_itedes;     // DESCRIPCION DEL ITEM
        private string _c_itepredes;  // ABREVIATURA DE LA PRESENTACION
        private string _c_tipexides;  // DESCRIPCION DEL TIPO DE EXISTENCIA

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idtrans
        {
            get { return _n_idtrans; }
            set { _n_idtrans = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idpre
        {
            get { return _n_idpre; }
            set { _n_idpre = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public int n_idalm
        {
            get { return _n_idalm; }
            set { _n_idalm = value; }
        }
        public string c_numlot
        {
            get { return _c_numlot; }
            set { _c_numlot = value; }
        }
        public int n_idtippro
        {
            get { return _n_idtippro; }
            set { _n_idtippro = value; }
        }
        public DateTime ?d_fchpro
        {
            get { return _d_fchpro; }
            set { _d_fchpro = value; }
        }
        public DateTime ?d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public int ?n_iddep
        {
            get { return _n_iddep; }
            set { _n_iddep = value; }
        }
        public int ?n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int ?n_iddis
        {
            get { return _n_iddis; }
            set { _n_iddis = value; }
        }
        public string c_desori
        {
            get { return _c_desori; }
            set { _c_desori = value; }
        }
        public string c_marca
        {
            get { return _c_marca; }
            set { _c_marca = value; }
        }
        public string h_horing
        {
            get { return _h_horing; }
            set { _h_horing = value; }
        }
        public string h_horsal
        {
            get { return _h_horsal; }
            set { _h_horsal = value; }
        }
        public int n_estpro
        {
            get { return _n_estpro; }
            set { _n_estpro = value; }
        }
        public double n_preuni
        {
            get { return _n_preuni; }
            set { _n_preuni = value; }
        }
        public double n_pretot
        {
            get { return _n_pretot; }
            set { _n_pretot = value; }
        }
        public string c_itedes
        {
            get { return _c_itedes; }
            set { _c_itedes = value; }
        }
        public string c_itepredes
        {
            get { return _c_itepredes; }
            set { _c_itepredes = value; }
        }
        public string c_tipexides
        {
            get { return _c_tipexides; }
            set { _c_tipexides = value; }
        }
    }
}
