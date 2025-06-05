using System.ComponentModel.DataAnnotations;

namespace CrudClientes.Web.Data.Dtos
{
    public class ClienteDto
    {
        /*
         * ### Modelo de Datos - Cliente
        | Campo | Tipo | Restricciones |
        |-------|------|--------------|
        | Id | int | Primary Key, Identity |
        | Nombre | string | Required, MaxLength(100) |
        | Email | string | Required, Email, MaxLength(100), Unique |
        | Telefono | string | Optional, Phone, MaxLength(15) |
        | Direccion | string | Optional, MaxLength(200) |
        | FechaCreacion | DateTime | Default: DateTime.Now |
        | Activo | bool | Default: true |
         */
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

    }
}
