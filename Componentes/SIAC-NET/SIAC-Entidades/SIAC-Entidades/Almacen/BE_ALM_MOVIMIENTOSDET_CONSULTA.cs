using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_MOVIMIENTOSDET_CONSULTA : BE_ALM_MOVIMIENTOSDET
    {
        private string _c_itedes;     // DESCRIPCION DEL ITEM
        private string _c_itepredes;  // ABREVIATURA DE LA PRESENTACION
        private string _c_tipexides;  // DESCRIPCION DEL TIPO DE EXISTENCIA
        private double _n_canconmp;   // CANTIDAD CONSUMIDA DE LA MP ESTE DATO SOLO SE LLENARA CUANDO LA CONSULTA SEA LLAMADA DESDE PRODUCCION
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
        public double n_canconmp
        {
            get { return _n_canconmp; }
            set { _n_canconmp = value; }
        }
    }
}
