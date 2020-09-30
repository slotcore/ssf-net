using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Classes.Contabilidad
{
    [Serializable]
    public class CosteoProdException: Exception
    {
        public string CodItem { get; set; }
        public string DesItem { get; set; }
        public DateTime Fecha { get; set; }
        public string Almacen { get; set; }
        public string NumeroMovimiento { get; set; }

        public CosteoProdException()
        {

        }

        public CosteoProdException(string codItem
            , string desItem
            , DateTime fecha
            , string almacen
            , string numeroMovimiento
            , string message) : base(message)
        {
            CodItem = codItem;
            DesItem = desItem;
            Fecha = fecha;
            Almacen = almacen;
            NumeroMovimiento = numeroMovimiento;
            Fecha = fecha;
        }

        public CosteoProdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CosteoProdException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
