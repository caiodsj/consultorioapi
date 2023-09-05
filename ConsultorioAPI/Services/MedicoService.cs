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

        public async Task<List<Consulta>> GetConsultasByMedico(int medicoID)
        {
            return await _context.Consultas.Where(c => c.IdMedico == medicoID).ToListAsync();
        }

        public async Task<List<MedicoDTO>> GetMedicoByEspecialidade(string especialidade)
        {
            return await _context.Medicos.Where(m => m.Especialidade.ToLower() == especialidade.ToLower()).Include(p => p.Pacientes)
                .Select(m => MedicoMapper.ModelToDTO(m)).ToListAsync();
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
    }
}
