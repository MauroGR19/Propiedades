using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    public class OperacionNoPermitidaException : DominioException
    {
        public OperacionNoPermitidaException(string operacion, string razon)
            : base($"Operacion '{operacion}' no permitida: '{razon}'") { }
    }
}
