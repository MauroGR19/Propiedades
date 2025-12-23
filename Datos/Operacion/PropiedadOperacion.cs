using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Comun;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Operacion
{
    public class PropiedadOperacion : IRepositorioPropiedad<Propiedad, string>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PropiedadOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        public async Task<List<Propiedad>> ObtenerTodoAsync()
        {
            var entidades = await db.propiedadEntidad.ToListAsync();
            return _mapper.Map<List<Propiedad>>(entidades);
        }

        public async Task<List<Propiedad>> ObtenerPorFiltroAsync(Propiedad entidad, string order)
        {
            var query = db.propiedadEntidad.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.Direccion))
                query = query.Where(x => x.Direccion.Contains(entidad.Direccion));

            if (entidad.Precio > 0)
                query = query.Where(x => x.Precio == entidad.Precio);

            if (entidad.Anio > 0)
                query = query.Where(x => x.Anio == entidad.Anio);

            var entidades = await query.ToListAsync();
            return _mapper.Map<List<Propiedad>>(entidades);
        }


        public async Task<Propiedad> ObtenerPorIDAsync(string entidadID)
        {
            var selecc = await db.propiedadEntidad.Where(x => x.MatriculaInmobiliaria == entidadID).FirstOrDefaultAsync();
            return _mapper.Map<Propiedad>(selecc);
        }

        public async Task<Propiedad> InsertarAsync(Propiedad entidad)
        {
            await db.propiedadEntidad.AddAsync(_mapper.Map<PropiedadEntidad>(entidad));
            return entidad;
        }

        public async Task<bool> ActualizarAsync(Propiedad entidad)
        {
            var selecc = await ObtenerPorIDAsync(entidad.MatriculaInmobiliaria);
            if (selecc != null)
            {
                selecc.Nombre = entidad.Nombre;
                selecc.Direccion = entidad.Direccion;
                selecc.Precio = entidad.Precio;
                selecc.Anio = entidad.Anio;
                selecc.NumeroDocumentoPropietario = entidad.NumeroDocumentoPropietario;

                db.Entry(_mapper.Map<PropiedadEntidad>(selecc)).State = EntityState.Modified;
                return true;
            }
            else
                return false;
        }

        public async Task<bool> EliminarAsync(string entidadID)
        {
            var selecc = await ObtenerPorIDAsync(entidadID);
            if (selecc != null)
            {
                db.propiedadEntidad.Remove(_mapper.Map<PropiedadEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public async Task SalvarTodoAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task<ResultadoPaginado<Propiedad>> ObtenerPaginadoAsync(PaginacionParametros parametros)
        {
            var totalRegistros = await db.propiedadEntidad.CountAsync();

            var entidades = await db.propiedadEntidad
                .Skip((parametros.Pagina - 1) * parametros.TamanioPagina)
                .Take(parametros.TamanioPagina)
                .ToListAsync();

            var datos = _mapper.Map<List<Propiedad>>(entidades);

            return new ResultadoPaginado<Propiedad>
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
