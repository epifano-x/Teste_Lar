using System.ComponentModel.DataAnnotations.Schema;

namespace Teste_Lar.Models
{
    [Table("Usuario", Schema = "public")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }

        private string _password;
        public string Password
        {
            get => _password;
            set => _password = value; // Removido o hashing daqui
        }

        // Método para verificar a senha
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _password);
        }
    }
}
