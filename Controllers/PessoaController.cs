using Microsoft.AspNetCore.Mvc;
using Teste_Lar.Models;
using Teste_Lar.Models.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Teste_Lar.Context;

namespace Teste_Lar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase

    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ITelefoneRepository _telefoneRepository;


        public PessoaController(IPessoaRepository pessoaRepository, ITelefoneRepository telefoneRepository)
        {
            _pessoaRepository = pessoaRepository;
            _telefoneRepository = telefoneRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            return Ok(pessoas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Pessoa>> Post([FromBody] Pessoa pessoa)
        {
            await _pessoaRepository.CreateAsync(pessoa);
            return CreatedAtAction(nameof(Get), new { id = pessoa.Id }, pessoa);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            var pessoaExistente = await _pessoaRepository.GetByIdAsync(id);
            if (pessoaExistente == null)
            {
                return NotFound();
            }

            // Atualizar propriedades da pessoa
            pessoaExistente.Nome = pessoa.Nome;
            pessoaExistente.CPF = pessoa.CPF;
            pessoaExistente.DataNascimento = pessoa.DataNascimento;
            pessoaExistente.EstaAtivo = pessoa.EstaAtivo;

            // Atualizar telefones
            foreach (var telefone in pessoa.Telefones)
            {
                if (telefone.Id == 0)
                {
                    // Novo telefone
                    telefone.PessoaId = pessoa.Id;
                    await _telefoneRepository.CreateAsync(telefone);
                }
                else
                {
                    // Atualizar telefone existente
                    var telefoneExistente = await _telefoneRepository.GetByIdAsync(telefone.Id);
                    if (telefoneExistente != null)
                    {
                        telefoneExistente.Numero = telefone.Numero;
                        telefoneExistente.Tipo = telefone.Tipo;
                        telefoneExistente.IsWhatsApp = telefone.IsWhatsApp;
                        await _telefoneRepository.UpdateAsync(telefoneExistente);
                    }
                }
            }

            // Remover telefones não presentes na lista atualizada
            var telefonesExistentes = await _telefoneRepository.GetAllAsync();
            foreach (var telefoneExistente in telefonesExistentes)
            {
                if (telefoneExistente.PessoaId == pessoa.Id && !pessoa.Telefones.Any(t => t.Id == telefoneExistente.Id))
                {
                    await _telefoneRepository.DeleteAsync(telefoneExistente.Id);
                }
            }

            // Salvar alterações da pessoa
            await _pessoaRepository.UpdateAsync(pessoaExistente);

            return NoContent();
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            await _pessoaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
