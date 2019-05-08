using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtileriasGlobales.Constants
{
    /// <summary>
    /// Constantes globales que son utilizadas para definir Mensajes en de información, 
    /// instrucción, advertencia o error al usuario, en algunas secciones del codigo, 
    /// definidas aqui para hacer mas fácil su mantenimiento.
    /// </summary>
    /// <remarks>
    /// 02/10/2012, CAmezcua: Creación.
    /// </remarks>
  
    public class Messages
    {

        #region General

        /// <summary>
        /// Error inesperado generico.
        /// </summary>
        public const string ERROR_INESPERADO = "Error Inesperado. Disculpe las molestias que esto le ocasione.";

       
        /// <summary>
        /// Procesando llamada asincrona generico.
        /// </summary>
        public const string MENSAJE_CARGANDO_AJAX = "Procesando...";


        #endregion

        #region Correo

        /// <summary>
        /// Envío de correo con éxito.
        /// </summary>
        public const string CORREO_ENVIADO = "Su mensaje fue enviado con éxito.";

        /// <summary>
        /// Fallo en el envío de correo.
        /// </summary>
        public const string CORREO_NO_ENVIADO = "Su mensaje no pudo ser enviado con éxito.";

        #endregion

        #region Administracion

        /// <summary>
        /// No se encontraron registros en el gridview.
        /// </summary>
        public const string ADMIN_NO_REGISTROS = "No se encontraron registros con los criterios de búsqueda seleccionados.";

        /// <summary>
        /// Estatus activo.
        /// </summary>
        public const string ADMIN_ESTATUS_ACTIVO = "Activo";

        /// <summary>
        /// Estatus inactivo.
        /// </summary>
        public const string ADMIN_ESTATUS_INACTIVO = "Inactivo";


        /// <summary>
        /// Mensaje de tooltip de activar registros.
        /// </summary>
        public const string ADMIN_ACTIVAR = "Activar registros";

        /// <summary>
        /// Mensaje de tooltip de desactivar registros.
        /// </summary>
        public const string ADMIN_DESACTIVAR = "Deshabilitar registros";

        /// <summary>
        /// Cancelación.
        /// </summary>
        public const string ADMIN_CANCELACION = "La operación ha sido cancelada por el usuario.";


        /// <summary>
        /// No se encontraron registros en el gridview.
        /// </summary>
        public const string ADMIN_ALTA_BOTON = "Agregar registro";

        /// <summary>
        /// Alta de Registro.
        /// </summary>
        public const string ADMIN_REGISTRO_ALTA = "Se ha creado exitosamente un nuevo registro.";

        /// <summary>
        /// Edición de Registro.
        /// </summary>
        public const string ADMIN_REGISTRO_EDICION = "Se ha modificado exitosamente el registro seleccionado.";


        /// <summary>
        /// Error en alta / edición registro.
        /// </summary>
        public const string ADMIN_REGISTRO_EXISTENTE = "Error: El registro que desea ingresar, ya existe en la base de datos.";


        /// <summary>
        /// Edición de Registro.
        /// </summary>
        public const string ADMIN_SIN_ARCHIVO = "Sin registro de archivo.";

        /// <summary>
        /// Confirmación Alta de Registro.
        /// </summary>
        public const string ADMIN_CONFIRMACION_ALTA = "¿Está seguro que desea agregar un nuevo registro?";

        /// <summary>
        /// Confirmación Edición de Registro.
        /// </summary>
        public const string ADMIN_CONFIRMACION_EDICION = "¿Está seguro que desea modificar el registro seleccionado?";

        /// <summary>
        /// Confirmación Alta de Registros.
        /// </summary>
        public const string ADMIN_CONFIRMACION_ALTA_LOGICA = "¿Está seguro que desea publicar los registros seleccionados?";

        /// <summary>
        /// Confirmación Baja de Registros.
        /// </summary>
        public const string ADMIN_CONFIRMACION_BAJA_LOGICA = "¿Está seguro que desea deshabilitar los registros seleccionados?";


        /// <summary>
        /// Confirmación Carga de archivo.
        /// </summary>
        public const string ADMIN_CONFIRMACION_CARGA_ARCHIVO = "¿Está seguro que desea guardar en el servidor el archivo seleccionado?";


        /// <summary>
        /// Confirmación carga satisfactoria del archivo.
        /// </summary>
        public const string ADMIN_ARCHIVO_EXITO = "El archivo fue cargado exitosamente en el servidor.";

        /// <summary>
        /// Confirmación carga satisfactoria del archivo.
        /// </summary>
        public const string ADMIN_ARCHIVO_ERROR = "No se pudo subir el archivo. ";

      
        /// <summary>
        /// No selecciono archivo
        /// </summary>
        public const string ADMIN_ARCHIVO_NO_SELECCIONADO = "No seleccionó un archivo.";

        /// <summary>
        /// Mensaje tamaño del archivo
        /// </summary>
        public const string ADMIN_ARCHIVO_LIMITE = "El tamaño máximo permitido es de ";

        /// <summary>
        /// TOTAL ERROR AL LEER EL ARCHIVO
        /// </summary>
        public const string ADMIN_ARCHIVO_FORMATO = "El Formato del archivo no es correcto.";


        #endregion

        #region Validadores
        
        /// <summary>
        /// Mensaje de error asignado al validador de usuario.
        /// </summary>
        public const string VALIDADOR_USUARIO_REGISTRADO = "El usuario ingresado ya existe.";

        /// <summary>
        /// Mensaje de error asignado al validador de vendedor.
        /// </summary>
        public const string VALIDADOR_VENDEDOR_REGISTRADO = "El vendedor ingresado ya existe.";



        #endregion


    }
}
