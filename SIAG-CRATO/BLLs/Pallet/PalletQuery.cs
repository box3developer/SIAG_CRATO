namespace SIAG_CRATO.BLLs.Pallet;

public class PalletQuery
{
    public const string SELECT = @"SELECT id_pallet, id_areaarmazenagem, id_agrupador, fg_status, qt_utilizacao, dt_ultimamovimentacao, cd_identificacao FROM pallet WITH(NOLOCK)";
    public const string INSERT = "INSERT INTO pallet (id_pallet, id_areaarmazenagem, id_agrupador, fg_status, qt_utilizacao, dt_ultimamovimentacao, cd_identificacao) VALUES (@Codigo, @AreaArmazenagem, @Agrupador, @Status, @QtUtilizacao, @DataUltimaMovimentacao, @Identificacao)";

    public const string UPDADE_STATUS = "UPDATE pallet SET fg_status = @status WHERE id_pallet = @id";

    public const string ALOCA_PALLET = "UPDATE pallet SET id_areaarmazenagem = @idAreaArmazenagem WHERE id_pallet = @id";

    public const string SELECT_COUNT_CAIXAS = "SELECT COUNT(*) FROM caixa WITH(NOLOCK)";

    public const string SELECT_RESERVA = @"SELECT top 1 
                                                  id_pallet,
                                                  pallet.id_areaarmazenagem,
                                                  pallet.id_agrupador,
                                                  pallet.fg_status
                                           FROM areaarmazenagem WITH(NOLOCK)
                                           INNER JOIN pallet WITH(NOLOCK) ON areaarmazenagem.id_areaarmazenagem = pallet.id_areaarmazenagem";

    public const string UPDATE_AREAARMAZENAGEM = @"UPDATE areaarmazenagem 
                                                   SET id_agrupador = @idAgrupador, fg_status = @status 
                                                   WHERE id_areaarmazenagem = @idAreaArmazenagem";

    public const string COUNT_PALLETS = @"SELECT COUNT(1)
                                            FROM pallet
                                            INNER JOIN areaarmazenagem 
                                                ON pallet.id_areaarmazenagem = areaarmazenagem.id_areaarmazenagem
                                                AND pallet.id_agrupador = @id_agrupador
                                            WHERE areaarmazenagem.id_endereco = @id_endereco";

    public const string UPDATE_AGRUPADOR_STATUS_BY_ID = @"UPDATE pallet SET id_agrupador = @idAgrupador, fg_status = @status WHERE id_pallet = @idPallet";
}
