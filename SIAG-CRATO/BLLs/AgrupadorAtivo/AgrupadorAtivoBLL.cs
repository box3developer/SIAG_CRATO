using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Log;
using SIAG_CRATO.Data;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AgrupadorAtivo
{
    public class AgrupadorAtivoBLL
    {
        public static async Task<int> GetAgrupadorStatus(Guid idAgrupador)
        {
            try
            {

                var query = $@"{AgrupadorAtivoQuery.SELECT} WHERE id_agrupador = @idAgrupador";

                using var conexao = new SqlConnection(Global.Conexao);
                
                var status = await conexao.QueryFirstOrDefaultAsync<AgrupadorAtivoModel>(query, new { idAgrupador })
                ??
                 throw new Exception("Não foi possivel recuperar AgrupadorAtivo");

                return (int)status.FgStatus;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> FinalizaAgrupador(Guid idAgrupador, Guid idRequisição)
        {
            try
            {
                var logInitial = new LogModel
                {
                    IdRequisicao = idRequisição,
                    NomeIdentificador = "",
                    IdCaixa = "",
                    Data = DateTime.Now,
                    Mensagem = $"Finalizando agrupador {idAgrupador}. Troca status do agrupador para 4",
                    Metodo = "FinalizarAgrupador",
                    IdOperador = "",
                    Tipo = "info",
                };

                await LogBLL.CreateLogCaracol(logInitial);

                using var conexao = new SqlConnection(Global.Conexao);

                var rows = await conexao.ExecuteAsync(AgrupadorAtivoQuery.UPDATE_FINALIZA_AGRUPADOR, new { idAgrupador });

                return rows > 0;
            }
            catch(Exception ex)
            {
                var logError = new LogModel
                {
                    IdRequisicao = idRequisição,
                    NomeIdentificador = "",
                    IdCaixa = "",
                    Data = DateTime.Now,
                    Mensagem = $"Erro ao finalizar agrupador {idAgrupador}",
                    Metodo = "FinalizarAgrupador",
                    IdOperador = "",
                    Tipo = "erro",
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception(ex.Message);
            }
           
        }


        public static async Task<bool> LiberarAgrupador(Guid idAgrupador, Guid idRequisição)
        {
            try
            {
                var logInitial = new LogModel
                {
                    IdRequisicao = idRequisição,
                    NomeIdentificador = "",
                    IdCaixa = "",
                    Data = DateTime.Now,
                    Mensagem = $"Removendo vínculo com área de armazenagem do agrupador {idAgrupador}",
                    Metodo = "LiberarAgrupador",
                    IdOperador = "",
                    Tipo = "info",
                };

                await LogBLL.CreateLogCaracol(logInitial);

                using var conexao = new SqlConnection(Global.Conexao);

                var rows = await conexao.ExecuteAsync(AgrupadorAtivoQuery.UPDATE_FINALIZA_AGRUPADOR, new { idAgrupador });

                return rows > 0;
            }
            catch (Exception ex)
            {
                var logError = new LogModel
                {
                    IdRequisicao = idRequisição,
                    NomeIdentificador = "",
                    IdCaixa = "",
                    Data = DateTime.Now,
                    Mensagem = $"Erro ao remover vínculo com área de armazenagem do agrupador {idAgrupador}",
                    Metodo = "FinalizarAgrupador",
                    IdOperador = "",
                    Tipo = "erro",
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception(ex.Message);
            }

        }
    }

    
}
