namespace SIAG_CRATO.BLLs.CaixaLeitura;

public class CaixaLeituraQuery
{
    public const string SELECT = @"SELECT id_caixaleitura, id_caixa, dt_leitura, fg_tipo,
                                          fg_status, id_operador, id_equipamento,
                                          id_pallet, id_areaarmazenagem, id_endereco,
                                          fg_cancelado, id_ordem
                                    FROM caixaleitura WITH(NOLOCK)";

    public const string INSERT = "INSERT INTO caixaleitura (id_caixa, dt_leitura, fg_tipo, fg_status, id_operador, id_equipamento, id_pallet, id_areaarmazenagem, id_endereco, fg_cancelado, id_ordem) " +
                                  "VALUES (@idCaixa, @dtLeitura, @fgTipo, @fgStatus, @idOperador, @idEquipamento, @idPallet, @idAreaArmazenagem, @idEndereco, @fgCancelado, @idOrdem)";
}
