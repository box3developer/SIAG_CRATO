﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("programa")]
    public class Programa
    {
        [Key]
        [Column("id_programa")]
        public int ProgramaId { get; set; }

        [Column("cd_programa")]
        public int CdPrograma { get; set; }

        [Column("cd_documento")]
        public int CdDocumento { get; set; }

        [Column("cd_fabrica")]
        public int CdFabrica { get; set; }

        [Column("cd_estabelecimento")]
        public int CdEstabelecimento { get; set; }

        [Column("cd_equipamento")]
        public string CdEquipamento { get; set; }

        [Column("dt_liberacao")]
        public DateTime DtLiberacao { get; set; }

        [Column("fg_tipo")]
        public int FgTipo { get; set; }

        [Column("cd_deposito")]
        public string CdDeposito { get; set; }

        [Column("qt_alturacaixa")]
        public decimal QtAlturaCaixa { get; set; }

        [Column("qt_larguracaixa")]
        public decimal QtLarguraCaixa { get; set; }

        [Column("qt_comprimentocaixa")]
        public decimal QtComprimentoCaixa { get; set; }
    }
}