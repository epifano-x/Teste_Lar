using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Teste_Lar.Models;
using Teste_Lar.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste_Lar.Services; // Certifique-se de que este namespace esteja correto

namespace Teste_Lar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AuthenticationManager _authenticationManager;

        public UsuarioController(IUsuarioRepository usuarioRepository, AuthenticationManager authenticationManager)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationManager = authenticationManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Usuario usuario)
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            await _usuarioRepository.CreateAsync(usuario);
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            // Atualizar senha, se fornecida
            if (!string.IsNullOrEmpty(usuario.Password))
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Usuario loginDetails)
        {
            
            var usuario = await _usuarioRepository.GetByUsernameAsync(loginDetails.Username);
            Console.WriteLine(loginDetails.Password);
            Console.WriteLine(usuario.Password);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDetails.Password, usuario.Password))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _authenticationManager.GenerateToken(usuario);
            return Ok(new { token });
        }
    }
}
