using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultasController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }
        [HttpGet("/consultas")]
        public async Task<IActionResult> ListarConsultasPorData([FromQuery] DateTime data)
        {
            var consultas = await _consultaService.GetConsultasPorData(data);

            if (consultas == null || consultas.Count == 0)
            {
                return NotFound("Nenhuma consulta agendada para a data especificada.");
            }

            return Ok(consultas);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteConsulta(int id)
        {
            var consulta = await _consultaService.DeleteConsultaAsync(id);
            if (consulta is null) return NotFound("Consulta não encontrada");
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Consulta>> CreateConsulta(ConsultaDTO request)
        {
            await _consultaService.CreateConsultaAsync(request);
            return Ok();
        }
    }
}
