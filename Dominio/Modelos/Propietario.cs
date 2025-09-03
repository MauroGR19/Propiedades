namespace Dominio.Modelos
{
    public class Propietario
    {
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Foto { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Propietario()
        {

            IdPropietario = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Foto = string.Empty;
            FechaNacimiento = DateTime.Now;
        }
    }
}
