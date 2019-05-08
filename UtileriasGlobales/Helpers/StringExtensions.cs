using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace UtileriasGlobales.Helpers
{
    public static class StringExtensions
    {
        #region Constantes
        /// <summary>
        /// Constantes que definen los formatos de las cadenas.
        /// </summary>
        private const string FORMATO_CURRENCY = "{0:c}";

        #endregion 
        #region Metodos

        /// <summary>
        /// Obtiene los primeros n caracteres de una Cadena.
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <param name="count">Cantidad de caracteres</param>
        /// <returns>Primeros n caracteres.</returns>
        public static string Left(this string s, int count)
        {
            if (count <= s.Length)
                return s.Substring(0, count);
            else
                return s;
        }

        /// <summary>
        /// Obtiene los últimos n caracteres de una Cadena.
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <param name="count">Cantidad de caracteres</param>
        /// <returns>Ultimos n caracteres.</returns>
        public static string Right(this string s, int count)
        {
            if (count <= s.Length)
                return s.Substring(s.Length - count, count);
            else
                throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Obtiene n caracteres de la Cadena 
        /// empezando en una posición determinada.
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <param name="index">Posición inicial</param>
        /// <param name="count">Cantidad de caracteres</param>
        /// <returns>N caracteres de la Cadena</returns>
        public static string Mid(this string s, int index, int count)
        {
            if (s.Length - count >= index)
                return s.Substring(index, count);
            else
                throw new IndexOutOfRangeException();

        }

        /// <summary>
        /// Obtiene la representación entera de una Cadena si es un
        /// entero, cero en otro caso.
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Representación entera de la s.</returns>
        public static int ToInteger(this string s)
        {
            int integerValue = 0;
            int.TryParse(s, out integerValue);
            return integerValue;
        }

        /// <summary>
        /// Obtiene la representación entera de una Cadena si es un
        /// entero, cero en otro caso.
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Representación entera de la s.</returns>
        public static short ToShort(this string s)
        {
            short integerValue = 0;
            short.TryParse(s, out integerValue);
            return integerValue;
        }

        /// <summary>
        /// Verifica si una cadena es una representación de un entero.
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>True si es entero, False en otro caso.</returns>
        public static bool IsInteger(this string s)
        {
            Regex regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");
            return regularExpression.Match(s).Success;
        }

        /// <summary>
        /// Valida un parámetro y lo convierte a float o 0.0 si es vacío
        /// </summary>
        /// <param name="s">parámetro a validar</param>
        /// <returns>valor flotante</returns>
        /// <remarks>
        /// 07/04/2011, CAmezcua: Creación
        /// </remarks>
        public static float ToFloat(this string s)
        {
            float resultado = 0.0f;
            if (!float.TryParse(s, out resultado)) resultado = 0.0f;
            return resultado;
        }

        /// <summary>
        /// Valida un parámetro y lo convierte a decimal o 0.0 si es vacío
        /// </summary>
        /// <param name="s">parámetro a validar</param>
        /// <returns>valor decimal</returns>
        /// <remarks>
        /// 07/04/2011, CAmezcua: Creación
        /// </remarks>
        public static decimal ToDecimal(this string s)
        {
            decimal resultado = 0;
            if (!decimal.TryParse(s, out resultado)) resultado = 0;
            return resultado;
        }

        /// <summary>
        /// Valida un parámetro y lo convierte a decimal o 0.0 si es vacío
        /// </summary>
        /// <param name="s">parámetro a validar</param>
        /// <returns>valor decimal</returns>
        /// <remarks>
        /// 07/04/2011, CAmezcua: Creación
        /// </remarks>
        public static double ToDouble(this string s)
        {
            double resultado = 0;
            if (!double.TryParse(s, out resultado)) resultado = 0;
            return resultado;
        }

        /// <summary>
        /// Valida un parámetro y lo convierte a boolean
        /// </summary>
        /// <param name="s">parámetro a validar</param>
        /// <returns>valor booleano</returns>
        /// <remarks>
        /// 10/03/2011, CAmezcua: Creación.
        /// </remarks>
        public static bool ToBoolean(this string s)
        {
            bool resultado = false;
            if (!bool.TryParse(s, out resultado)) resultado = false;
            return resultado;
        }


        /// <summary>
        /// Sustituye acentos, reemplaza espacios por
        /// guiones medios y convierte a minúsculas
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Cadena normalizada</returns>
        public static string CustomNormalize(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("á", "a");
                s = s.Replace("é", "e");
                s = s.Replace("í", "i");
                s = s.Replace("ó", "o");
                s = s.Replace("ú", "u");
                s = s.Replace("Á", "A");
                s = s.Replace("É", "E");
                s = s.Replace("Í", "I");
                s = s.Replace("Ó", "O");
                s = s.Replace("Ú", "U");
                s = s.Replace("&", "y");
                s = s.Replace("+", "-");
                s = s.Replace(" ", "-").ToLower();
                s = s.Replace("/", "-").ToLower();
                s = s.Replace(",", String.Empty);
                s = s.Replace("\"", String.Empty);
                s = s.Replace("'", String.Empty);
                s = s.Replace("(", String.Empty);
                s = s.Replace(")", String.Empty);
                s = s.Replace("'", String.Empty);
                s = s.Replace("®", String.Empty);
                s = s.TrimEnd('.');
                s = Slugify(s);
                return s;
            }
            return string.Empty;
        }

        public static string Slugify(string text)
        {
            try
            {
                IdnMapping idnMapping = new IdnMapping();
                text = idnMapping.GetAscii(text);

                text = RemoveAccent(text).ToLower();

                //  Remove all invalid characters.  
                text = Regex.Replace(text, @"[^a-z0-9\s-_]", "");

                //  Convert multiple spaces into one space
                text = Regex.Replace(text, @"\s+", " ").Trim();

                //  Replace spaces by underscores.
                text = Regex.Replace(text, @"\s", "-");
            }
            catch (Exception)
            {
            }

            return text;
        }

        public static string RemoveAccent(string text)
        {
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);

            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Sustituye acentos, reemplaza espacios por
        /// contraparte html
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Cadena normalizada</returns>
        public static string CustomHTML(this string s)
        {
            s = s.Replace("á", "&acute;");
            s = s.Replace("é", "&ecute;");
            s = s.Replace("í", "&icute;");
            s = s.Replace("ó", "&ocute;");
            s = s.Replace("ú", "&ucute;");
            s = s.Replace("Á", "&Acute;");
            s = s.Replace("É", "&Ecute;");
            s = s.Replace("Í", "&Icute;");
            s = s.Replace("Ó", "&Ocute;");
            s = s.Replace("Ú", "&Ucute;");
            s = s.Replace("ñ", "&ntilde;");
            s = s.Replace("Ñ", "&Ntilde;");
            return s;
        }

        /// <summary>
        /// Formatea la cadena con los parámetros
        /// que se envían en el listado
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <param name="values">Diccionario de parámetros</param>
        /// <returns>Cadena formateada</returns>
        public static string CustomFormat(this string s, Dictionary<string, string> values)
        {
            if (values != null)
            {
                foreach (string key in values.Keys)
                {
                    s = s.Replace("{" + key + "}", values[key].ToTitleCase());
                }
            }

            //Elimina los marcadores que no se hayan reemplazado
            Regex oReg = new Regex(@"\{\w+\}");
            Match oMatch = oReg.Match(s);

            if (oMatch.Success)
                s = oReg.Replace(s, "");

            return s;
        }

        /// <summary>
        /// Convierte la primera letra de cada palabra
        /// en la cadena a mayúsculas
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Cadena con mayúsculas</returns>
        public static string ToTitleCase(this string s)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(s.ToLower().Replace("-", " "));
        }

        /// <summary>
        /// Convierte la primera letra 
        /// de la cadena a minúsculas
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Cadena con la primera letra en minúsculas</returns>
        public static string FirstLetterLower(this string s)
        {
            s = s[0].ToString().ToLower() + s.Substring(1, s.Length - 1);

            return s;
        }

        /// <summary>
        /// Verifica si la cadena contiene todos 
        /// los elementos del listado
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <param name="keys">Listado de elementos</param>
        /// <returns>True si los contiene todos, False en otro caso.</returns>
        public static bool CustomContains(this string s, string[] keys)
        {
            bool result = true;

            if (keys != null && keys.Count() != 0)
            {
                foreach (string key in keys)
                {
                    if (key != String.Empty && !s.Contains(key))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else if (s != String.Empty)
                result = false;

            return result;
        }


        /// <summary>
        /// Construye URL compuesta de una URL base y niveles posteriores.
        /// </summary>
        /// <param name="url1">Url base</param>
        /// <param name="url2">Url .adicional</param>
        /// <returns>Url compuesta</returns>
        /// <remarks>
        /// 10/03/2011, CAmezcua: Creación.
        /// </remarks>
        public static string UrlCombine(this string url1, string url2)
        {
            string url = String.Empty;
            if (!String.IsNullOrEmpty(url1) && !String.IsNullOrEmpty(url2))
            {
                url1 = url1.TrimEnd('/');
                url2 = url2.TrimStart('/');
                url = string.Format("{0}/{1}", url1, url2);
            }

            return url;
        }

        /// <summary>
        /// Construye URL compuesta de una URL base y niveles posteriores.
        /// </summary>
        /// <param name="url1">Url base</param>
        /// <param name="url2">Url .adicional</param>
        /// <returns>Url compuesta</returns>
        /// <remarks>
        /// 10/03/2011, CAmezcua: Creación.
        /// </remarks>
        public static string RecortarCadena(this string cadena, int caracteres)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                if (cadena.Length > caracteres)
                    cadena = cadena.Substring(0, caracteres - 1) + "...";
                return cadena;
            }
            return string.Empty;
        }

        public static string RemoverAcentos(this string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }


        public static string Limpiar(this string text)
        {
            text = text.Trim();
            text = Regex.Replace(text, @"\r\n?|\n", string.Empty);
            text = text.Replace(System.Environment.NewLine, string.Empty);
            return text;
        }

        public static string Alfanumerico(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                return rgx.Replace(str, "").TrimStart('0').Trim().ToUpper();
            }
            return string.Empty;
        }
        #endregion
    }
}
