using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;

namespace SIAC_Objetos.Ventas
{
    public class Ventas
    {
        // STRUCTURA PARA EL MANEJO DE LAS VENTAS
        public struct STU_VTA_VENTAS
        {
            public BE_VTA_VENTAS entDocumento;
            public List<BE_VTA_VENTASDET> entDocumentodetalle;
        };

        
    }
}
