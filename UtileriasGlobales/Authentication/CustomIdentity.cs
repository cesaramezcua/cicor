using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UtileriasGlobales.Helpers;
using UtileriasGlobales.Constants;

namespace UtileriasGlobales.Authentication
{
    /// <summary>
    /// Clase responsable de implementar la interfase IIdentity, 
    /// la cual define las propiedades: Ticket, AuthenticationType, IsAuthenticated, Name.
    /// </summary>
    public class CustomIdentity : System.Security.Principal.IIdentity
    {
        #region Constantes

        /// <summary>
        /// Define el tipo de autenticación de la identidad.
        /// </summary>
        private const string AUTHENTICATION_TYPE = "Custom";

        /// <summary>
        /// Constantes que definen el indice dentro del arreglo obtenido
        /// a partir de _ticket.UserData.
        /// </summary>
        private const int INDICE_NOMBRE = 0;
        private const int INDICE_ID_USUARIO = 1;
        private const int INDICE_CORREO = 2;

        #endregion

        #region Variables
        /// <summary>
        /// Ticket del tipo Forms Authentication
        /// </summary>
        private FormsAuthenticationTicket _ticket;
        #endregion

        #region Propiedades
        /// <summary>
        /// Obtiene el Ticket de tipo FormsAuthenticationTicket
        /// </summary>
        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
        }

        /// <summary>
        /// Obtiene el tipo de autenticación Custom tipo AuthenticationType
        /// </summary>
        public string AuthenticationType
        {
            get { return AUTHENTICATION_TYPE; }
        }

        /// <summary>
        /// Obtiene si el usuario esta autenticado.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>
        /// Obtiene el nombre completo del usuario.
        /// </summary>
        public string Name
        {
            get
            {
                string[] userData = _ticket.UserData.Split('|');
                return userData[INDICE_NOMBRE];
            }
        }

        /// <summary>
        /// Obtiene el IdUsuario a partir del userData del Ticket.
        /// </summary>
        public int IdUsuario
        {
            get
            {
                string[] userData = _ticket.UserData.Split('|');
                int idUsuario = 0;
                int.TryParse(userData[INDICE_ID_USUARIO], out idUsuario);
                return idUsuario;
            }
        }

        /// <summary>
        /// Obtiene el nombre completo del usuario.
        /// </summary>
        public string Correo
        {
            get
            {
                string[] userData = _ticket.UserData.Split('|');
                return userData[INDICE_CORREO];
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor que crea la identidad personalizada a partir del FormsAuthenticationTicket.
        /// </summary>
        /// <param name="ticket">Objeto de tipo FormsAuthenticationTicket</param>
        /// <remarks>
        /// 11/11/2011, MArteche: Creación.
        /// </remarks>
        public CustomIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
        }
        #endregion
        
        #region Metodos

        /// <summary>
        /// Obtiene las páginas a las que el usuario tiene acceso.
        /// </summary>
        /// <returns>Arreglo de páginas</returns>
        public string[] ObtenerPaginasAcceso()
        {
            string[] pages = null;
            string cookieName = FormsAuthentication.FormsCookieName + Configuration.COOKIE_PAGE_LEVEL;
            HttpCookie pgCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (pgCookie != null)
            {
                string[] paginas = Cryptography.Desencriptar(pgCookie.Value).Split('|');
                pages = paginas[0].Split(',');
            }

            //Agrega la extension para ahorrar espacio en la cookie
            //if (pages!= null)
            //    for (int i = 0; i < pages.Length; i++)
            //        pages[i] += Configuration.EXTENSION_ASPX;
	
            return pages;
        }

        /// <summary>
        /// Obtiene las páginas a las que el usuario tiene acceso.
        /// </summary>
        /// <returns>Arreglo de páginas</returns>
        public string[] ObtenerIdPaginasAcceso()
        {
            string[] pages = null;
            string cookieName = FormsAuthentication.FormsCookieName + Configuration.COOKIE_PAGE_LEVEL;
            HttpCookie pgCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (pgCookie != null)
            {
                string[] paginas = Cryptography.Desencriptar(pgCookie.Value).Split('|');
                pages = paginas[1].Split(',');
            }

            return pages;
        }

	    #endregion

    }
}