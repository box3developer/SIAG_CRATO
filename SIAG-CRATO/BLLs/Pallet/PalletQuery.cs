namespace SIAG_CRATO.BLLs.Pallet;

public class PalletQuery
{
    public const string SELECT = @"SELECT id_pallet, id_areaarmazenagem, id_agrupador, fg_status, qt_utilizacao, dt_ultimamovimentacao, cd_identificacao FROM pallet WITH(NOLOCK)";
    public const string INSERT = "INSERT INTO pallet (id_pallet, id_areaarmazenagem, id_agrupador, fg_status, qt_utilizacao, dt_ultimamovimentacao, cd_identificacao) VALUES (@Codigo, @AreaArmazenagem, @Agrupador, @Status, @QtUtilizacao, @DataUltimaMovimentacao, @Identificacao)";

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


}
