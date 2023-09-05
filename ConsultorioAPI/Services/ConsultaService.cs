using ConsultorioAPI.Data;

namespace ConsultorioAPI.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly DataContext _context;

        public ConsultaService(DataContext context)
        {
            _context = context;
        }

        public async Task<Consulta> CreateConsultaAsync(ConsultaDTO consulta)
        {
            var consultaModel = new Consulta
            {
                DataConsulta = consulta.DataConsulta,
                Descricao = consulta.Descricao,
                PrescricaoMedica = consulta.PrescricaoMedica,
                ValorConsulta = consulta.ValorConsulta,
                IdMedico = consulta.IdMedico,
                IdPaciente = consulta.IdPaciente
            };
            await _context.Consultas.AddAsync(consultaModel);
            await _context.SaveChangesAsync();
            return (consultaModel);
        }

        public async Task<string> DeleteConsultaAsync(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta is null) return null;
            _context.Remove(consulta);
            await _context.SaveChangesAsync();
            return "Consulta excluída com sucesso.";
        }

        public async Task<List<Consulta>> GetConsultasPorData(DateTime data)
        {
            return await _context.Consultas.Where(c => c.DataConsulta.Date == data.Date).ToListAsync();
        }
    }
}
