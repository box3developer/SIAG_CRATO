using SIAG.Domain.Armazenagem.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("desempenhoonline")]
    public class DesempenhoOnline
    {
        [Key]
        [Column("id_desempenhoonline")]
        public int IdDesempenhoonline { get; set; }

        [Column("id_operador")]
        public int IdOperador { get; set; }

        [Column("id_equipamentomodelo")]
        public int IdEquipamentomodelo { get; set; }

        [Column("id_setortrabalho")]
        public int IdSetortrabalho { get; set; }

        [Column("nr_tempologado")]
        public int NrTempologado { get; set; }

        [Column("nr_tempoprevisto")]
        public int NrTempoprevisto { get; set; }

        [Column("nr_temporealizado")]
        public int NrTemporealizado { get; set; }

        [Column("qt_prevista")]
        public int QtPrevista { get; set; }

        [Column("qt_realizada")]
        public int QtRealizada { get; set; }

        [Column("dt_registro")]
        public DateTime DtRegistro { get; set; }
    }
}
