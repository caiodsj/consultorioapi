namespace ConsultorioAPI.DTOs
{
    public class CreatePacienteDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; } = DateTime.MinValue;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
