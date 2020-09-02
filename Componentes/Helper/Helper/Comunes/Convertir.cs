using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.Data;


namespace Helper.Comunes
{
    public class Convertir
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public double HoraEnDecimal(string c_HoraConvertir)
        {
            bool b_Error = false;
            double n_hora = 0;
            if (c_HoraConvertir == "") { return n_hora; }
            

            if (c_HoraConvertir.Length == 5) { c_HoraConvertir = c_HoraConvertir + ":00"; }
            //if ((c_HoraConvertir.Length > 8) || (c_HoraConvertir.Length < 8))
            //{
            //    b_Error = true;
            //    booOcurrioError = b_Error;
            //    StrErrorMensaje = "longitud de la hora no es valida";
            //    return n_hora;
            //}
            string c_Hora;
            string c_Minutos;
            string c_Segundos;
            double n_Hora = 0;

            string[] parts = c_HoraConvertir.Split(':');

            //c_Hora = c_HoraConvertir.Substring(0, 2);
            //c_Minutos = c_HoraConvertir.Substring(3, 2);
            //c_Segundos = c_HoraConvertir.Substring(6, 2);

            c_Hora = parts[0];
            c_Minutos = parts[1];
            c_Segundos = parts[2];

            // CALCULAMOS LA HORA EN DECIMALES
            n_Hora = (Convert.ToDouble(c_Hora) + (Convert.ToDouble(c_Minutos) / 60));
            
            booOcurrioError = b_Error;
            return n_Hora;
        }
        public string DecimalEnHoras(double n_DecimalConvertir)
        {
            bool b_Error = false;
            string c_hora;
            string c_minuto;
            double n_Minuto = 0;
            //c_hora = Convert.ToInt32(n_DecimalConvertir).ToString();
            c_hora = Math.Truncate(n_DecimalConvertir).ToString("00");
            n_Minuto = (n_DecimalConvertir - Convert.ToDouble(c_hora));
            c_minuto = (n_Minuto * 60).ToString("00");
            c_hora = c_hora + ":" + c_minuto + ":00";

            booOcurrioError = b_Error;
            return c_hora;
        }
        public double HallarPrecio(double n_Valor, double n_Precio)
        {
            double n_val = 0;

            string numStr = n_Valor.ToString("0.00");
            int n_entero = int.Parse(numStr.Split('.')[0]);
            float n_decimal = float.Parse("0," + numStr.Split('.')[1]);

            n_val = n_entero * n_Precio;

            if (n_decimal <= 50)
            {
                double n_valmit = 0;
                double n_mitadprecio = (n_Precio / 2);
                numStr = n_mitadprecio.ToString("0.00");
                int n_ent = int.Parse(numStr.Split('.')[0]);

                if ((n_mitadprecio - n_ent) == 0)
                {
                    n_valmit = n_mitadprecio;
                }
                else
                {
                    n_valmit = n_mitadprecio + 0.5;
                }
                //n_val = (n_val + n_mitadprecio);
                n_val = n_val + n_valmit;
            }
            else
            {
                n_val = n_val + n_Precio;
            }

            return n_val;
        }
        public int EsHoraFraccion(double n_Valor)
        {
            // 1 = es entero
            // 2 = es fraccion
            string numStr = n_Valor.ToString("0.00");
            int n_entero = int.Parse(numStr.Split('.')[0]);
            float n_decimal = float.Parse("0," + numStr.Split('.')[1]);
            int n_parteentera = int.Parse(numStr.Split('.')[0]);
            if (n_decimal < 50)
            {
                if (n_parteentera >= 1)
                {
                    if (n_decimal <= 50)
                    {
                        n_entero = 2;
                    }
                    else
                    { 
                        n_entero = 1;
                    }
                }
                else
                {
                    n_entero = 2;
                }
            }
            else
            {
                if (n_parteentera == 0)
                {
                    n_entero = 2;
                }
                else
                {
                    if (n_decimal == 50)
                    {
                        n_entero = 2;
                    }
                    else
                    { 
                        n_entero = 1;
                    }
                }
            }

            return n_entero;
        }
    }
}
