using Dominio.Comun;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para la gestión de imágenes de propiedades
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad ImagenPropiedad</typeparam>
    /// <typeparam name="TEntidadID">Tipo del identificador de la imagen (int)</typeparam>
    /// <remarks>
    /// Proporciona operaciones básicas para gestionar imágenes asociadas a propiedades,
    /// incluyendo inserción, actualización, eliminación y consulta individual
    /// </remarks>
    public interface IUseCaseImagenPropiedad<TEntidad, TEntidadID>
    : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>
    {
        /// <summary>
        /// Obtiene una imagen específica por su identificador
        /// </summary>
        /// <param name="entidadID">Identificador único de la imagen</param>
        /// <returns>Entidad de imagen si existe, null en caso contrario</returns>
        Task<TEntidad> ObtenerPorIDAsync(TEntidadID entidadID);
    }
}
