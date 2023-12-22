using Microsoft.EntityFrameworkCore;
using Teste_Lar.Models;
using Teste_Lar.Models.Interface;

namespace Teste_Lar.Context
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ConnectionContext _context; 

        public PessoaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            // Inclui os telefones na consulta
            return await _context.Pessoa.Include(p => p.Telefones).ToListAsync();
        }

        public async Task<Pessoa> GetByIdAsync(int id)
        {
            return await _context.Pessoa.Include(p => p.Telefones).FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task CreateAsync(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pessoa pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }
    }
}