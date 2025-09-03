using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;

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
        public Autenticacion ObtenerAutenticacion(string Usuario, string Contrasena)
        {
            var Selecc = db.autenticacionEntidad.Where(x => x.usuario == Usuario && x.contrasena == Contrasena).FirstOrDefault();

            if (Selecc == null)
                return new Autenticacion();
            else
                return _mapper.Map<Autenticacion>(Selecc);
        }

        public Autenticacion Insertar(Autenticacion entidad)
        {
            db.autenticacionEntidad.Add(_mapper.Map<AutenticacionEntidad>(entidad));
            return entidad;
        }

        public void SalvarTodo()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
