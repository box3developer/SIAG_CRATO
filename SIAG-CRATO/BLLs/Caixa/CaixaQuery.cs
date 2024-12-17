namespace SIAG_CRATO.BLLs.Caixa;

public class CaixaQuery
{
    public const string SELECT = $"SELECT id_caixa, id_agrupador, id_pedido, dt_expedicao, dt_estufamento, id_pallet, fg_status, dt_sorter FROM caixa WITH(NOLOCK)";

    public const string SELECT_COUNT = "SELECT count(*) FROM caixa WITH(NOLOCK)";

    public const string SELECT_PENDENTES = @"SELECT 
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
}
