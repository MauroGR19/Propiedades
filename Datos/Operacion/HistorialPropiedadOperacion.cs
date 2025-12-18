using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Comun;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Operacion
{
    public class HistorialPropiedadOperacion : IRepositorioHistorialPropiedad<HistorialPropiedad, int>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public HistorialPropiedadOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        public async Task<HistorialPropiedad> InsertarAsync(HistorialPropiedad entidad)
        {
            await db.historialPropiedadEntidad.AddAsync(_mapper.Map<HistorialPropiedadEntidad>(entidad));
            return entidad;
        }

        public async Task<bool> ActualizarAsync(HistorialPropiedad entidad)
        {
            var selecc = await ObtenerPorIDAsync(entidad.IdHistorialPropiedad);
            if (selecc != null)
            {
                selecc.FechaVenta = entidad.FechaVenta;
                selecc.Nombre = entidad.Nombre;
                selecc.Valor = entidad.Valor;
                selecc.Impuesto = entidad.Impuesto;

                db.Entry(_mapper.Map<HistorialPropiedadEntidad>(selecc)).State = EntityState.Modified;
                return true;
            }
            else
                return false;
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            var selecc = await ObtenerPorIDAsync(entidadID);
            if (selecc != null)
            {
                db.historialPropiedadEntidad.Remove(_mapper.Map<HistorialPropiedadEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public async Task<List<HistorialPropiedad>> ObtenerTodoAsync()
        {
            var entidades = await db.historialPropiedadEntidad.ToListAsync();
            return _mapper.Map<List<HistorialPropiedad>>(entidades);
        }

        public async Task<HistorialPropiedad> ObtenerPorIDAsync(int entidadID)
        {
            var selecc = await db.historialPropiedadEntidad.Where(x => (x.IdHistorialPropiedad == entidadID)).FirstOrDefaultAsync();

            return _mapper.Map<HistorialPropiedad>(selecc);
        }

        public async Task SalvarTodoAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task<ResultadoPaginado<HistorialPropiedad>> ObtenerPaginadoAsync(PaginacionParametros parametros)
        {
            var totalRegistros = await db.historialPropiedadEntidad.CountAsync();

            var entidades = await db.historialPropiedadEntidad
                .Skip((parametros.Pagina - 1) * parametros.TamanioPagina)
                .Take(parametros.TamanioPagina)
                .ToListAsync();

            var datos = _mapper.Map<List<HistorialPropiedad>>(entidades);

            return new ResultadoPaginado<HistorialPropiedad>
            {
                Datos = datos,
                TotalRegistros = totalRegistros,
                PaginaActual = parametros.Pagina,
                TamanioPagina = parametros.TamanioPagina,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)parametros.TamanioPagina),
                TienePaginaAnterior = parametros.Pagina > 1,
                TienePaginaSiguiente = parametros.Pagina < (int)Math.Ceiling(totalRegistros / (double)parametros.TamanioPagina)
            };
        }
        #endregion
    }
}
