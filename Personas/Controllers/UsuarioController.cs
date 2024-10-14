using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personas.Nucleo;

namespace Personas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string _connectionString = "server=DESKTOP-S2MM97P\\DEV;database=db_Usuario;Integrated Security=True;TrustServerCertificate=true;";

        // Método para usar esa conexión
        private Conexion ObtenerConexion()
        {
            var conexion = new Conexion();
            conexion.StringConnection = _connectionString;
            conexion.Database.Migrate();
            return conexion;
        }

        [HttpPost(Name = "PostUsuario")]
        public IActionResult PostUsuario(string cedula, string nombre, string apellido, DateTime fecha, double pago)
        {
            var conexion = ObtenerConexion();

            var nuevoUsuario = new Usuario
            {
                Cedula = cedula,
                nombre = nombre,
                apellido = apellido,
                fecha = DateTime.Now,
                Pago = pago
            };

            conexion.Guardar(nuevoUsuario);
            conexion.GuardarCambios();

            return Ok(new { message = "Usuario guardado correctamente" });
        }

        [HttpPost("Estadisticas", Name = "PostEstadisticas")]
        public IActionResult PostEstadisticas()
        {
            var conexion = ObtenerConexion();
            // Obtener el total de personas atendidas (usuarios registrados)
            var totalPersonas = conexion.Listar<Usuario>()?.Count() ?? 0;
            // Obtener el pago promedio
            var pagoPromedio = conexion.Listar<Usuario>()?.Average(u => u.Pago) ?? 0.0;

            var estadisticas = new
            {
                TotalPersonasAtendidas = totalPersonas,
                PagoPromedio = pagoPromedio
            };

            return Ok(estadisticas);
        }
    }
}

/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personas.Nucleo;

namespace Personas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet(Name = "GetUsuario")]
        public IEnumerable<Usuario> Get()
        {
            var conexion = new Conexion();
            conexion.StringConnection = " server=DESKTOP-S2MM97P\\DEV;database=db_Personas;Integrated Security=True;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            conexion.Guardar(new Usuario()
            {
                Cedula = "3123131231",
                nombre= "Pepito",
                apellido="Perez",
                fecha = DateTime.Now,
                Pago = 40032.6,


            });
            conexion.GuardarCambios();

            return conexion.Listar<Usuario>();
        
        }
        [HttpGet("Estadisticas", Name = "GetEstadisticas")]
        public IActionResult GetEstadisticas()
        {
            var conexion = new Conexion();
            conexion.StringConnection = " server=DESKTOP-S2MM97P\\DEV;database=db_Personas;Integrated Security=True;TrustServerCertificate=true;";

            // Obtener el total de personas atendidas (usuarios registrados)
            var totalPersonas = conexion.Usuarios?.Count() ?? 0;
            // Obtener el pago promedio
            var pagoPromedio = conexion.Usuarios?.Average(u => u.Pago) ?? 0.0;

            var estadisticas = new
            {
                TotalPersonasAtendidas = totalPersonas,
                PagoPromedio = pagoPromedio
            };

            return Ok(estadisticas);
        }
    }
}
*/
