using AppDocentes.Models;
using Microsoft.EntityFrameworkCore;


namespace AppDocentes.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string nombre, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
