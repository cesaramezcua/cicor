using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web.UI;
using UtileriasGlobales.Constants;
using System.Net;

namespace UtileriasGlobales.Helpers
{

    /// <summary>
    /// Clase que permite el envio de correo electrónico.
    /// </summary>
    public class Mail
    {
       
        #region Métodos

        /// <summary>
        /// Envía el correo a traves de un servidor SMTP.
        /// </summary>
        /// <param name="correoPara">Destinatario</param>
        /// <param name="correoDe">Remitente</param>
        /// <param name="asunto">Tema del correo(Subject)</param>
        /// <param name="contenido">Cuerpo el correo</param>
        /// <param name="isBodyHtml">Valida si el contenido es html</param>
        /// <param name="tema">Dominio del sitio</param>
        /// <param name="correoCC">Lista de Copia de Correo separada por comas </param>
        /// <param name="correoBCC">Lista de Copia oculta de correo separada por comas </param>
        /// <param name="adjuntos">Coleccion de archivos adjuntos, Ejemplo new AttachmentCollection();</param>
        /// <param name="parametrosRemplazar">Diccionario de datos a remplazar, Ejemplo  new Dictionary string, string (); despues Add </param>
        /// <returns>Estatus de envio</returns>
        public static bool EnviarCorreo(string correoPara, string correoDe, string asunto, string contenido, object isBodyHtml = null, string tema = null, string correoCC = null, string correoBCC = null, AttachmentCollection adjuntos = null, IEnumerable<KeyValuePair<string, string>> parametrosRemplazar = null, string responderPara = null, string fromNameDisplay = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                if (!string.IsNullOrEmpty(fromNameDisplay))
                    mail.From = new MailAddress(correoDe, fromNameDisplay);
                else
                    mail.From = new MailAddress(correoDe);

                mail.To.Add(correoPara.Replace(';', ','));
                mail.Priority = MailPriority.Normal;
                mail.Subject = asunto;

                if (isBodyHtml != null)
                    mail.IsBodyHtml = isBodyHtml.ToString().ToBoolean();

                if (parametrosRemplazar != null)
                    foreach (KeyValuePair<string, string> token in parametrosRemplazar)
                        contenido = contenido.Replace(token.Key, token.Value);

                if (tema != null)
                    mail.Body = ObtenerContenidoHtml(contenido, tema);
                else
                    mail.Body = contenido;

                if (!string.IsNullOrEmpty(responderPara))
                {
                    responderPara = responderPara.Replace(';', ',');
                    string[] mails = responderPara.Split(',');
                    foreach (string item in mails)
                        mail.ReplyToList.Add(new System.Net.Mail.MailAddress(item));
                }

                if (!string.IsNullOrEmpty(correoCC))
                {
                    correoCC = correoCC.Replace(';', ',');
                    string[] mails = correoCC.Split(',');
                    foreach (string item in mails)
                        mail.CC.Add(new System.Net.Mail.MailAddress(item));
                }

                if (!string.IsNullOrEmpty(correoBCC))
                {
                    correoBCC = correoBCC.Replace(';', ',');
                    string[] mails = correoBCC.Split(',');
                    foreach (string item in mails)
                        mail.Bcc.Add(new System.Net.Mail.MailAddress(item));
                }

                if (adjuntos != null)
                    foreach (Attachment fAttachment in adjuntos) mail.Attachments.Add(fAttachment);

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, Configuration.EXCEPTION_POLICY);
                throw ex;
            }

            return true;
        }


        /// <summary>
        /// Agrega el top y bottom al cuerpo del correo
        /// </summary>
        /// <param name="contenido">Cuerpo del correo</param>
        /// <param name="tema">Dominio donde consultará las rutas de imagen para el top y bottom</param>
        /// <returns>correo con formato html</returns>
        private static string ObtenerContenidoHtml(string contenido, string tema)
        {
            string htmlMail = String.Empty;
            string fecha = DateTime.Now.CustomFormat(2);


            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "tabla");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);

                //Top
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(Helpers.Html.GeneraTagImagen("~".UrlCombine(Configuration.RUTA_IMAGES).UrlCombine(Configuration.IMAGEN_EMAIL_TOP), String.Empty));
                writer.RenderEndTag();
                writer.RenderEndTag();

                //Espacio
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("&nbsp;");
                writer.RenderEndTag();
                writer.RenderEndTag();

                //Espacio
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("&nbsp;");
                writer.RenderEndTag();
                writer.RenderEndTag();

                //Fecha
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px;");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "label");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(fecha);
                writer.RenderEndTag();
                writer.RenderEndTag();

                //Mensaje
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(contenido);
                writer.RenderEndTag();
                writer.RenderEndTag();

                //Bottom
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(Helpers.Html.GeneraTagImagen("~".UrlCombine(Configuration.RUTA_IMAGES).UrlCombine(Configuration.IMAGEN_EMAIL_BOTTOM), String.Empty));
                writer.RenderEndTag();
                writer.RenderEndTag();

                writer.RenderEndTag();

            }

            return stringWriter.ToString();

        }

        /// <summary>
        /// Obtiene el contenido de un arhivo 
        /// </summary>
        /// <param name="path">ruta del archivo o URL.</param>
        /// <returns></returns>
        public static string ObtenerTemplateArchivo(string path)
        {
            try
            {
                if (path.StartsWith("http://") || path.StartsWith("https://") || path.StartsWith("www.")) return ObtienerTemplateArchivo(new Uri(path));
                using (var sr = new StreamReader(path)) return sr.ReadToEnd();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Obtiene el template del correo desde una ruta HTML o HTMLS
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <returns></returns>
        public static string ObtienerTemplateArchivo(Uri remoteAddress)
        {
            var result = String.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(remoteAddress);
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream())) result = sr.ReadToEnd();
                }
            }
            catch
            {
                return result;
            }
            return result;
        }

        #endregion
    }
}
