using CrudClientes.Web.Components.Account.Pages.Manage;
using System;
using CrudClientes.Web.Data.Context;
using CrudClientes.Web.Data.Dtos;
using CrudClientes.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrudClientes.Web.Data.Services
{
    public interface IClienteService
    {
        List<ClienteDto> Consultar(string filtro, bool solo_activos = true);
        (bool success, string message) Actualizar(ClienteDto cliente);
        bool Crear(ClienteDto cliente);
        (bool success, string message) Eliminar(int id);
        List<ClienteDto> GetClientes();
    }

    public class ClienteService(ApplicationDbContext db) : IClienteService
    {
        public List<ClienteDto> GetClientes()
        {
            return db.Clientes.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono,
                Direccion = c.Direccion,
                FechaCreacion = c.FechaCreacion,
                Activo = c.Activo
            }).ToList();
        }
        
        public bool Crear(ClienteDto cliente)
        {
            var newCliente = new Cliente
            {
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                FechaCreacion = DateTime.Now,
                Activo = true
            };
            db.Clientes.Add(newCliente);
            return db.SaveChanges() > 0;
        }
        
        public (bool success, string message) Actualizar(ClienteDto cliente)
        {
            var clienteExistente = db.Clientes
            .Where(c => c.Id == cliente.Id)
            .FirstOrDefault();
            if (clienteExistente == null)
                return (false, "El cliente no existe");

            bool cambio = clienteExistente.Actualizar(cliente);
            if (!cambio)
                return (false, "No se realizó ningún cambio.");

            var registrado = db.SaveChanges() > 0;
            if (!registrado)
                return (registrado, "No se pudo registrar el cambio.");

            return (true, "Se modificó exitosamente");
            
        }
        public (bool success, string message) Eliminar(int id)
        {
            var cliente = db.Clientes
            .Where(c => c.Id == id)
            .FirstOrDefault();
            if (cliente == null)
                return (false, "El cliente no existe");

            db.Clientes.Remove(cliente);

            var registrado = db.SaveChanges() > 0;
            if (!registrado)
                return (registrado, "No se pudo eliminar el cliente.");

            return (true, "Se eliminó exitosamente");
            


            
        }
        public List<ClienteDto> Consultar(string filtro, bool solo_activos = true)
        {
            var clientesQuery = db.Clientes
                .AsNoTracking()
                .Where(c =>
                c.Nombre.Contains(filtro) ||
                (c.Telefono != null && c.Telefono.Contains(filtro)) ||
                (c.Direccion != null && c.Direccion.Contains(filtro)) ||
                c.Email.Contains(filtro)
                );
            if (solo_activos)
            {
                clientesQuery = clientesQuery.Where(c => c.Activo == true);
            }
            var clientes = clientesQuery
                .Select(c => c.ToDto())
                .ToList();
            return clientes;
        }

    }
  
}
