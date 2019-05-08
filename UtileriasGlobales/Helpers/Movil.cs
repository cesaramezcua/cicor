using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtileriasGlobales.Helpers
{
    /// <summary>
    /// Clase que nos permite optimizar ciertos elementos para dispositivos moviles.
    /// </summary>
    public class Movil
    {
        #region Constantes

        /// <summary>
        /// Constantes que Definen los agentes de los navegadores.
        /// </summary>
        const string AGENTE_WINDOWS = "WINDOWS";
        const string AGENTE_MOZILLA = "MOZILLA";
        const string AGENTE_CHROME = "CHROME";
        const string AGENTE_SAFARI = "SAFARI";
        const string AGENTE_OPERA = "OPERA";
        const string AGENTE_MOBILE = "MOBILE";
        //Fin Agentes

        #endregion

        /// <summary>
        /// Valida en base al agente del navegador si es un dispositivo móvil.
        /// </summary>
        /// <returns>Si es un dispositivo movil</returns>
        /// <remarks>
        /// 02/10/2012, CAmezcua: Creación.
        /// </remarks>
        public static bool EsDispositivoMovil(string agente)
        {
            bool esMovil = false;

            if (!String.IsNullOrEmpty(agente))
            {
                if ((agente.IndexOf(AGENTE_WINDOWS) > 0 || agente.IndexOf(AGENTE_MOZILLA) > 0 || agente.IndexOf(AGENTE_CHROME) > 0 || agente.IndexOf(AGENTE_SAFARI) > 0 || agente.IndexOf(AGENTE_OPERA) > 0) && (agente.IndexOf(AGENTE_MOBILE) <= 0))
                {
                    esMovil = false;
                }
                else
                {
                    esMovil = true;
                }
            }
            return esMovil;
        }

    }
}
