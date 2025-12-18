using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para la gestión de propietarios de inmuebles
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad Propietario</typeparam>
    /// <typeparam name="TEntidadID">Tipo del identificador del propietario (int)</typeparam>
    /// <remarks>
    /// Hereda todas las operaciones CRUD básicas de IUseCaseBase
    /// </remarks>
    public interface IUseCasePropietario<TEntidad, TEntidadID> : IUseCaseBase<TEntidad, TEntidadID>
    {
    }
}
