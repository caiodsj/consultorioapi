using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicos()
        {
            List<MedicoDTO> list = await _medicoService.GetAllMedicos();
            if (!list.Any()) return NotFound("Não há nenhum médico");

            return Ok(list);

        }

        [HttpGet("medicos/disponiveis")]
        public async Task<ActionResult<List<MedicoDTO>>> GetMedicosDisponiveis([FromQuery] string date, [FromQuery] string especialidade)
        {
            List<MedicoDTO> medicos = await _medicoService.GetMedicoDisponivel(date, especialidade);

            if (!medicos.Any()) return Ok("Não há médicos disponiveis");

            return Ok(medicos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Consulta>>> GetConsultaByMedico(int id)
        {
            List<Consulta> list = await _medicoService.GetConsultasByMedico(id);
            if (!list.Any()) return NotFound("Não há nenhuma consulta para esse médico");

            return Ok(list);
        }

        [HttpGet("medicos/")]
        public async Task<ActionResult<List<MedicoDTO>>> GetMedicoByEspecialidade([FromQuery] string especialidade)
        {
            List<MedicoDTO> medicos = await _medicoService.GetMedicoByEspecialidade(especialidade);
            if (!medicos.Any()) return NotFound("Nenhum médico com essa especialidade encontrado");

            return Ok(medicos);
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CreateMedico(CreateMedicoDTO medicoDTO)
        {
            var resposta = await _medicoService.CreateMedico(medicoDTO);
            if (resposta is null) return BadRequest("Erro ao criar o médico");

            return Ok("Médico criado com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedico(int id, MedicoDTO dto)
        {
            if (id != dto.Id) return BadRequest("Ids devem ser iguais");
            var resposta = await _medicoService.UpdateMedico(dto);

            if (resposta == null) return BadRequest("Houve algum erro para atualizar o médico.");

            return Ok("Médico alterado com sucesso");

        }

        [HttpPatch("medicos/{id}")]
        public async Task<IActionResult> UpdateEspecialidadeMedico(int id, string especialidade)
        {
            var respota = await _medicoService.UpdateEspecialidade(id, especialidade);
            if (respota is null) return BadRequest("Erro ao atualizar a especialidade");

            return Ok("Especialidade atualizada com sucesso");
        }
    }
}
