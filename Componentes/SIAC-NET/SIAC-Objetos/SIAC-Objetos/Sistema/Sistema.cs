using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Objetos.Sistema
{
    public class Sistema
    {
        public struct STU_SISTEMA
        {
            public int EMPRESAID;
            public string EMPRESANOMBRE;
            public int ANOTRABAJO;
            public int MESTRABAJO;
            public int USUARIOID;
            public int USUARIOPERFIL;
            public string USUARIOALIAS;
            public double TIPOCAMBIO;
            public int MONEDA;
            public string EMPRESARUC;

            public string RUTAREPORTES;
            public string RUTAIMAGENES;
            public string RUTAARCHIVOS;
            public string RUTAMANUAL;
            public string RUTADOCUMENTACION;

            public string RUTAFOTOITEMS;
            public string RUTAFOTOEMPLEADOS;
            public string RUTAPRODUCCIONCERTIFICADOS;

            public string BD_IP;
            public string BD_NOMSERVIDOR;
            public string BD_NOMBASEDATOS;
            public string BD_USUARIO;
            public string BD_CONTRASEÑA;
            public string BD_SERVIDOR;
            public string BD_PUERTO;

            public int SYS_UNIBD;
            public int SYS_UNIUSU;
            public string SYS_NOMBRE;
            public string SYS_NOMBREABREV;
            public string SYS_VESION;
            public string SYS_NOMALT;
            public int SYS_VERDATALT;

        };
    }
}
