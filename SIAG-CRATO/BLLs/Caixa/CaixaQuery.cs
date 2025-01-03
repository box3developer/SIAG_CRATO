namespace SIAG_CRATO.BLLs.Caixa;

public class CaixaQuery
{
    public const string SELECT = @$"SELECT id_caixa, id_agrupador, id_pallet, id_programa, id_pedido, cd_produto, cd_cor, cd_gradetamanho, nr_caixa, nr_pares, fg_rfid, fg_status, dt_embalagem, dt_sorter, dt_estufamento, dt_expedicao FROM caixa WITH(NOLOCK)";

    public const string SELECT_COUNT = "SELECT count(*) FROM caixa WITH(NOLOCK)";

    public const string SELECT_COUNT_PENDENTES = @"SELECT 
                                                 CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) as Item1, 
                                                 count(*) as Item2
                                             FROM caixa WITH(NOLOCK)
                                             LEFT JOIN agrupadorativo WITH(NOLOCK) ON agrupadorativo.id_agrupador = caixa.id_agrupador
                                             INNER JOIN areaarmazenagem WITH(NOLOCK) ON agrupadorativo.id_areaarmazenagem = areaarmazenagem.id_areaarmazenagem
                                             WHERE 
                                                 caixa.fg_status < 4 
                                                 AND caixa.dt_sorter IS NOT NULL 
                                                 AND caixa.dt_estufamento IS NULL
                                             GROUP BY CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2)
                                             ORDER BY CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) desc";

    public const string SELECT_PENDENTES_LIDER = @"SELECT 
                                                        CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) AS Item1, 
                                                        COUNT(*) AS Item2
                                                   FROM caixa WITH(NOLOCK)
                                                   LEFT JOIN agrupadorativo WITH(NOLOCK) ON agrupadorativo.id_agrupador = caixa.id_agrupador
                                                   INNER JOIN areaarmazenagem WITH(NOLOCK) ON agrupadorativo.id_areaarmazenagem = areaarmazenagem.id_areaarmazenagem
                                                   LEFT JOIN equipamento WITH(NOLOCK) ON equipamento.nm_identificador = CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2)
                                                   WHERE 
                                                        caixa.fg_status < 4 
                                                        AND caixa.dt_sorter IS NOT NULL 
                                                        AND caixa.dt_estufamento IS NULL 
                                                        AND caixa.dt_sorter > (SELECT MAX(operadorhistorico.dt_evento) 
                                                                               FROM operadorhistorico 
                                                                               WHERE operadorhistorico.cd_evento = 2 
                                                                                     AND operadorhistorico.dt_evento IS NOT NULL 
                                                                                     AND operadorhistorico.id_equipamento = equipamento.id_equipamento)
                                                   GROUP BY CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2)
                                                   ORDER BY CAST(areaarmazenagem.id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) DESC";

    public const string UPDATE_CAIXA_PENDENTE = @"UPDATE equipamento
                                                    SET   cd_leitura_pendente = @id_caixa
                                                    WHERE id_equipamento = @id_equipamento";

    public const string UPDATE_REMOVE_DT_ESTUFAMENTO = @$"UPDATE caixa SET dt_estufamento = null WHERE id_caixa = @id_caixa";

    public const string UPDATE_SET_DT_ESTUFAMENTO_BY_CAIXA = $@"UPDATE caixa SET dt_estufamento = @dt_estufamento WHERE id_caixa = @id_caixa";
}
