using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;

namespace UtileriasGlobales.Helpers
{
    public class Html
    {
        #region Métodos

        /// <summary>
        /// Genera tags para el header de la página
        /// </summary>
        /// <param name="tag">Nombre del tag (tittle, metas)</param>
        /// <param name="name">Nombre del tag (keyword, description)</param>
        /// <param name="content">Contenido del tag</param>
        /// <returns>Control de Html</returns>
        /// <remarks>11/08/2011, CAmezcua:Creación</remarks>
        public static Control GeneraTagHeader(string tag, string name, string content)
        {
            Control ctrl = new Control();

            switch (tag)
            {
                case "HtmlMeta":
                    HtmlMeta meta = new HtmlMeta();
                    meta.Name = name;
                    meta.Content = content;
                    meta.ID = name;
                    ctrl = meta;
                    break;
                case "HtmlTitle":
                    HtmlTitle tittle = new HtmlTitle();
                    tittle.Text = content;
                    tittle.ID = "title";
                    ctrl = tittle;
                    break;
                default:
                    break;
            }
            return ctrl;
        }

        /// <summary>
        /// Genera un tag img con los atributos url y alt
        /// </summary>
        /// <param name="url">Url de la imagen</param>
        /// <param name="alt">Alt de la imagen</param>
        /// <param name="width">Ancho de la imagen(opcional)</param>
        /// <param name="height">Alto de la imagen(opcional)</param>
        /// <returns>Cadena del tag img</returns>
        /// <remarks>25/08/2011, CAmezcua:Creación</remarks>
        public static string GeneraTagImagen(string url, string alt, Int32 width = 0, Int32 height = 0, string clase = null)
        {

            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, url);
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, alt);

                if (width != 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, width.ToString());
                }

                if (height != 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Height, height.ToString());
                }

                if (!string.IsNullOrEmpty(clase))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, clase);
                }

                
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }

            return stringWriter.ToString();

        }


        /// <summary>
        /// Genera un tag Script para mostrar
        /// los archivos flash
        /// </summary>
        /// <param name="url">Url del archivo</param>
        /// <param name="type">Tipo del script</param>
        /// <param name="width">Ancho del archivo</param>
        /// <param name="height">Alto del archivo</param>
        /// <returns>Tag script</returns>
        /// <remarks>25/08/2011, CAmezcua:Creación</remarks>
        public static string GeneraTagScript(string url, string type, Int32 width = 0, Int32 height = 0)
        { 
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Type, type);
                writer.RenderBeginTag(HtmlTextWriterTag.Script);
                writer.Write("AC_FL_RunContent('codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0', 'width', '" + width +"', 'height', '" + height + "', 'src', '" + url +"', 'quality', 'high', 'wmode', 'transparent', 'pluginspage', 'http://www.macromedia.com/go/getflashplayer', 'movie', '"+ url +"');");
                writer.RenderEndTag();
                
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Genera un tag Object para mostrar
        /// archivos de video
        /// </summary>
        /// <param name="src">Url del archivo</param>
        /// <param name="width">Ancho del objeto</param>
        /// <param name="height">Alto del objeto</param>
        /// <returns>Tag object</returns>
        /// <remarks>29/08/2011, CAmezcua:Creación</remarks>
        public static string GeneraTagObject(string src, int width, int height)
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, width.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Height, height.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Object);

                writer.AddAttribute(HtmlTextWriterAttribute.Name, "movie");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, src);
                writer.RenderBeginTag(HtmlTextWriterTag.Param);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Name, "allowFullScreen");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, "true");
                writer.RenderBeginTag(HtmlTextWriterTag.Param);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Name, "allowscriptaccess");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, "always");
                writer.RenderBeginTag(HtmlTextWriterTag.Param);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Src, src);
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "application/x-shockwave-flash");
                writer.AddAttribute(HtmlTextWriterAttribute.Width, width.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Height, height.ToString());
                writer.AddAttribute("allowscriptaccess", "always");
                writer.AddAttribute("allowfullscreen", "true");
                writer.RenderBeginTag(HtmlTextWriterTag.Embed);
                writer.RenderEndTag();

                writer.RenderEndTag();

            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Genera un tag Link A con su nombre y url
        /// </summary>
        /// <param name="label">Nombre del link</param>
        /// <param name="url">Url del link</param>
        /// <returns>Tag anchor en html</returns>
        /// <remarks>18/08/2011, CAmezcua:Creación</remarks>
        public static string GeneraLink(string label, string url, string target, string clase)
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
                writer.AddAttribute(HtmlTextWriterAttribute.Target, target);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, clase);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(label);
                writer.RenderEndTag();
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Genera un tag Br
        /// </summary>
        /// <returns>Tag Br</returns>
        /// <remarks>01/09/2011, CAmezcua:Creación</remarks>
        public static string GeneraBr()
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Genera un tag div
        /// </summary>
        /// <param name="id">Id del div</param>
        /// <param name="contenido">Contenindo del div</param>
        /// <param name="cssclass">Css class</param>
        /// <returns>Tag div</returns>
        public static string GeneraDiv(string id, string contenido, string cssclass="")
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                if (cssclass != String.Empty)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, cssclass);

                writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(contenido);
                writer.RenderEndTag();
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Genera un tag bold
        /// </summary>
        /// <param name="contenido">Contenindo del bold</param>
        /// <returns>Tag Bold</returns>
        /// <remarks>02/11/2011, CAmezcua:Creación</remarks>
        public static string GeneraBold(string contenido)
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.B);
                writer.Write(contenido);
                writer.RenderEndTag();
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Genera un tag HR (Linea)
        /// </summary>
        /// <returns>Tag HR</returns>
        /// <remarks>02/11/2011, CAmezcua:Creación</remarks>
        public static string GeneraLinea(int porcentaje =0)
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                if(porcentaje!=0)
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, porcentaje+"%");
                writer.RenderBeginTag(HtmlTextWriterTag.Hr);
            }

            return stringWriter.ToString();
        }



        /// <summary>
        /// Genera un tag P
        /// </summary>
        /// <param name="contenido">Contenindo del p</param>
        /// <param name="cssclass">Css class</param>
        /// <returns>Tag p</returns>
        public static string GeneraParrafo( string contenido, string cssclass = "")
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                if (cssclass != String.Empty)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, cssclass);

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(contenido);
                writer.RenderEndTag();
            }

            return stringWriter.ToString();
        }

        #endregion

        /// <summary>
        /// Crea una tabla con una cantidad de filas y columnas determinadas
        /// e inserta los datos es un arreglo de arreglos (Para correos)
        /// </summary>
        /// <param name="filas">Cantidad de filas de la tabla</param>
        /// <param name="columnas">Cantidad de columnas de la tabla</param>
        /// <param name="datos">DAtos que deben contener las celdas de la tabla</param>
        /// <returns>Cadena con el tag table y su contenido</returns>
        /// <remarks>01/07/2011, CAmezcua:Creación</remarks>
        public static string ObtenerTabla(Int32 filas, Int32 columnas, ArrayList datos)
        {
            //Inicializa la instancia de StringWriter.
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Arial");
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "12px");

                writer.RenderBeginTag(HtmlTextWriterTag.Table);

                for (Int32 i = 0; i <= filas - 1; i++)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    for (Int32 j = 0; j <= columnas - 1; j++)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);

                        if (datos[i].GetType().Name == "String")
                        {
                            writer.Write(datos[i].ToString());
                        }
                        else if (datos[i].GetType().Name == "ArrayList" & (datos[i] != null))
                        {
                            writer.Write(((ArrayList)datos[i])[j].ToString());
                        }

                        writer.RenderEndTag();
                    }

                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }

            return stringWriter.ToString();
        }
    }
}
