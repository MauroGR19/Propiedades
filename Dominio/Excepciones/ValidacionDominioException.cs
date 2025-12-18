using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    public class ValidacionDominioException : DominioException
    {
        public ValidacionDominioException (string message) : base(message) { }
        public ValidacionDominioException (string campo, string valor, string razon)
            : base($"Validación fallida en campo '{campo}' con valor '{valor}': {razon}") { }
    }
}
