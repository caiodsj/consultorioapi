using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet("{id}/consultas")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetAllConsultasById(int id)
        {
            var consultas = _pacienteService.GetAllConsultasByPacienteAsync(id);
            if (consultas is null) return NotFound($"Paciente com ID {id} não encontrado.");
            return Ok();
        }

        [HttpGet("/pacientes")]
        public async Task<IActionResult> ListarPacientesPorIdade([FromQuery] int idade_maior_que)
        {
            var pacientes = await _pacienteService.GetAllPacienteMaiorQueAsync(idade_maior_que);

            if (pacientes == null || pacientes.Count == 0)
            {
                return NotFound("Nenhum paciente encontrado com a idade especificada.");
            }

            return Ok(pacientes);
        }

        [HttpPatch("/pacientes/{id}")]
        public async Task<IActionResult> AtualizarEndereco(int id, [FromBody] PacienteDTO atualizacaoEndereco)
        {
            var mensagem = await _pacienteService.UpdateEnderecoAsync(id, atualizacaoEndereco.Endereco);

            if (mensagem == null)
            {
                return NotFound($"Paciente com ID {id} não encontrado.");
            }

            return Ok(mensagem);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaciente(int id, PacienteDTO request)
        {
            var paciente = await _pacienteService.UpdatePacienteAsync(id, request);
            if (paciente is null) return NotFound($"Paciente com ID {id} não encontrado.");
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> CreatePaciente(CreatePacienteDTO paciente)
        {
            await _pacienteService.CreatePacienteAsync(paciente);
            return Ok();
        }
    }
}
