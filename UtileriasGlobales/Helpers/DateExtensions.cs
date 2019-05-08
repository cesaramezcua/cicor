using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace UtileriasGlobales.Helpers
{
    public static class DateExtensions
    {
        #region Constantes
        /// <summary>
        /// Constantes que definen los formatos de Fecha.
        /// </summary>
        private const string FORMATO_FECHA_ESTANDAR = "{0:dd/MM/yyyy hh:mm:ss tt}";
        private const string FORMATO_FECHA_LARGO = "{0:dddd, dd MMMM yyyy}";
        private const string FORMATO_FECHA_SQL = "{0:yyyyMMdd HH:mm:ss}";
        private const string FORMATO_FECHA_SQL_RANGO_INICIAL = "{0:yyyyMMdd 00:00:00}";
        private const string FORMATO_FECHA_SQL_RANGO_FINAL = "{0:yyyyMMdd 23:59:59}";
        private const string FORMATO_FECHA_HORA_LARGO = "{0:dddd, dd MMMM yyyy hh:mm:ss tt}";
        #endregion 

        /// <summary>
        /// Obtienen la fecha en el formato especificado.
        /// </summary>
        /// <param name="fecha">Fecha hoy</param>
        /// <param name="tipoFormato">Tipo de formato:
        /// 1- Estandar {dd/MM/yyyy hh:mm:ss tt}
        /// 2- Completo {0:dddd, dd MMMM yyyy h:mm}
        /// 3- SQL {0:yyyyMMdd HH:mm:ss}
        /// 4- RANGO INICIAL {0:yyyyMMdd 00:00:00}
        /// 5- RANGO FINAL {0:yyyyMMdd 23:59:59}
        /// </param>
        /// <returns>Fecha en el formato indicado</returns>
        /// <remarks>
        /// 12/03/2011, CAmezcua: Creación.
        /// </remarks>
        public static string CustomFormat(this DateTime fecha, int tipoFormato)
        {
            string fechaFormato = String.Empty;

            switch (tipoFormato)
            {
                case 1:
                    fechaFormato = String.Format(FORMATO_FECHA_ESTANDAR, fecha);
                    break;
                case 2:
                    fechaFormato = String.Format(FORMATO_FECHA_LARGO, fecha);
                    break;
                case 3:
                    fechaFormato = String.Format(FORMATO_FECHA_SQL, fecha);
                    break;
                case 4:
                    fechaFormato = String.Format(FORMATO_FECHA_SQL_RANGO_INICIAL, fecha);
                    break;
                case 5:
                    fechaFormato = String.Format(FORMATO_FECHA_SQL_RANGO_FINAL, fecha);
                    break;
                case 6:
                    fechaFormato = String.Format(FORMATO_FECHA_HORA_LARGO, fecha);
                    break;
                default:
                    break;
            }

            return fechaFormato;
        }

        /// <summary>
        /// Obtiene el nombre en español del mes
        /// de la fecha de actual.
        /// </summary>
        /// <param name="fecha">Fecha</param>
        /// <returns>Mes en español</returns>
        /// <remarks>
        /// 12/03/2011, CAmezcua: Creación.
        /// </remarks>
        public static string SpanishMonth(this DateTime fecha)
        {
            string month = String.Empty;

            CultureInfo culture = new CultureInfo("es-ES");
            DateTimeFormatInfo d = culture.DateTimeFormat;
            month = d.MonthNames[fecha.Month-1];
            return month;
        }

        /// <summary>
        /// Gets the 12:00:00 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteStart(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteEnd(this DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
        }
    }
}
