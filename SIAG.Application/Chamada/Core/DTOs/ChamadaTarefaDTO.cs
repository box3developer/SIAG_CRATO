using SIAG.Application.Chamada.Cadastro.DTOs;

namespace SIAG.Application.Chamada.Core.DTOs
{
    public class ChamadaTarefaDTO
    {
        public Guid IdChamada { get; set; }

        public ChamadaDTO? Chamada { get; set; }

        public int IdTarefa { get; set; }

        public AtividadeTarefaDTO? Tarefa { get; set; }

        public DateTime? DtInicio { get; set; }

        public DateTime? DtFim { get; set; }
    }
}
