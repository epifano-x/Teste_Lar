using System;
using System.ComponentModel.DataAnnotations;

namespace Teste_Lar.ViewModel
{
    public class PessoaViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public bool EstaAtivo { get; set; }
        public PessoaViewModel(Teste_Lar.Models.Pessoa pessoa)
        {
            Nome = pessoa.Nome;
            CPF = pessoa.CPF;
            DataNascimento = pessoa.DataNascimento;
            EstaAtivo = pessoa.EstaAtivo;
        }
    }
}


