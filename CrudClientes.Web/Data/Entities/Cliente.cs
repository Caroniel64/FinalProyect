using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrudClientes.Web.Data.Dtos;

namespace CrudClientes.Web.Data.Entities;

[Table("Clientes")]
public class Cliente
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public bool Activo { get; set; } = true;

    internal static Cliente Crear(ClienteDto cliente)
    => new()
    {
        Nombre = cliente.Nombre,
        Email = cliente.Email,
        Direccion = cliente.Direccion,
        Telefono = cliente.Telefono,
        FechaCreacion = DateTime.Now,
        Activo = cliente.Activo
    };
    internal bool Actualizar(ClienteDto cliente)
    {
        var changed = false;
        if (Nombre != cliente.Nombre)
        {
            Nombre = cliente.Nombre;
            changed = true;
        }
        if (Telefono != cliente.Telefono)
        {
            Telefono = cliente.Telefono;
            changed = true;
        }
        if (Direccion != cliente.Direccion)
        {
            Direccion = cliente.Direccion;
            changed = true;
        }
        if (Email != cliente.Email)
        {
            Email = cliente.Email;
            changed = true;
        }
        if (Activo != cliente.Activo)
        {
            Activo = cliente.Activo;
            changed = true;
        }
        return changed;
    }
    internal ClienteDto ToDto()
    => new()
    {
        Id = Id,
        Nombre = Nombre,
        Email = Email,
        Direccion = Direccion,
        Telefono = Telefono,
        FechaCreacion = DateTime.Now,
        Activo = Activo
    };
}
