using APIColetaDeLixo.Interfaces.Services;
using APIColetaDeLixo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIColetaDeLixo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColetaController : ControllerBase
    {
        private readonly IColetaService _service; 

        public ColetaController(IColetaService service) 
        {
            _service = service;
        }

        [HttpPost("AddColeta")]
        public async Task<ColetaModel> AddColeta([FromBody] ColetaModel model)
        {
            return await _service.AddColeta(model);
        }

        [HttpGet("GetAllColetas")]
        public async Task<List<ColetaModel>> GetAllColetas()
        {
            return await _service.GetAllColetas();
        }

        [HttpGet("GetColeta")]
        public async Task<ColetaModel> GetColeta(int id)
        {
            return await _service.GetColeta(id);
        }

        [HttpPut("UpdateColeta")]
        public async Task<ColetaModel> UpdateColeta([FromBody] ColetaModel model)
        {
            return await _service.UpdateColeta(model);
        }

        [HttpDelete("DeleteColeta")]
        public async Task<IActionResult> DeleteColeta(int id)
        {
            bool deleted = await _service.DeleteColeta(id);
            if (deleted)
            {
                return Ok("Coleta excluída com sucesso.");
            }
            else
            {
                return NotFound("Coleta não encontrada."); 
            }
        }

        [HttpGet("GetColetaByUsuario")]
        public async Task<List<ColetaModel>> GetColetaByUsuario(int id)
        {
            return await _service.GetColetaByUsuario(id);
        }
    }

}
