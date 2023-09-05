namespace ConsultorioAPI.Services.Interfaces
{
    public interface IPacienteService
    {
        Task<object> GetAllConsultasByPacienteAsync(int id);
        Task<List<Paciente>> GetAllPacienteMaiorQueAsync(int idade);
        Task<List<Consulta>> GetHistoricoMedicoAsync(int id);
        Task<string> UpdatePacienteAsync(int id, PacienteDTO request);
        Task<string> CreatePacienteAsync(CreatePacienteDTO request);
        Task<string> UpdateEnderecoAsync(int id, string endereco);
    }
}
