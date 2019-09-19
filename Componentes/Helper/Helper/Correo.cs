using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public class Correo
    {
        public string[] c_Destinatarios = { };
        public string[] c_ListaArchivos = { };
        public string c_CorreoRecibeCopia;
        public string c_CuentaOrigen;
        public string c_CuentaContraseña;
        public string c_DominioCuenta;
        public string c_AsuntoCorreo;
        public string c_CuerpoCorreo;

        public bool SeEnvio;

        public void EnviarCorreo()
        {
            int n_row=0;
            string filename = "";
            string c_ListaDestinatarios = "";
            
            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronicos a la que queremos enviar el mensaje
            int n_NumeroElementos = Convert.ToInt32(c_Destinatarios.GetLongLength(0)) - 1;
            for (n_row = 0; n_row <= n_NumeroElementos; n_row++)
            {
                mmsg.To.Add(c_Destinatarios[n_row]);                                    //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
                if (n_row > 0) { c_ListaDestinatarios = c_ListaDestinatarios + ", "; }
                c_ListaDestinatarios = c_ListaDestinatarios + c_Destinatarios[n_row];
            }

            // Lista de Archivos atachados
            n_NumeroElementos = Convert.ToInt32(c_ListaArchivos.GetLongLength(0)) - 1;
            for (n_row = 0; n_row <= n_NumeroElementos; n_row++)
            {
                filename = c_ListaArchivos[n_row];
                Attachment data = new Attachment(filename, MediaTypeNames.Application.Octet);
                mmsg.Attachments.Add(data);                                             // ASIGNAMOS EL ARCHIVO A ENVIAR
            }

            mmsg.Subject = c_AsuntoCorreo;                                      // ASIGNAMOS EL ASUNTO DEL CORREO
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            mmsg.Bcc.Add(c_CorreoRecibeCopia);                                  // CUENTA DE CORREO QUE RECIBIRA LA COPIA DEL CORREO (opcional)
            mmsg.Body = c_CuerpoCorreo;                                         // CUERPO DEL MEMSAJE
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = false;                                            //Si no queremos que se envíe como HTML
            mmsg.From = new System.Net.Mail.MailAddress(c_CuentaOrigen);        // CUENTA DE CORREO DE DONDE SE ENVIA EL CORREO

            /*-------------------------CLIENTE DE CORREO----------------------*/
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential(c_CuentaOrigen, c_CuentaContraseña);
            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

            //ventaspolysur@gmail.com
            if ((c_DominioCuenta == "smtp.gmail.com") || (c_DominioCuenta == "smtp.live.com"))
            {
                cliente.Host = c_DominioCuenta;
                cliente.Port = 587;
                //cliente.Credentials = new NetworkCredential("elpollongo@gmail.com", "010419762005");
                cliente.Credentials = new NetworkCredential(c_CuentaOrigen, c_CuentaContraseña);
                cliente.EnableSsl = true;
                cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                //cliente.UseDefaultCredentials = false;
            }
            else
            {
                cliente.Host = c_DominioCuenta; //Para Gmail "smtp.gmail.com";
            }
                    
            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
                MessageBox.Show("El correo se envio con exito a los siguientes destinatarios :  " + c_ListaDestinatarios, "Servicio Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                SeEnvio = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                MessageBox.Show("El correo no pudo ser enviado por el siguiente motivo :  " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                SeEnvio = false;
            }
        }
    }
}
