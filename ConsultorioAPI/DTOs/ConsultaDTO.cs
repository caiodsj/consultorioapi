namespace ConsultorioAPI.DTOs
{
    public class ConsultaDTO
    {
        public DateTime DataConsulta { get; set; } = DateTime.MinValue;
        public string Descricao { get; set; } = string.Empty;
        public string PrescricaoMedica { get; set; } = string.Empty;
        public double ValorConsulta { get; set; } = 0;
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
    }
}
