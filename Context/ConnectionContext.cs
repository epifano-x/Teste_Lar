using Microsoft.EntityFrameworkCore;
using Teste_Lar.Models;

namespace Teste_Lar.Context
{
    public class ConnectionContext : DbContext
    {
        // Adicione este construtor
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
