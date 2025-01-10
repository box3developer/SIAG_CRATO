public class AcaoPrevisaoVeiculo
    {
        public const int Consultar = 1;
        public const int Salvar = 2;
        public const int ConsultarPorPedido = 3;
    }
public class AgendamentoSistema
    {
        public const int Nenhum = 0;
        public const int Embalagem = 1;
        public const int Sorter = 2;
        public const int Armazenagem = 4;
        public const int Expedicao = 8;
    }
public class StatusAtivo
{
        public const int Inativo = 0;
        public const int Ativo = 1;
    }
public class ConflitoDeEnderecos
    {
        public const int SemBloqueio = 0;
        public const int BloquearEndereco = 1;
        public const int RestringirPorZona = 2;
        public const int RestringirPorZonaEEndereco = 3;
    }
public class ControleEndereco
    {
        public const int UtilizaAmbos = 0;
        public const int UtilizaLadoUm = 1;
        public const int UtilizaLadoDois = 2;
    }
public class ControleSMS
    {
        public const int Indefinido = 0;
        public const int NaoEnviouSMS = 1;
        public const int EnviouSMS = 2;
        public const int ReenviouSMS = 3;
        public const int IgnorarSMS = 9;
    }
public class DirecaoAlocacaoSorter
    {
        public const int Desconhecido = 0;
        public const int InicioProximoAoBuffer = 1;
        public const int InicioLongeDoBuffer = 2;
    }
public class DisponibilidadeCaixa
    {
        public const int Indisponivel = 0;
        public const int Parcial = 1;
        public const int Disponivel = 2;
    }
public class EquipamentoModeloDefault
    {
        public const int Indefinido = 0;
        public const int Caracol = 1;
        public const int Paleteira = 2;
        public const int EmpilhadeiraRetratil = 3;
        public const int EmpilhadeiraBilateral = 4;
        public const int TV = 5;
    }
public class Estabelecimentos
    {
        public const int Sobral = 20;
        public const int Fortaleza = 21;
        public const int Crato = 40;
        public const int Farroupilha = 24;
        public const int Teixeira = 60;
    }
public class EventoOperador
    {
        public const int Indefinido = 0;
        public const int LogIn = 1;
        public const int LogOut = 2;
        public const int QuebraSequenciaPallet = 3;
        public const int LiberaPallet = 4;
        public const int ChamadaManualPallet = 5;
        public const int DesabilitaSequenciaTipoCarga = 6;
    }
public class EventoOrdem
    {
        public const int Programada = 1;
        public const int Alocada = 2;
        public const int Conferida = 3;
        public const int AlteradaCarga = 4;
        public const int Finalizada = 5;
        public const int Cancelada = 6;
        public const int EntradaAutorizada = 7;
        public const int SaidaAutorizada = 8;
        public const int SequenciaCarregamento = 9;
        public const int AlteradaDoca = 10;
        public const int ProgramacaoInconsistente = 11;
        public const int PriorizacaoOrdem = 12;
        public const int AlteradaCargaAposEncerramento = 13;
        public const int AlteradaCargaAposFinalizada = 14;
        public const int AlteradoLadoUtilizadoDoca = 15;
        public const int EmailEnviado = 16;
        public const int ChegadaMotorista = 17;
        public const int Contratada = 18;
    }
public class ExcelColunas
    {
        public const int Colunas = 2;
        public const int Meta = 3;
        public const int Realizado = 4;
        public const int Percentual = 5;
        public const int Diferenca = 6;
        public const int FinalCongelado = 7;
        public const int Fatias = 8;
    }
public class ExcelLinhas
    {
        public const int AntesTitulo = 0;
        public const int Titulo = 1;
        public const int SubTitulo = 2;
        public const int EmbarqueTotal = 3;
        public const int EmbarqueMI = 4;
        public const int EmbarqueME = 5;
        public const int Faturamento = 6;
        public const int PreData = 7;
        public const int DisponivelEmbarque_MI = 8;
        public const int DisponivelEmbarque_PD = 9;
        public const int DisponivelEmbarque_MI_PD = 10;
        public const int DisponivelEmbarque_ME = 11;
        public const int DisponivelEmbarque_MI_ME_PD = 12;
        public const int DisponivelEmbarque_MI_ME = 13;
    }
public class ExcelLocalidades
    {
        public const int Grendene = 5;
        public const int Sobral = 20;
        public const int Crato = 35;
        public const int Teixeira = 50;
        public const int Fortaleza = 65;
        public const int Farroupilha = 80;
    }
public class Fabrica
    {
        public const int SobralFabrica1 = 7501;
        public const int SobralFabrica3 = 7503;
        public const int SobralFabrica4 = 7504;
        public const int SobralFabrica5 = 7505;
        public const int SobralFabrica6 = 7506;
        public const int SobralFabrica7 = 7507;
        public const int FortalezaFabrica1 = 7101;
        public const int CratoFabrica1 = 7001;
        public const int TeixeiraFabrica1 = 6001;
    }
public class FinalizarTurno
    {
        public const int InicioDoSeguinte = 1;
        public const int FimDoAtual = 2;
    }
public class FuncaoOperador
    {
        public const int Indefinida = 0;
        public const int Administrador = 1;
        public const int OperadorSorter = 2;
        public const int OperadorEmpilhadeira = 3;
        public const int OperadorExpedicao = 4;
        public const int SupervisorCD = 5;
        public const int Auditor = 6;
    }
public class Funcoes
    {
        public const int DEFAULT = 0;
        public const int PRINCIPAL = 0;
        public const int RELATORIO = 0;
        public const int DASHBOARD = 0;
        public const int PORTLETMENUFAVORITO = 0;
        public const int CADASTROSETORTRABALHO = 5141;
        public const int CONSULTASETORTRABALHO = 5141;
        public const int CADASTROREGIAOTRABALHO = 5142;
        public const int CONSULTAREGIAOTRABALHO = 5142;
        public const int CADASTROTIPOENDERECO = 5143;
        public const int CONSULTATIPOENDERECO = 5143;
        public const int CADASTROTIPOAREA = 5144;
        public const int CONSULTATIPOAREA = 5144;
        public const int CADASTROENDERECO = 5145;
        public const int CONSULTAENDERECO = 5145;
        public const int CADASTROAREAARMAZENAGEM = 5146;
        public const int CONSULTAAREAARMAZENAGEM = 5146;
        public const int OCUPACAOARMAZEM = 5147;
        public const int ALOCACAOSORTER = 5148;
    }
public class HumorEficiencia
    {
        public const int Feliz = 1;
        public const int Regular = 2;
        public const int Triste = 3;
    }
public class IniciarDia
    {
        public const int Turno1 = 1;
        public const int Turno2 = 2;
        public const int Turno3 = 3;
    }
public class LadoCorredor
    {
        public const int NaoPossui = 0;
        public const int Esquerdo = 1;
        public const int Direito = 2;
    }
public class LoteSKU
    {
        public const int SemSKU = 0;
        public const int ComSKUProgramaCorGrade = 1;
        public const int ComSKUProgramaCor = 2;
        public const int ComSKUPrograma = 3;
    }

public class Meses
    {
        public const int Janeiro = 1;
        public const int Fevereiro = 2;
        public const int Marco = 3;
        public const int Abril = 4;
        public const int Maio = 5;
        public const int Junho = 6;
        public const int Julho = 7;
        public const int Agosto = 8;
        public const int Setembro = 9;
        public const int Outubro = 10;
        public const int Novembro = 11;
        public const int Dezembro = 12;
    }
public class ModeloEquipamento
    {
        public const int Desconhecido = 0;
        public const int PaleteiraPatinha = 1;
        public const int EmpilhadeiraConvencional = 2;
        public const int Bilateral = 3;
    }
public class Recursos
    {
        public const int Indefinido = 0;
        public const int Automatico = 1;
        public const int LeituraPallet = 2;
        public const int LeituraEndereco = 3;
        public const int LeituraPalletEndereco = 4;
        public const int ConfirmarOperacao = 5;
    }
public class RegraAlocacaoSorter
    {
        public const int Desconhecido = 0;
        public const int CaracolMenosOcupado = 1;
        public const int RamalECaracolComMenorCargaEmbalada = 2;
        public const int RamalECaracolComMenorCargaDeCaixasPendentes = 3;
        public const int RamalECaracolComMenosCaixasRecebidas = 4;
        public const int RamalECaracolComMenorCargaDeCaixasIgnorandoRamalComMaisCaixasRecebidas = 5;
        public const int RamalECaracolComMenosCaixasRecebidasIgnorandoRamalComMaiorCargaDeCaixas = 6;
        public const int PriorizacaoDaAvenida1RamalECaracolComMenorCargaDeCaixasIgnorandoRamalComMaisCaixasRecebidas = 7;
    }
public class RejeicaoTarefa
    {
        public const int Indefinido = 0;
        public const int NaoPermite = 1;
        public const int Permite = 2;
    }
public class Restricao
    {
        public const int Indefinido = 0;
        public const int SemRestricao = 1;
        public const int AcessoBloqueado = 2;
    }
public class RestricaoVeiculo
    {
        public const int MotoristaExclusivo = 1;
        public const int QualquerMotorista = 2;
    }
public class SequenciamentoEmbarque
    {
        public const int Nao = 0;
        public const int Sim = 1;
    }
public class SessionObjects
    {
        public const int UsuarioLogado = 0;
        public const int Chave = 1;
        public const int Contestacao = 2;
        public const int MensagemErro = 3;
        public const int Filtro = 4;
        public const int Ordenarcao = 5;
        public const int RetornoBox = 6;
        public const int RetornoPedidos = 7;
        public const int DataFaturamento = 8;
        public const int HierarquiaDesempenho = 9;
        public const int MimeTypeRelatorio = 10;
        public const int VisaoEmbarque = 11;
        public const int ParametroTelao = 12;
        public const int PrevisaoVeiculoUsuario = 13;
        public const int ParametrosLinkExterno = 14;
        public const int ConsultaNotaFiscal = 15;
        public const int HoraSessaoRenovada = 999;
    }
public class SetorTrabalhoDefault
    {
        public const int Sorter = 1;
        public const int Buffer = 2;
        public const int PortaPallet = 3;
        public const int Expedicao = 4;
    }
public class Sexo
    {
        public const int Masculino = 1;
        public const int Feminino = 2;
    }
public class Sistema
    {
        public const int Codigo = 586;
    }
public class Status
    {
        public const int Indefinido = 0;
        public const int Inativo = 1;
        public const int Ativo = 2;
        public const int Manutencao = 3;
        public const int Ocupado = 4;
        public const int Sorter = 20;
        public const int Armazenado = 21;
        public const int Expedido = 22;
        public const int Cancelado = 40;
        public const int Faturado = 41;
        public const int CanceladoInadimplencia = 42;
        public const int Reservado = 50;
    }
public class StatusAgendamento
    {
        public const int Indefinido = 0;
        public const int Aguardando = 1;
        public const int Executando = 2;
    }
public class StatusAgrupador
    {
        public const int Inativo = 0;
        public const int NaFila = 1;
        public const int Reservado = 2;
        public const int Alocado = 3;
        public const int Concluido = 4;
    }
public class StatusAreaArmazenagem
    {
        public const int Livre = 1;
        public const int Reservado = 2;
        public const int Ocupado = 3;
        public const int Manutencao = 4;
        public const int Desabilitado = 5;
        public const int Bloqueado = 6;
    }
public class StatusAuditoria
    {
        public const int Indefinido = 0;
        public const int EmAndamento = 1;
        public const int Finalizado = 2;
        public const int Cancelado = 3;
    }
public class StatusCaixa
    {
        public const int Implantada = 1;
        public const int Produzido = 2;
        public const int Embalada = 3;
        public const int Armazenada = 4;
        public const int Expedida = 5;
        public const int Cancelada = 6;
        public const int Consumida = 7;
        public const int Retrabalhada = 8;
    }
public class StatusCaixaLeitura
    {
        public const int Pendente = 0;
        public const int LeituraOK = 1;
        public const int Cancelada = 2;
    }
public class StatusChamada
    {
        public const int Dependente = 0;
        public const int Aguardando = 1;
        public const int Recebido = 2;
        public const int Andamento = 3;
        public const int Rejeitado = 4;
        public const int Finalizado = 5;
        public const int Suspensa = 6;
        public const int Cancelada = 7;
    }
public class StatusEndereco
    {
        public const int Indefinido = 0;
        public const int Inativo = 1;
        public const int Ativo = 2;
        public const int Manutencao = 3;
    }
public class StatusEquipamento
    {
        public const int Indefinido = 0;
        public const int Inativo = 1;
        public const int Ativo = 2;
        public const int Manutencao = 3;
    }
public class StatusEquipamentoOperacao
    {
        public const int Indefinido = 0;
        public const int OffLine = 1;
        public const int Normal = 10;
        public const int Atencao = 20;
        public const int Critico = 30;
    }
public class StatusModeloEquipamento
    {
        public const int Inativo = 1;
        public const int Ativo = 2;
    }
public class StatusMonitorExecucao
    {
        public const int Indefinido = 0;
        public const int Criado = 1;
        public const int Agendado = 2;
        public const int EmExecucao = 3;
        public const int Finalizado = 4;
        public const int Cancelado = 5;
        public const int Erro = 6;
    }
public class StatusMotorista
    {
        public const int NaoEstaNaEmpresa = 0;
        public const int EstaNaEmpresa = 1;
        public const int AguardandoForaDaEmpresa = 2;
    }
public class StatusOrdem
    {
        public const int Indefinido = 0;
        public const int Disponivel = 1;
        public const int AguardandoLiberacao = 5;
        public const int Programada = 11;
        public const int Alocada = 12;
        public const int Conferida = 13;
        public const int AguardandoExpedicao = 21;
        public const int EmExpedicao = 22;
        public const int ExpedicaoInterrompida = 23;
        public const int ExpedicaoEncerrada = 24;
        public const int OrdemFinalizada = 31;
        public const int OrdemCancelada = 32;
        public const int EmAuditoria = 33;
    }
public class StatusPallet
    {
        public const int Livre = 1;
        public const int Ocupado = 2;
        public const int Cheio = 3;
        public const int Manutencao = 4;
        public const int EmExpedicao = 5;
    }
public class StatusPedido
    {
        public const int Indefinido = 0;
        public const int Implantado = 1;
        public const int Produzido = 2;
        public const int Armazenado = 3;
        public const int Programado = 4;
        public const int Expedido = 5;
        public const int Finalizado = 6;
        public const int Cancelado = 7;
        public const int Congelado = 8;
    }
public class StatusTela
    {
        public const int Indefinido = 0;
        public const int Ativo = 1;
        public const int Inativo = 2;
    }
public class StatusWiFi
    {
        public const int Forte = 100;
        public const int Medio = 75;
        public const int Fraco = 25;
        public const int SemSinal = 1;
    }
public class TipoAgrupamento
    {
        public const int Indefinido = 0;
        public const int Box = 1;
        public const int ProdutoCorGrade = 2;
        public const int ProdutoCorTamanho = 3;
        public const int OrdemExportacao = 4;
        public const int BoxSku = 5;
    }
public class TipoAmbiente
    {
        public const int DESENVOLVIMENTO = 0;
        public const int TESTE = 1;
        public const int PRODUCAO = 2;
    }
public class TipoAreaDefault
    {
        public const int PADRAO = 1;
        public const int STAGEIN = 2;
        public const int STAGEOUT = 3;
    }
public class TipoAtribuicaoAutomatica
    {
        public const int Indefinido = 0;
        public const int QualquerOperador = 1;
        public const int MesmoOperador = 2;
    }
public class TipoCaixaLeitura
    {
        public const int Embalada = 1;
        public const int Sorter = 2;
        public const int Caracol = 3;
        public const int Expedicao = 4;
        public const int Auditada = 5;
        public const int EquipamentoBloqueado = 11;
        public const int CaracolIncorreto = 12;
        public const int PalletIndisponivel = 13;
        public const int CaixaNaoLidaPortal = 14;
        public const int LiberacaoSupervisor = 15;
        public const int ExpedicaoNaoEmbarcada = 16;
        public const int Retrabalhada = 17;
        public const int DescontarDoCaracol = 18;
        public const int EstufamentoConfirmado = 19;
        public const int CaixaJaExpedidaEmOutraOrdem = 20;
        public const int MesmaCaixaLidaIntervaloCincoSegundos = 50;
        public const int VoceDeveLerACaixaX = 51;
        public const int ConsistenciaDoBox = 52;
        public const int MaisDeUmPalletRegistrado = 53;
        public const int LeituraRetornouOk = 54;
        public const int AuditoriaIniciada = 55;
        public const int AuditoriaTerminada = 56;
        public const int DesvioDeCaixaParaAuditoria = 57;
        public const int EmbaladaPeloPortalF6 = 58;
        public const int CaixaSuspeitaDeDuplicata = 59;
    }
public class TipoContato
    {
        public const int ContatoNormal = 0;
        public const int ContatoParaSMS = 1;
    }
public class TipoEnderecoDefault
    {
        public const int PORTAPALLET = 1;
        public const int DOCA = 2;
        public const int SORTER = 3;
        public const int BUFFER = 4;
    }
public class TipoExecucaoMonitor
    {
        public const int Imediato = 0;
        public const int Agendada = 1;
    }
public class TipoExibicao
    {
        public const int SIAG = 1;
        public const int CD = 2;
        public const int Motorista = 3;
    }
public class TipoHierarquia
    {
        public const int Endereco = 1;
        public const int Equipamento = 2;
    }
public class TipoInterrupcao
    {
        public const int Temporaria = 1;
        public const int Definitiva = 2;
    }
public class TipoMercado
    {
        public const int MI = 1;
        public const int ME = 2;
        public const int DVE = 3;
    }
public class TipoOperadorDiarioBordo
    {
        public const int Indefinido = 0;
        public const int Orientacao = 1;
        public const int Advertencia = 2;
        public const int Outros = 3;
    }
public class TipoOrdem
    {
        public const int Indefinido = 0;
        public const int OrdemCarregamento = 1;
        public const int MovimentacaoInterna = 2;
        public const int Retrabalho = 3;
        public const int Auditoria = 4;
        public const int Saldo = 5;
        public const int EstoqueEstrategico = 6;
        public const int Transferencia = 7;
    }
public class TipoPalletProcessamento
    {
        public const int Previsto = 0;
        public const int Processado = 1;
    }
public class TipoParametro
    {
        public const int Inteiro = 1;
        public const int Decimal = 2;
        public const int Enumerador = 3;
        public const int Moeda = 4;
        public const int Texto = 5;
        public const int ListaDeTexto = 6;
        public const int Entidade = 7;
        public const int RadioButtons = 9;
    }
public class TipoPerformance
    {
        public const int Desempenho = 0;
        public const int Eficiencia = 1;
        public const int NASA = 2;
    }
public class TipoPreenchimento
    {
        public const int Desconhecido = 0;
        public const int Horizontal = 1;
        public const int Vertical = 2;
    }
public class TipoPrograma
    {
        public const int Indefinido = 0;
        public const int Normal = 1;
        public const int EstoqueEstrategico = 2;
        public const int ForaDeLinha = 3;
    }
public class TipoResultadoEficiencia
    {
        public const int GeralOperadores = 0;
        public const int GeralPorHora = 1;
        public const int OperadoresPorHora = 2;
    }
public class TipoRotina
    {
        public const int StoredProcedure = 1;
        public const int MetodoClasse = 2;
    }
public class TipoTela
    {
        public const int Indefinido = 0;
        public const int Dados = 1;
        public const int Mensagem = 2;
    }
public class TipoTurnoParada
    {
        public const int Indefinido = 0;
        public const int TRF = 1;
        public const int Refeicao = 2;
    }
public class TipoUsuario
    {
        public const int Funcionario = 0;
        public const int Transportadora = 1;
    }
public class TipoVisaoEmbarque
    {
        public const int Indefinido = 0;
        public const int Geral = 1;
        public const int Estabelecimento = 2;
        public const int Canal = 3;
        public const int EstabelecimentoCanal = 4;
        public const int Mercado = 5;
    }