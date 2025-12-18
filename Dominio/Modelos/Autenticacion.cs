namespace Dominio.Modelos
{
    public class Autenticacion
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public Autenticacion()
        {
            Usuario = string.Empty;
            Contrasena = string.Empty;
        }
    }
}
