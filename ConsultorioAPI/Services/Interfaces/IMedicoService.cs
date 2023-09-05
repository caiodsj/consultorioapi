namespace ConsultorioAPI.Services.Interfaces
{
    public interface IMedicoService
    {
        public Task<List<MedicoDTO>> GetAllMedicos();
        public Task<List<Consulta>> GetConsultasByMedico(int medicoID);
        public Task<List<MedicoDTO>> GetMedicoByEspecialidade(string especialidade);
        public Task<List<MedicoDTO>> GetMedicoDisponivel(string data, string especialidade);
        public Task<int?> CreateMedico(CreateMedicoDTO medico);
        public Task<int?> UpdateMedico(MedicoDTO medico);
        public Task<int?> UpdateEspecialidade(int idMedico, string especialidade);
    }
}
