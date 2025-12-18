using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    public class EntidadNoEncontradaException : DominioException
    {
        public EntidadNoEncontradaException (string entidad, object id) 
            : base($"No se encontró la entidad {entidad} con id {id}") { }
    }
}
