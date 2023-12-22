using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teste_Lar.Models
{
    [Table("Pessoa", Schema = "public")]
    public class Pessoa
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("Nome")]
        public required string Nome { get; set; }

        [Required]
        [StringLength(255)]
        [Column("CPF")]
        public required string CPF { get; set; }

        [Required]
        [Column("DataNascimento", TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Column("EstaAtivo")]
        public bool EstaAtivo { get; set; }

        // Coleção para Telefones
        public virtual required ICollection<Telefone> Telefones { get; set; }

    }
}
