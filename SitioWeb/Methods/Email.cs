using SitioWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using UtileriasGlobales.Helpers;

namespace SitioWeb.Methods
{
    public class Email
    {
        #region Metodos


        /// <summary>
        /// Enviar los comprobantes por correo electronico
        /// </summary>
        /// <param name="para">Correos a quien se enviara el comprobante</param>
        /// <param name="copiaPara">Correos con copia</param>
        /// <param name="asunto">Asunto del mensaje</param>
        /// <returns></returns>
        public bool SendEmail(Contact contact, string subject, string bussiness)
        {
            bool error = false;
            try
            {
                //Obtiene el detalle de la configuracion de correo para recuperar contraseña
                var parametrosRemplazar = new Dictionary<string, string>();

                //Remplazar datos genereales del template
                string domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                parametrosRemplazar.Add("##fecha##", DateTime.Now.ToString());
                parametrosRemplazar.Add("##asunto##", subject);
                parametrosRemplazar.Add("##rutaTop##", domain + "/Content/img/logoCompleto.png");
                parametrosRemplazar.Add("##rutaBottom##", "");

                //Contacto
                parametrosRemplazar.Add("##nombre##", contact.name);
                parametrosRemplazar.Add("##correo##", contact.email);
                parametrosRemplazar.Add("##telefono##", contact.phone);
                parametrosRemplazar.Add("##mensaje##", contact.message);
                parametrosRemplazar.Add("##empresa##", bussiness); 

                //Leer el template
                string rutaDirectorio = System.Web.Hosting.HostingEnvironment.MapPath("/Content/messages/notificacionContacto.html");
                string contenido = System.IO.File.ReadAllText(rutaDirectorio);

                //Envia el correo
                string emailTo = System.Configuration.ConfigurationManager.AppSettings["EmailTo"];
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"];
                Mail.EnviarCorreo(emailTo, emailFrom, subject, contenido, true, null, null, null, null, parametrosRemplazar, emailFrom, bussiness);
            }
            catch (Exception ex)
            {
                error = true;
            }

            return error;
        }
        #endregion
    }
}