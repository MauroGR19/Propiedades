namespace Dominio.Modelos
{
    public class Autenticacion
    {
        public string usuario { get; set; }
        public string contrasena { get; set; }

        public Autenticacion()
        {
            usuario = string.Empty;
            contrasena = string.Empty;
        }
    }
}
