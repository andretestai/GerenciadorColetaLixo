using APIColetaDeLixo.Interfaces.Services;
using APIColetaDeLixo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIColetaDeLixo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("AddUsuario")]
        public async Task<UsuarioModel> AddUsuario([FromBody] UsuarioModel model)
        {
            return await _service.AddUsuario(model);
        }

        [HttpGet("GetAllUsuarios")]
        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            return await _service.GetAllUsuarios();
        }

        [HttpGet("GetUsuario")]
        public async Task<UsuarioModel> GetUsuario (int id)
        {
            return await _service.GetUsuario(id);
        }

        [HttpPut("UpdateUsuario")]
        public async Task<UsuarioModel> UpdateUsuario([FromBody] UsuarioModel model)
        {
            return await _service.UpdateUsuario(model);
        }

        [HttpDelete("DeleteUsuario")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            bool deleted = await _service.DeleteUsuario(id);
            if (deleted)
            {
                return Ok("Usuário excluído com sucesso.");
            }
            else
            {
                return NotFound("Usuário não encontrado.");
            }
        }
    }
}
