using Microsoft.EntityFrameworkCore;
using Teste_Lar.Models;
using Teste_Lar.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teste_Lar.Context
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly ConnectionContext _context;

        public TelefoneRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefone>> GetAllAsync()
        {
            return await _context.Telefone.ToListAsync();
        }

        public async Task<Telefone> GetByIdAsync(int id)
        {
            return await _context.Telefone.FindAsync(id);
        }

        public async Task CreateAsync(Telefone telefone)
        {
            _context.Telefone.Add(telefone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Telefone telefone)
        {
            _context.Entry(telefone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var telefone = await _context.Telefone.FindAsync(id);
            if (telefone != null)
            {
                _context.Telefone.Remove(telefone);
                await _context.SaveChangesAsync();
            }
        }
    }
}
