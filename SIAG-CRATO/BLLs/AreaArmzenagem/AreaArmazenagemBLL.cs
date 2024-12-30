using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Parametro;
using SIAG_CRATO.Data;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AreaArmzenagem;

public class AreaArmazenagemBLL
{
    public static async Task<List<AreaArmazenagemModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(AreaArmazenagemQuery.SELECT);

        return areasArmazenagem.ToList();
    }

    public static async Task<AreaArmazenagemModel?> GetByIdAsync(int id)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} WHERE id_areaarmazenagem = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { id });

        return areasArmazenagem;
    }


    public static async Task<AreaArmazenagemModel?> GetByAgrupadorAsync(int idAgrupador)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} WHERE id_agrupador = @idAgrupador";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { idAgrupador });

        return areasArmazenagem;
    }

    public static async Task<AreaArmazenagemModel?> GetByPosicaoAsync(string identificadorCaracol, int posicaoY)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} 
                     WHERE CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                           AND nr_posicaoy = @posicaoY";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { identificadorCaracol, posicaoY });

        return areasArmazenagem;
    }

    public static async Task<List<AreaArmazenagemModel>> GetByIdentificadorCaracolAsync(string identificadorCaracol)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} 
                     WHERE CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                     ORDER BY nr_posicaoy DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(sql, new { identificadorCaracol });

        return areasArmazenagem.ToList();
    }

    public static async Task<int> SetStatusAsync(long id, StatusAreaArmazenagem status)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var area = await conexao.ExecuteAsync(AreaArmazenagemQuery.UPDATE_STATUS, new { status = (int)status, id });

        return area;
    }

    public static async Task<AreaArmazenagemModel?> GetStageInLivreAsync(int idEndereco)
    {
        var parametroEntity = await ParametroBLL.GetParametroByParametro("TIPO AREA STAGEIN")
        ??
            throw new Exception("Erro ao executar StageInLivre");

        var nmValor = Int16.Parse(parametroEntity.Valor ?? "");

        var sql = $@"{AreaArmazenagemQuery.SELECT} where id_endereco = @idEndereco
		                                            and id_tipoarea = @nmValor
		                                            and fg_status = 1
		                                            order by nr_posicaoy, nr_lado";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { idEndereco, nmValor });

        return areasArmazenagem;

    }
}
