using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para la gestión del historial de transacciones de propiedades
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad HistorialPropiedad</typeparam>
    /// <typeparam name="TEntidadID">Tipo del identificador del historial (int)</typeparam>
    /// <remarks>
    /// Hereda todas las operaciones CRUD básicas de IUseCaseBase para gestionar
    /// el registro de ventas, compras y otras transacciones de propiedades
    /// </remarks>
    public interface IUseCaseHistorialPropiedad<TEntidad, TEntidadID> : IUseCaseBase<TEntidad, TEntidadID>
    {

    }
}
