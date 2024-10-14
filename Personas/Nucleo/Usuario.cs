using System.ComponentModel.DataAnnotations;

namespace Personas.Nucleo
{
    public class Usuario
    {
        public int id { get; set; }
        public string? Cedula { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public DateTime fecha { get; set; }
        public double Pago { get; set; }

    }
}
