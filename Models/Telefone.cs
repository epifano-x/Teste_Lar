using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Teste_Lar.Models
{
    [Table("Telefone", Schema = "public")]
    public class Telefone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public required string Numero { get; set; }

        [Required]
        [StringLength(20)]
        public required string Tipo { get; set; } // Celular, Residencial, Comercial

        [Required]
        public bool IsWhatsApp { get; set; }

        // Chave estrangeira para Pessoa
        public int PessoaId { get; set; }

        // Navegação para Pessoa
        [JsonIgnore]
        public Pessoa? Pessoa { get; set; }
    }
}
