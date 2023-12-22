namespace Teste_Lar.Models.Interface
{
    public interface ITelefoneRepository
    {
        Task<IEnumerable<Telefone>> GetAllAsync();
        Task<Telefone> GetByIdAsync(int id);
        Task CreateAsync(Telefone telefone);
        Task UpdateAsync(Telefone telefone);
        Task DeleteAsync(int id);
    }
}
