using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuarios> GetUsuarios(String correo, String clave);
        Task<Usuarios> SaveUsuarios(Usuarios model);
    }
}
