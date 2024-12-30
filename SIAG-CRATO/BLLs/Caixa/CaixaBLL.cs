using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Caixa;

public class CaixaBLL
{
    public async static Task<CaixaModel?> GetByIdAsync(string id)
    {
        var sql = $"{CaixaQuery.SELECT} WHERE id_caixa = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixa = await conexao.QueryFirstOrDefaultAsync<CaixaModel>(sql, new { id });

        return caixa;
    }

    public async static Task<List<CaixaModel>> GetByPalletAsync(int idPallet)
    {
        var sql = $"{CaixaQuery.SELECT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixas = await conexao.QueryAsync<CaixaModel>(sql, new { idPallet });

        return caixas.ToList();
    }

    public async static Task<int> GetQuantidadeByPalletAsync(int idPallet)
    {
        var sql = $"{CaixaQuery.SELECT_COUNT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var quantidade = await conexao.QueryFirstOrDefaultAsync<int>(sql, new { idPallet });

        return quantidade;
    }

    public async static Task<int> GetQuantidadePendentesAsync(int idAgrupador)
    {
        var sql = $"{CaixaQuery.SELECT_COUNT} WHERE id_agrupador = @idAgrupador AND (fg_status < 4 OR fg_status = 8)";

        using var conexao = new SqlConnection(Global.Conexao);
        var quantidade = await conexao.QueryFirstOrDefaultAsync<int>(sql, new { idAgrupador });

        return quantidade;
    }

    public async static Task<Dictionary<string, int>> GetPendentesAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var caixasPendentes = await conexao.QueryAsync<Tuple<string, int>>(CaixaQuery.SELECT_COUNT_PENDENTES);

        return caixasPendentes.ToDictionary(x => x.Item1, x => x.Item2);
    }

    public async static Task<Dictionary<string, int>> GetPendentesByLiderAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var caixasPendentes = await conexao.QueryAsync<Tuple<string, int>>(CaixaQuery.SELECT_PENDENTES_LIDER);

        return caixasPendentes.ToDictionary(x => x.Item1, x => x.Item2);
    }

    public async static Task<string> GetFabricaAsync(string idCaixa)
    {
        var sqlCaixa = "SELECT id_programa FROM caixa WITH(NOLOCK) WHERE id_caixa = @idCaixa";
        var sqlPrograma = "SELECT cd_fabrica FROM programa WITH(NOLOCK) WHERE id_programa = @idPrograma";

        using var conexao = new SqlConnection(Global.Conexao);
        var idPrograma = await conexao.QueryFirstOrDefaultAsync<string>(sqlCaixa, new { idCaixa });
        var fabrica = await conexao.QueryFirstOrDefaultAsync(sqlPrograma, new { idPrograma });

        return fabrica ?? "";
    }
}
