using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Cls_ServiciosSunat
    {
        public string Contribuyente_Nombre = "";
        public string Contribuyente_NumRUC = "";
        public string Contribuyente_Direccion = "";
        public string Contribuyente_Estado = "";
        public string Contribuyente_Condicion = "";
        public string Contribuyente_FchIniAct = "";
        public string Contribuyente_ActEco = "";
        public string Contribuyente_TipCon = "";

        public string Ciudadano_Nombre = "";
        public string Ciudadano_NumDNI = "";
        public string Ciudadano_ApePat = "";
        public string Ciudadano_ApeMat = "";
        public void ConsultarRUC()
        {
            Formularios.FrmConsultaRuc xFrm = new Formularios.FrmConsultaRuc();
            xFrm.ShowDialog();
            if (xFrm.n_EstadoForm == 2)
            {
                Contribuyente_Nombre = xFrm.Contribuyente_Nombre;
                Contribuyente_NumRUC = xFrm.Contribuyente_NumRUC;
                Contribuyente_Direccion = xFrm.Contribuyente_Direccion;
                Contribuyente_Estado = xFrm.Contribuyente_Estado;
                Contribuyente_Condicion = xFrm.Contribuyente_Condicion;
                Contribuyente_FchIniAct = xFrm.Contribuyente_FchIniAct;
                Contribuyente_ActEco = xFrm.Contribuyente_ActEco;
                Contribuyente_TipCon = xFrm.Contribuyente_TipCon;
                xFrm.Close();
            }
        }
        public void ConsultarDNI()
        {
            Formularios.FrmConsultaDNI xFrm = new Formularios.FrmConsultaDNI();
            xFrm.ShowDialog();
            if (xFrm.n_EstadoForm == 2)
            {
                Ciudadano_Nombre = xFrm.Ciudadano_Nombre;
                Ciudadano_NumDNI = xFrm.Ciudadano_NumDNI;
                Ciudadano_ApePat = xFrm.Ciudadano_ApePat;
                Ciudadano_ApeMat = xFrm.Ciudadano_ApeMat;
                xFrm.Close();
            }
        }
    }
}
