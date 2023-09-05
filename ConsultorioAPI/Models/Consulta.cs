namespace ConsultorioAPI.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; } = DateTime.MinValue;
        public string Descricao { get; set; } = string.Empty;
        public string PrescricaoMedica { get; set; } = string.Empty;
        public double ValorConsulta { get; set; } = 0;
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
    }
}
