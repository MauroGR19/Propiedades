using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mestras
{
    public class Encriptar
    {
        public static string Encriptarr(string input)
        {
            string result = string.Empty;
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            result = Convert.ToBase64String(bytes);
            return result;
        }

        public static string Desencriptar(string input)
        {
            string result = string.Empty;
            byte[] decrypter = Convert.FromBase64String(input);
            result = Encoding.Unicode.GetString(decrypter);
            return result;
        }
    }
}
