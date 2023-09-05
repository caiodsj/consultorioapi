using ConsultorioAPI.Data;
using Microsoft.IdentityModel.Tokens;

namespace ConsultorioAPI.Mappers
{
    public class MedicoMapper
    {

        public static  MedicoDTO ModelToDTO(Medico medico, DataContext _context)
        {
            MedicoDTO medicoDTO = new MedicoDTO()
            {
                CRM = medico.CRM,
                DataAdmissão = medico.DataAdmissão,
                DataNascimento = medico.DataNascimento,
                Especialidade = medico.Especialidade,
                Genero = medico.Genero,
                Id = medico.Id,
                Nome = medico.Nome,
                Telefone = medico.Telefone,

            };

            var paciente = _context.Consultas.ToList();
            
            foreach (var p in paciente)
            {
               if(p.IdMedico == medicoDTO.Id)
                {
                    medicoDTO.Pacientes.Add(_context.Pacientes.Find(p.IdPaciente).Nome);
                }
            }
            return medicoDTO;
        }

        public static Medico CreateMedico(CreateMedicoDTO dto)
        {
            Medico medico = new Medico()
            {
                Telefone = dto.Telefone,
                Especialidade = dto.Especialidade,
                CRM = dto.CRM,
                DataAdmissão = dto.DataAdmissão,
                DataNascimento = dto.DataNascimento,
                Nome = dto.Nome,
                Genero = dto.Genero
            };

            return medico;
        }

        public static Medico DtoToEntity(Medico medico, MedicoDTO medicoDTO, DataContext context)
        {
            medico.DataAdmissão = medicoDTO.DataAdmissão;

            medico.DataNascimento = medicoDTO.DataNascimento;

            if (medicoDTO.Especialidade != "string" && !medicoDTO.Especialidade.IsNullOrEmpty()) medico.Especialidade = medicoDTO.Especialidade;
            if (medicoDTO.CRM != "string" && !medicoDTO.CRM.IsNullOrEmpty()) medico.CRM = medicoDTO.CRM;
            if (medicoDTO.Genero != "string" && !medicoDTO.Genero.IsNullOrEmpty()) medico.Genero = medicoDTO.Genero;
            if (medicoDTO.Telefone != "string" && !medicoDTO.Telefone.IsNullOrEmpty()) medico.Telefone = medicoDTO.Telefone;
            if (medicoDTO.Nome != "string" && !medicoDTO.Nome.IsNullOrEmpty()) medico.Nome = medicoDTO.Nome;

            foreach (var paciente in medicoDTO.Pacientes)
            {
                var p = context.Pacientes.FirstOrDefault(p => p.Nome.ToLower() == paciente.ToLower());
                if (p != null)
                {
                    medico.Pacientes.Add(p);
                }


            }
            return medico;
        }
    }
}
