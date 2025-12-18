using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Operacion
{
    public class AutenticacionOperacion : IRepositorioAutenticacion<Autenticacion, string>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AutenticacionOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        
        public async Task<Autenticacion> ObtenerPorUsuarioAsync(string Usuario)
        {
            var Selecc = await db.autenticacionEntidad.Where(x => x.Usuario == Usuario).FirstOrDefaultAsync();

            if (Selecc == null)
                return null;
            else
                return _mapper.Map<Autenticacion>(Selecc);
        }

        public async Task<Autenticacion> InsertarAsync(Autenticacion entidad)
        {
            await db.autenticacionEntidad.AddAsync(_mapper.Map<AutenticacionEntidad>(entidad));
            return entidad;
        }

        public async Task SalvarTodoAsync()
        {
            await db.SaveChangesAsync(); 
        }
        #endregion
    }
}
