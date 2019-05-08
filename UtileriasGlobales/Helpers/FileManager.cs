using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Drawing;
using System.Reflection;

namespace UtileriasGlobales.Helpers
{
    /// <summary>
    /// Clase para el manejo de archivos.
    /// </summary>
    public class FileManager
    {

        #region Metodos
        /// <summary>
        /// Obtiene la página que se está ejecutando.
        /// </summary>
        /// <param name="pagina">objeto tipo pagina</param>
        /// <returns>Nombre del script name</returns>
        /// <remarks>
        /// 24-02-2011, CAmezcua: Creación.
        /// </remarks>
        public static string ObtenerPaginaActual(Page pagina)
        {
            string paginaActual = String.Empty;
            try
            {
                if (pagina != null)
                {
                    paginaActual = Path.GetFileName(pagina.Request.Path).ToLower();
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, UtileriasGlobales.Constants.Configuration.EXCEPTION_POLICY);
                throw ex;
            }
            return paginaActual;
        }

        /// <summary>
        /// Obtiene el directorio donde se encuentra la página que se está ejecutando.
        /// </summary>
        /// <param name="pagina">objeto tipo pagina</param>
        /// <returns>nombre del directorio.</returns>
        /// <remarks>
        /// 24-02-2011, CAmezcua: Creación.
        /// </remarks>
        public static string ObtenerDirectorioActual(Page pagina)
        {
            string directorioActual = String.Empty;
            try
            {
                if (pagina != null)
                {
                    directorioActual = Path.GetDirectoryName(pagina.Request.Path).ToLower().Trim();
                    int posicion = directorioActual.LastIndexOf("\\") + 1;
                    directorioActual = directorioActual.Substring(posicion);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, UtileriasGlobales.Constants.Configuration.EXCEPTION_POLICY);
                throw ex;
            }
            return directorioActual;
        }

        /// <summary>
        /// Función que valida si existe un archivo en el sistema
        /// </summary>
        /// <param name="dominio">Cadena Direccion del servidor donde se encuentra el archivo</param>
        /// <param name="ruta">Cadena Ruta dentro del sistema de archivos</param>
        /// <param name="carpeta">Cadena Folder donde se debe localizar</param>
        /// <param name="archivo">Cadena Nombre del archivo a buscar</param>
        /// <param name="extension">Cadena extensión del archivo</param>
        /// <returns>URL del documento</returns>
        /// <remarks>
        /// 07/03/2011, CAmezcua: Creación.
        /// </remarks>
        public static string validarDocumento(string dominio, string ruta, string carpeta, string archivo, string extension)
        {
            string documento = String.Empty;
            string rutaFisica = Path.Combine(Path.Combine(ruta, carpeta), archivo + extension);
            string rutaRelativa = dominio.UrlCombine(carpeta);

            try
            {
                bool blnFileExist = File.Exists(rutaFisica);

                if (blnFileExist)
                {
                    archivo = rutaRelativa.UrlCombine(archivo + extension);
                    documento = archivo;
                }
                else
                {
                    documento = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, UtileriasGlobales.Constants.Configuration.EXCEPTION_POLICY);
                throw ex;
            }

            return documento;
        }


        /// <summary>
        /// fileRename mueve y/o renombra un archivo de una ruta a otra
        /// </summary>
        /// <param name="sourcePath">Ruta de origen</param>
        /// <param name="destinationPath">Ruta destino</param>
        /// <remarks>
        /// 15/06/2011, CAmezcua: Creación.
        /// </remarks>
        public static void FileRename(string sourcePath, string destinationPath) {
            if (FileExists(sourcePath)) {
                File.Copy(sourcePath, destinationPath, true);
                File.Delete(sourcePath);
            }
        }// end fileRename

        /// <summary>
        /// fileExists valida si un archivo existe o no
        /// </summary>
        /// <param name="fileFullPath">Ruta absoluta del archivo</param>
        /// <returns>True->Si el archivo existe. False->En caso contrario</returns>
        /// <remarks>
        /// 15/06/2011, CAmezcua: Creación.
        /// </remarks>
        public static bool FileExists(string fileFullPath) {
            System.IO.FileInfo f = new System.IO.FileInfo(fileFullPath);
            return f.Exists;
        }

        /// <summary>
        /// FileDelete elimina un archivo si éste existe
        /// </summary>
        /// <param name="sourcePath">Ruta absoluta del archivo a eliminar</param>
        /// <remarks>
        /// 15/06/2011, CAmezcua: Creación.
        /// </remarks>
        public static void FileDelete(string sourcePath) {
            if (FileExists(sourcePath)) {
                File.Delete(sourcePath);
            }
        }

        /// <summary>
        /// Despliegue de un archivo a través de un enlace
        /// </summary>
        /// <param name="dominio"></param>
        /// <param name="ruta"></param>
        /// <param name="carpeta"></param>
        /// <param name="archivo"></param>
        /// <param name="leyenda"></param>
        /// <param name="mensajeDefault"></param>
        /// <returns></returns>
        /// <remarks>
        /// 30/06/2011, CAmezcua: Creación.
        /// </remarks>
        public static string validarArchivo(string dominio, string ruta, string carpeta, string archivo, string leyenda, string mensajeDefault) {
            string htmlArchivo = String.Empty;
            string rutaFisica = Path.Combine(Path.Combine(ruta, carpeta), archivo);
            string rutaRelativa = dominio.UrlCombine(carpeta);
            try {
                bool blnFileExist = System.IO.File.Exists(rutaFisica);

                if (blnFileExist) {
                    archivo = rutaRelativa.UrlCombine(archivo);
                    htmlArchivo = "<a href='" + archivo + "' target=\"blank\" >" + leyenda + "</a>";
                } else {
                    htmlArchivo = mensajeDefault;
                }
            } catch (Exception ex) {
                ExceptionPolicy.HandleException(ex, Constants.Configuration.EXCEPTION_POLICY);
                throw ex;
            }

            return htmlArchivo;
        }


        /// <summary>
        /// Crea los directorios que no existan de acuetrdo a la ruta
        /// </summary>
        /// <param name="ruta">ruta para crear directorios</param>
        public static void CrearDirectorios(string ruta)
        {
            try
            {
                string[] directorios = ruta.Split('\\');

                string rutaTemporal = string.Empty;
                bool banderaRaiz = true;
                foreach (string item in directorios)
                {
                    if (banderaRaiz)
                    {
                        rutaTemporal += item + "\\";
                        banderaRaiz = false;
                    }
                    else
                    {
                        rutaTemporal = Path.Combine(rutaTemporal, item);
                        if (rutaTemporal.Contains("shared"))
                        {
                            if (!Directory.Exists(rutaTemporal))
                                Directory.CreateDirectory(rutaTemporal);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message, "original");
            }
        }

        /// <summary>
        /// Convertir arreglo de bytes a archivo
        /// </summary>
        /// <param name="_FileName">ruta y nombre del archivo</param>
        /// <param name="_ByteArray">arreglo de bytes para archivo</param>
        /// <returns></returns>
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        /// <summary>
        /// Le el archivo y regresa un arreglo de bytes, cierra el archivo al final
        /// </summary>
        /// <param name="filePath">Ruta del archivo</param>
        /// <returns>Arreglo de bytes</returns>
        public static byte[] ReadFileBytes(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        /// <summary>
        /// Regresa una nueva imagen con base a un arreglo de bytes
        /// </summary>
        /// <param name="byteArrayIn">Arreglo de bytes</param>
        /// <returns>Imagen</returns>
        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// !!CERRAR EL ARCHIVO DESPUES DE USARLO!!
        /// Le el archivo y regresa un stream
        /// esto para el problema de uso de archivos
        /// fileStream.Close();
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileStream ReadFileStream(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }


        /// <summary>
        /// Metodo que indica si un archivo se encuentra bloqueado por otro proceso
        /// </summary>
        /// <param name="file">ruta del archivo fisico</param>
        /// <returns></returns>
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// Lee el archivo de un stream a byte[]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            input.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Regresa la imagen de un nuevo tamaño
        /// </summary>
        /// <param name="imgToResize">Imagen principal</param>
        /// <param name="size">Nuevo tamaño</param>
        /// <returns></returns>
        public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            return (System.Drawing.Image)(new Bitmap(imgToResize, size));
        }

        #endregion
    }    
}