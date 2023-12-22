// Namespace Teste_Lar.Context
using Teste_Lar.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste_Lar.Models;

namespace Teste_Lar.Context
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context;

        public UsuarioRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }


    }
}
