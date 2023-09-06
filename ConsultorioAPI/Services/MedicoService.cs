using ConsultorioAPI.Data;
using ConsultorioAPI.Mappers;

namespace ConsultorioAPI.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly DataContext _context;

        public MedicoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MedicoDTO>> GetAllMedicos()
        {
            var medicos = await _context.Medicos.Include(p => p.Pacientes).ToListAsync();
            List<MedicoDTO> medicoDTOs = new List<MedicoDTO>();
            foreach(var medico in medicos)
            {
                medicoDTOs.Add(MedicoMapper.ModelToDTO(medico,_context));
            }
            
            return medicoDTOs;
            
        }

        public async Task<List<Consulta>> GetConsultasByMedico(int medicoID)
        {
            return await _context.Consultas.Where(c => c.IdMedico == medicoID).Include(p => p.Paciente).ToListAsync();
        }

        public async Task<List<MedicoDTO>> GetMedicoByEspecialidade(string especialidade)
        {
            var lista = await _context.Medicos.Where(m => m.Especialidade.ToLower() == especialidade.ToLower()).Include(p => p.Pacientes)
               .ToListAsync();

            List<MedicoDTO> medicoDTOs = new List<MedicoDTO>();

            foreach(var m in lista)
            {
                medicoDTOs.Add(MedicoMapper.ModelToDTO(m, _context));
            }

            return medicoDTOs;
        }



        public async Task<int?> CreateMedico(CreateMedicoDTO medico)
        {

            await _context.AddAsync(MedicoMapper.CreateMedico(medico));
            await _context.SaveChangesAsync();
            return 1;

        }



        public async Task<int?> UpdateEspecialidade(int idMedico, string especialidade)
        {
            var medicoDb = await _context.Medicos.FirstOrDefaultAsync(id => id.Id == idMedico);

            if (medicoDb == null) return null;

            medicoDb.Especialidade = especialidade;
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int?> UpdateMedico(MedicoDTO medico)
        {
            var dbMedico = await _context.Medicos.Include(p => p.Pacientes).FirstOrDefaultAsync(m => m.Id == medico.Id);

            if (dbMedico == null) return null;
            else
            {
                dbMedico = MedicoMapper.DtoToEntity(dbMedico, medico, _context);
                await _context.SaveChangesAsync();
                return 1;
            }
        }

        public async Task<List<MedicoDTO>> GetMedicoDisponivel(DateTime data, string especialidade)
        {
            var medicos = await _context.Medicos.Where(e => e.Especialidade.ToLower() == especialidade.ToLower()).ToListAsync();
            var medicosDisponiveis = new List<MedicoDTO>();
            var consultasDiarias = await _context.Consultas.Where(d => d.DataConsulta.Date == data.Date).ToListAsync();
            
            foreach(var medico in medicos)
            {
                int consultas = 0;
                foreach(var consulta in consultasDiarias)
                {
                    if(consulta.IdMedico == medico.Id)
                    {
                        consultas++;
                    }
                }
                if(consultas < 3)
                {
                    medicosDisponiveis.Add(MedicoMapper.ModelToDTO(medico,_context));
                }
            }

            return medicosDisponiveis;
        }
    }
}
