
using ConsultorioAPI.Data;
using ConsultorioAPI.Models;

namespace ConsultorioAPI.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly DataContext _context;

        public PacienteService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> CreatePacienteAsync(CreatePacienteDTO request)
        {
            var paciente = new Paciente
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                CPF = request.CPF,
                Telefone = request.Telefone,
                Endereco = request.Endereco,
                Sexo = request.Sexo,
                Email = request.Email
            };
            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
            return "Paciente criado com sucesso";
        }

        public async Task<object> GetAllConsultasByPacienteAsync(int id)
        {
            var dataAtual = DateTime.UtcNow.Date;

            var consultas = await _context.Consultas
                .Where(c => c.IdPaciente == id && c.DataConsulta >= dataAtual)
                .OrderBy(c => c.DataConsulta)
                .ToListAsync();

            if (consultas.Count == 0)
            {
                return "O paciente não possui consultas marcadas a partir da data atual.";
            }

            return consultas;
        }


        public async Task<List<Paciente>> GetAllPacienteMaiorQueAsync(int idade)
        {
            var dataComparacao = DateTime.Now.AddYears(-idade);
            return await _context.Pacientes.Where(p => p.DataNascimento <= dataComparacao).ToListAsync();
        }

        public async Task<List<Consulta>> GetHistoricoMedicoAsync(int id)
        {
            var historico = await _context.Consultas
                .Where(c => c.IdPaciente == id)
                .OrderBy(c => c.DataConsulta)
                .ToListAsync();
            return historico;
        }

        public async Task<string> UpdateEnderecoAsync(int id, string endereco)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente is null) return null;
            paciente.Endereco = endereco;
            await _context.SaveChangesAsync();
            return "Endereço atualizado com sucesso";

        }

        public async Task<string> UpdatePacienteAsync(int id, PacienteDTO request)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente is null) return null;
            paciente.Nome = request.Nome;
            paciente.DataNascimento = request.DataNascimento;
            paciente.Telefone = request.Telefone;
            paciente.Endereco = request.Endereco;
            paciente.Sexo = request.Sexo;
            paciente.Email = request.Email;
            await _context.SaveChangesAsync();
            return "Paciente atualizado com sucesso";
        }
    }
}
