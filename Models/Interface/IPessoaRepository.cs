namespace Teste_Lar.Models.Interface
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa> GetByIdAsync(int id);
        Task CreateAsync(Pessoa pessoa);
        Task UpdateAsync(Pessoa pessoa);
        Task DeleteAsync(int id);
    }
}
