using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using UtileriasGlobales.Helpers;
using UtileriasGlobales.Constants;

namespace UtileriasGlobales.Helpers
{
    /// <summary>
    /// Clase para el uso de sindicación de contenido (RSS).
    /// </summary>
    public class RSS
    {
        #region Metodos
        /// <summary>
        /// Genera un archivo xml con formato RSS
        /// </summary>
        /// <param name="ruta">Ruta fisica donde se guardara el archivo XML</param>
        /// <param name="dominio"></param>
        /// <param name="seccion"></param>
        /// <param name="title">Titulo del RSS</param>
        /// <param name="link">URL general del tipo de RSS</param>
        /// <param name="description">Descripcion General del RSS</param>
        /// <param name="copyright">Copyright de quien lo realiza</param>
        /// <param name="listado">Listado de elementos (titulo, contenido, fechaAlta)</param>
        /// <param name="urlImagen">URL de la Imagen para el RSS</param>
        /// <param name="titleImage">Titulo de la Imagen</param>
        public static void Generate(string ruta, string dominio, string seccion, string title, string link, string description, string copyright, DataTable listado, string urlImagen = null, string titleImage = null)
        {
            XmlTextWriter writer = new XmlTextWriter(ruta, Encoding.UTF8);
            writer.WriteStartDocument();

            // The mandatory rss tag
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");

            // The channel tag contains RSS feed details
            writer.WriteStartElement("channel");
            writer.WriteElementString("title", title);
            writer.WriteElementString("link", link);
            writer.WriteElementString("description", description);
            writer.WriteElementString("copyright", copyright);
            writer.WriteStartElement("atom:link");
            writer.WriteAttributeString("rel", "self");
            writer.WriteAttributeString("type", "application/rss+xml");
            writer.WriteEndElement();

            if (!string.IsNullOrEmpty(urlImagen))
            {
                // File Parade image    
                writer.WriteStartElement("image");
                writer.WriteElementString("url", urlImagen);
                writer.WriteElementString("title", titleImage);
                writer.WriteElementString("link", link);
                writer.WriteEndElement();
            }

            foreach (DataRowView row in listado.DefaultView)
            {
                writer.WriteStartElement("item");
                writer.WriteElementString("title", row["titulo"].ToString());
                writer.WriteElementString("description", row["contenido"].ToString());
                writer.WriteElementString("link", dominio.UrlCombine(seccion));
                writer.WriteElementString("pubDate", ((DateTime)row["fechaAlta"]).ToShortDateString());
                writer.WriteEndElement();
            }

            // Close all tags
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
        #endregion
    }
}
