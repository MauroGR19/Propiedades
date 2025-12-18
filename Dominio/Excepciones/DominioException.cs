using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    public abstract class DominioException : Exception
    {
        protected DominioException(string message) : base(message) { }
        protected DominioException(string message, Exception InnerException) : base(message, InnerException) { }
        
    }
}
   