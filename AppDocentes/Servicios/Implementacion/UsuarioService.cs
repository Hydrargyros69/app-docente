using AppDocentes.Data;
using AppDocentes.Models;
using AppDocentes.Servicios.Contrato;
using Microsoft.EntityFrameworkCore;

namespace AppDocentes.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DocentesDbContext _dbContext;

        public UsuarioService(DocentesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string nombre, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(clave))
                return null!;

            // Buscar por Nombre de usuario y contraseña (ya encriptada)
            var usuario_encontrado = await _dbContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.NomUsuario == nombre && u.PassUsuario == clave);

            return usuario_encontrado!;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            if (modelo == null)
                return null!;

            // Evitar duplicados por nombre de usuario o correo
            var existe = await _dbContext.Usuarios
                .AnyAsync(u => u.NomUsuario == modelo.NomUsuario || u.CorrUsuario == modelo.CorrUsuario);

            if (existe)
                return null!;

            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();

            return modelo;
        }
    }
}
