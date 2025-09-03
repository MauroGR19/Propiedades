using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace Dominio.Maestras
{
    public class Excepciones
    {
        #region Metodos
        public Exception Error(Exception ex, string _Mensaje)
        {
            string strMensaje = _Mensaje;

            try
            {
            }
            catch (ArgumentNullException)
            {
                strMensaje = strMensaje + ", Valores nulos";
            }
            catch (DirectoryNotFoundException)
            {
                strMensaje = strMensaje + ", El directorio no es válido";
            }
            catch (FormatException)
            {
                strMensaje = strMensaje + ", El formato no es válido";
            }
            catch (TimeoutException)
            {
                strMensaje = strMensaje + ", El intervalo de tiempo asignado a una operación ha expirado.";
            }
            catch (AuthenticationException)
            {
                strMensaje = strMensaje + ", Es necesario autenticarse";
            }
            catch (ValidationException)
            {
                strMensaje = strMensaje + ", Error de validación";
            }
            catch (InvalidOperationException)
            {
                strMensaje = strMensaje + ", Operación inválida";
            }

            strMensaje = strMensaje + ", Detalle del error: " + ex.Message;

            throw new Exception(strMensaje);
        }
        #endregion
    }
}
