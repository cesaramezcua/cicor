using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using UtileriasGlobales.Constants;
using System.Data;

namespace UtileriasGlobales.Helpers
{
    /// <summary>
    /// Clase necesario para la validación de parametros.
    /// </summary>
    public class ParameterValidator
    {
        #region Metodos
        /// <summary>
        /// Obtiene los parametros del QUERY_STRING y
        ///  los agrega a un listado tipo SqlParameter.
        /// </summary>
        /// <param name="parametros">Listado de parametros tipo Sql.</param>
        /// <param name="queryString">Cadena con QUERY_STRING.</param>
        /// <returns>Listado de Parametros tipo Sql</returns>
        public static List<SqlParameter> ObtenerParametrosDeQuery(string queryString, List<SqlParameter> parametros = null)
        {
            if (parametros == null)
                parametros = new List<SqlParameter>();

            int contadorParametro = Common.VALOR_CERO;

            if (!String.IsNullOrEmpty(queryString))
            {
                string[] args = queryString.Split(Common.SEPARADOR_QUERYS_STRING.ToCharArray());

                for (contadorParametro = Common.VALOR_CERO; contadorParametro <= args.Length - Common.VALOR_UNO; contadorParametro++)
                {
                    if (!string.IsNullOrEmpty(args[contadorParametro]))
                    {
                        string[] elems = args[contadorParametro].Split(Common.SEPARADOR_IGUAL.ToCharArray());

                        SqlParameter parametro = new SqlParameter(elems[Common.VALOR_CERO], DbType.String);
                        parametro.Value = elems[Common.VALOR_UNO].Replace(Common.CARACTER_GUION_MEDIO_SIN_ESPACIOS, Common.CARACTER_ESPACIO);

                        if (!parametros.Exists(l => l.ParameterName == parametro.ParameterName))
                        {
                            parametros.Add(parametro);
                        }
                        else
                        {
                            //Si viene un parámetro repetido, seconcatena su value al valor que ya existía (Versiones)
                            SqlParameter param = parametros.Find(l => l.ParameterName == parametro.ParameterName);
                            parametros.Remove(param);
                            param.Value = param.Value + Common.CARACTER_COMA + parametro.Value;
                            parametros.Add(param);
                        }
                    }
                }
            }

            return parametros;
        }
        #endregion

    }
}
