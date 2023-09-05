namespace ConsultorioAPI.DTOs
{
    public class CreateMedicoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public DateTime DataAdmissão { get; set; } = DateTime.MinValue;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; } = DateTime.MinValue;
        public string Genero { get; set; } = string.Empty;
    }
}
