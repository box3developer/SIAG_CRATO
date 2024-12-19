# SIAG CRATO

## APIs
| **Controller**                  | **Método**                      | **Verbo HTTP** | **Parâmetros**                                                                  
|---------------------------------|---------------------------------|----------------|---------------------------------------------------------------------------------        
| **CaixaController**             | CarregarCaixasOrdem             | HttpGet        | long ordem                                                                      
|                                 | ExpedirCaixas                   | HttpPost       | List<CaixasNaOrdemAPI> caixas                                                   
|                                 | EmbalarCaixas                   | HttpPost       | List<CaixaAEmbalar> caixas                                                     
|                                 | IncluirHistorico                | HttpPost       | string caixa, DateTime dataLeitura, TipoCaixaLeitura tipoCaixaLeitura, StatusCaixaLeiturastatusCaixaLeitura |
|                                 | CriarChamadasPorLado            | HttpGet        | long endereco, int lado, long ordem                                            
| **ChamadaController**           | CriarChamadasPorLado | HttpGet  | long endereco, int lado, long ordem                                                          
| **EquipamentoController**       | ListaEquipamentosPorSetorModelo | HttpGet        | int setor, int modelo                                                          
| **MotivoInterrupcaoController** | ListaEquipamentosPorSetorModelo | HttpGet        |                                                                                 
| **MotoristaController**         | MotoristaOrdem                  | HttpGet        | long ordem                                                                      
| **OperadorController**          | VerificaOperador                | HttpGet        | long operador                                                                  
|                                 | AtribuirHistoricoOperador       | HttpPost       | long operador, int endereco, EventoOperador motivo, DateTime dtEvento                  
| **OrdemController**             | OrdensAlocadas                  | HttpGet        | int portao                                                                      
|                                 | interromperOrdem                | HttpGet        | long ordem, int id_motivointerrupcao, string dt_interrupcao, int qtCaixas      
|                                 | InsereOrdemSequencia            | HttpPost       | models.SequenciaTipoCargaOrdem sequenciaTipoCargaOrdem                         
|                                 | InsereOrdemSequencia            | HttpPost(/Ordem/v2/InsereOrdemSequencia) | SequenciaTipoCargaOrdem sequenciaTipoCargaOrdem       
|                                 | AlteraStatusOrdem               | HttpPost       | Ordem ordem                                                                     
| **PalletController**            | LiberarPallet                   | HttpGet        | int pallet, long ordem, int portao                                             
| **ParametroController**         | ValorParametro                  | HttpGet        | int id_parametro                                                               
| **PortaoController**            | VerificarPortao                 | HttpGet        | int id                                                                         
|                                 | ListarPortoes                   | HttpGet        | int Setor                                                                              
| **UtilController**              | GetWebPageToImage               | HttpPost       | object data                                                                    
| **VeiculoController**           | VeiculoOrdem                    | HttpPost       | long ordem                                                                     
|                                 | IncluirVeiculo                  | HttpPost       | VeiculoAPI dados                                                               
|                                 | EditarVeiculo                   | HttpPost       | VeiculoAPI dados                                                               
|                                 | ExcluirVeiculo                  | HttpPost       | VeiculoAPI dados                                                               
|                                 | ContratarVeiculo                | HttpPost       | VeiculoAPI dados                                                               
|                                 | DescontratarVeiculo             | HttpPost       | Ordem dados                                                                                                                                                                                                                                 

---

## Tabelas
|Classificação tabela             | Nome                            | Número de Linhas 
|---------------------------------|---------------------------------|------------------
| **Tabela crud**                 | caixa                           | 40.679.049       
|                                 | chamadatarefa                   | 15.868.400       
|                                 | chamada                         | 7.440.963        
|                                 | pedido                          | 6.849.191        
|                                 | programa                        | 5.242.580        
|                                 | ordemcarga                      | 2.565.822        
|                                 | enderecoarmazenagem             | 1.991.996        
|                                 | notafiscal                      | 1.712.639      
|                                 | agrupadorativo                  | 1.099.761        
|                                 | veiculotipocarga                | 371.715          
|                                 | previsaoveiculo                 | 105.189   
|                                 | cliente                         | 62.232           
|                                 | pallet                          | 31.299           
|                                 | areaarmazenagem                 | 30.342             
|                                 | ordem                           | 25.264           
|                                 | ordemsequencia                  | 21.235         
|                                 | ordeminterrupcao                | 20.715       
|                                 | veiculo                         | 18.316          
|                                 | motoristacontato                | 9.859      
|                                 | motorista                       | 9.463               
|                                 | operador                        | 2.766            
|                                 | monitorexecucao                 | 1.886       
|                                 | portletconfig                   | 1.861            
|                                 | portletativo                    | 1.317         
|                                 | transportadoratipocarga         | 581              
|                                 | metaembarque                    | 252                 
|                                 | equipamento                     | 184                    
|                                 | parametro                       | 92          
|                                 | equipamentoendereco             | 69                  
|                                 | endereco                        | 66               
|                                 | transportadora                  | 61               
|                                 | equipamentoatividade            | 60          
|                                 | preenchimento                   | 46     
|                                 | atividadeprioridade             | 39                              
|                                 | atividadeTarefa                 | 37        
|                                 | programacaotela                 | 29    
|                                 | equipamentoChecklist            | 28               
|                                 | uf                              | 27               
|                                 | atividade                       | 22      
|                                 | canal                           | 21     
|                                 | turnoParada                     | 21   
|                                 | tela                            | 15               
|                                 | motivoInterrupcao               | 13                 
|                                 | equipamentoModelo               | 9                
|                                 | operadorResponsavel             | 6                
|                                 | tempoatividade                  | 6                
|                                 | hierarquiaTurnosetor            | 5                
|                                 | tipoVeiculo                     | 5                
|                                 | setorTrabalho                   | 4                
|                                 | tipoendereco                    | 4                
|                                 | hierarquiaTurno                 | 3                
|                                 | tipoarea                        | 3                
|                                 | turno                           | 3                
|                                 | deposito                        | 2                
|                                 | atividadeRejeicao               | 1                
|                                 | turnoexcecao                    | 1                
|                                 | regiaotrabalho                  | 1       
|                                 | auditoria                       | 0                
|                                 | auditoriacaixa                  | 0                
|                                 | centrodecustos                  | 0                         
| **Tabela log**                  | logSIAG                         | 123.915          
| **Tabelas usadas sistema Caracol** | caixaleitura                    | 151.437.816      
|                                 | logCaracol                      | 145.885.396      
|                                 | desempenho                      | 43.578.748       
|                                 | siaglog                         | 7.608.224        
|                                 | operadorhistorico               | 5.892.427        
|                                 | lidervirtual                    | 192.997          
|                                 | prioridadesareasarmazenagem     | 1.099            
|                                 | parametromensagemCaracol        | 28               
|                                 | posicaoCaracolRefugo            | 10               
|                                 | niveisAgrupadores               | 8                
|                                 | status_leitor                   | 4                  
|                                 | caixahistorico                  | 0              
| **Tabelas usadas sistema Chamadas** | istoricopallet                 | 7.584.244              
|                                 | tmp_transicaochamada            | 174        
|                                 | atividadeRotina                 | 35                   
| **Tabelas sem uso definido**    | historico                       | 5.782.885        
|                                 | equipamentochecklistoperador    | 1.088.705        
|                                 | ordemhistorico                  | 108.697
|                                 | visaoembarque_predatapordia     | 87.969        
|                                 | lote                            | 17.446          
|                                 | visaoembarque_predata           | 16.932           
|                                 | produto                         | 13.184
|                                 | visaoembarque_carregamento      | 4.418              
|                                 | visaoembarque_faturamento       | 3.662              
|                                 | fatia                           | 3.441      
|                                 | operadorDiariobordo             | 272               
|                                 | equipamentoStatus               | 17               
|                                 | convocacaoPrioridade            | 13                 
|                                 | portlet                         | 11           
|                                 | previsaoveiculousuario          | 11               
|                                 | utilitario                      | 10               
|                                 | ordemretrabalho                 | 0                
|                                 | ordemexportacao                 | 0                
|                                 | agendamento                     | 0                
|                                 | desempenhotrocacaracol          | 0                
|                                 | caixaimagem                     | 0              
| **Tabelas não usadas API SIAG** | tmp_dadosclp                    | 75.395.657       
|                                 | caixatemp                       | 10.189.966       
|                                 | tmp_dadosclpautomatico          | 57.771           
|                                 | tmp_ezequiel                    | 106              
|                                 | tmp_acaosorter_historico        | 48               
|                                 | tmp_acaosorter_int              | 45               
|                                 | tmp_transicaochamadaBCK         | 18               
|                                 | tmp_ezequiel1                   | 0                
|                                 | tmp_acaosorter                  | 0       
|                                 |                                 | 
|                                 | tmp_acaosorter_bkp_int          | 19      
|                                 |                                 |
|                                 | tmp_impcaixaprocesso            | 24               
|                                 | tmp_impagrupador                | 6     
|                                 |                                 |   
|                                 | imp_caixa_historico_            | 63.073.103       
|                                 | imp_caixa_historico             | 28.782.483         
|                                 | imp_pedidopredata               | 111.235          
|                                 | imp_caixa_sum                   | 13.861           
|                                 | imp_caixa                       | 8.485            
|                                 | imp_pedido_baca                 | 4.147            
|                                 | imp_fatia                       | 3.310            
|                                 | imp_pedidocongelado             | 1.892            
|                                 | imp_visaoembarque_carregamento  | 278              
|                                 | imp_visaoembarque_faturamento   | 229              
|                                 | imp_pedido                      | 77               
|                                 | imp_caixa_ee_ajuste_status      | 69               
|                                 | imp_visaoembarque_predatapordia | 38               
|                                 | imp_programa                    | 33               
|                                 | imp_cliente                     | 26               
|                                 | imp_lote                        | 11               
|                                 | imp_visaoembarque_predata       | 5                
|                                 | imp_programa_dimensoes          | 0                
|                                 | imp_produto                     | 0                
|                                 | imp_caixa_gr_item               | 0                
|                                 | imp_pedido_remove_notafiscal    | 0                
|                                 | imp_caixa_ivp                   | 0                
|                                 | imp_notafiscal                  | 0       
|                                 |                                 |
|                                 | imp_log_sucesso                 | 112.238          
|                                 | imp_log_falha                   | 111.176        
|                                 |                                 |
|                                 | logSorterBack                   | 20.275.620       
|                                 | ms_sorter_log_proc              | 5.754.023        
|                                 | logSorterBackSick               | 5.290.681        
|                                 | ms_sorter_log                   | 3.213.397        
|                                 | logSorterErros                  | 1.814.185        
|                                 | logProcedure                    | 955.390          
|                                 | logPortalSorter                 | 952.108          
|                                 | fs_logconsultas1                | 171.430          
|                                 | logcaixaembalagem               | 33.413           
|                                 | logImportacao                   | 29.539           
|                                 | logprogramadocumento            | 18.915           
|                                 | logCLP                          | 11.348           
|                                 | logPedido                       | 655              
|                                 | logcaixaestufada                | 2                
|                                 | logtrocadecaracolchamada        | 0                
|                                 | logtrocadecaracolexecucao       | 0                
|                                 | logvisaoembarque                | 0                
|                                 | caixa_status_log                | 0                
|                                 | log_chamada_finalizada          | 0        
|                                 |                                 |
|                                 | logSIAG_bkp                     | 19.671    
|                                 |                                 |
|                                 | caixa_bkp                       | 11.939.059       
|                                 | caixa_bkp_Artur                 | 5.869.725        
|                                 | pedido_bkp                      | 4.121.124        
|                                 | pedido_bkp_Artur                | 3.428.309        
|                                 | notafiscal_bkp                  | 999.001   
|                                 |                                 |
|                                 | caixaintegracao                 | 53.566.708       
|                                 | chamadatarefa_historico         | 9.742.909        
|                                 | chamada_historico               | 4.728.126        
|                                 | caixaleituraauditoria           | 3.862.433        
|                                 | caixadataembalagem              | 1.913.863        
|                                 | ordemInternaAux                 | 1.113.194        
|                                 | chamadadependencia              | 546.612          
|                                 | sorterfilacaixa                 | 446.923          
|                                 | chamadadependencia_historico    | 390.646          
|                                 | equipamentoineficiencia         | 150.050          
|                                 | embalagemrfid                   | 135.355          
|                                 | pedidopredata                   | 74.284           
|                                 | desempenhocaixa                 | 55.005           
|                                 | pedidocongelado                 | 19.175           
|                                 | alocacaoautomaticafatiaendereco | 12.414           
|                                 | desempenhoonline                | 3.694            
|                                 | caixadataestoqueestrategico     | 2.540            
|                                 | equipamentoManutencao           | 881              
|                                 | MotivoOrdem                     | 834              
|                                 | alocacaoautomaticafatia         | 611              
|                                 | sorterAcaoclp                   | 154              
|                                 | sortercaixascaracol             | 110              
|                                 | caixas_nao_faturadas            | 97               
|                                 | funcoessistema                  | 96               
|                                 | embalagemEquipamento            | 60               
|                                 | SolicitanteOrdem                | 54               
|                                 | usoDeAtividadePorModelo         | 27               
|                                 | equipamentoenderecoPrioridade   | 19               
|                                 | visaoembarque_carregamento_composicao | 12         
|                                 | fluxoFabrica                    | 9                
|                                 | tipoEventosSorter               | 5                
|                                 | USODEATIVIDADE                  | 2                
|                                 | gta_projeto                     | 0                
|                                 | caixadestino                    | 0                
|                                 | notafiscal_bck_ajuste_dt_embarque | 0              
|                                 | equipamentotroca                | 0             
|                                 | embalagemduplicada              | 0                 
                                                     

---

## Procedures
|Nome
|---------------------------------
| **Procedures Usadas no GTA**       
| sp_sorter_confirmaleituraportal
| sp_sorter_confirmaleituraportal_V0_04042019
| sp_sorter_destinocaixa
| sp_sorter_destinocaixa_desativado
| sp_sorter_destinocaixa_TESTE_REGRA_ALOC
| sp_sorter_desalocaagrupadoressempendencia
| sp_sorter_acaosorterclp
| sp_sorter_acaosorterclp_bkp08062022
| sp_sorter_acaosorterclp_BKP_20042022_1541
| sp_gta_expurgodados
| sp_siag_gestaovisual_gravaperformance_online
| sp_sorter_confirmapalletcheio
| sp_sorter_existecaixapendente
| sp_sorter_confirmaleituracaracol
| sp_sorter_confirmaleituracaracol_bkp_26072022
| sp_sorter_leituracaracol
| sp_sorter_leituracaracol_v19052021
| sp_sorter_leituracaracol_OLD
| sp_sorter_leituracaracol_V0
| sp_siag_expedicaocontrolaordensinternas
| sp_siag_expedicaocontrolaordensinternas_BKP_09-10-24
| **Procedures Usadas Sistemas Chamadas**
| sp_siag_alocacaoautomaticabilaterais
| sp_get_checklist_equipamento
| sp_rotina_retornastageinlivre
| sp_siag_atualizaequipamento
| sp_siag_busca_qtde_pallets
| sp_siag_buscaarealivre
| sp_siag_criachamada
| sp_siag_criachamada_BKP_09-10-24
| sp_siag_destinopallet
| sp_siag_destinopallet_ALLAN_29032019
| sp_siag_destinopallet_V0
| sp_siag_destinopallet_V1
| sp_siag_finalizachamada
| sp_siag_finalizachamada_bkp_16052022
| sp_siag_loginoperador
| sp_siag_loginoperador_bkp_12082022
| sp_siag_logoffoperador
| sp_siag_performanceonline
| sp_siag_reiniciachamada
| sp_siag_rejeitachamada
| sp_siag_selecionachamada
| sp_siag_selecionachamada_BKP_21-10-24
| **Procedures Usadas Sistemas Caracol**
| sp_siag_gestaovisual_gravaperformance
| sp_siag_criachamada
| sp_siag_criachamada_BKP_09-10-24
| **Procedures com uso desconhecido **
| sp_portoes_ExpedirCaixa
| sp_siag_importa_dados
| sp_Hygor_ObterHistoricoPallets
| sp_supervisoriomaquina_tempo_medio_operacao
| sp_supervisoriomaquina_movimentacao_pallets
| sp_supervisoriomaquina_improdutividade_Empilhadeira
| sp_supervisoriomaquina_movimentacao_pallets_acumulado_mes
| SIAGTranspaleteira_MovimentacaoPallets_Acumulado_Mes
| sp_supervisoriomaquina_status_maquina
| sp_supervisoriomaquina_atividades_troca_corredores
| sp_supervisoriomaquina_login_logoff
| sp_supervisoriomaquina_tempos_movimento
| sp_supervisoriomaquina_atividade_nivel
| sp_supervisoriomaquina_eficiencia_por_hora
| sp_supervisoriomaquina_eficiencia_crono
| sp_supervisoriomaquina_eficiencia_carga
| sp_supervisoriomaquina_operacoes_produtivas
| sp_supervisoriomaquina_perdas_maquinas
| sp_siag_impressaoordemV2
| sp_relatorio_setores_ordem_agrupador
| sp_siag_impressaoordem_artur
| SIAGBilateral_estimadoErealizado
| sp_supervisoriomaquina_estimadoErealizado
| SIAGTranspaleteira_TempoMedioOpreacao
| SIAGTranspaleteira_StatusEquipamentos
| SIAGTranspaleteira_AtividadePorTrocaDeAvenidas
| SIAGTranspaleteira_MovimentacaoPallets
| SIAGTranspaleteira_LoginELogoff
| SIAGTranspaleteira_TempoMedioDeOperacao
| SIAGTranspaleteira_atividade_nivel
| SIAGTranspaleteira_EficienciaHoraAHora
| SIAGTranspaleteira_eficiencia_crono
| SIAGTranspaleteira_Eficiencia
| SIAGTranspaleteira_Improdutividade
| SIAGTranspaleteira_PerdaDaMaquina
| SIAGTranspaleteira_PrevistoERealizado
| SIAGEmpilhadeira_StatusDoEquipamento
| SIAGEmpilhadeira_AtividadePorTrocaDeAvenidas
| SIAGEmpilhadeira_MovimentosPalletsMes
| SIAGEmpilhadeira_LoginELogoff
| SIAGEmpilhadeira_TemposDeMovimentos
| SIAGEmpilhadeira_atividade_nivel
| SIAGEmpilhadeira_EficienciaHoraAHora
| SIAGEmpilhadeira_eficiencia_crono
| SIAGEmpilhadeira_Eficiencia
| SIAGEmpilhadeira_Improdutividade
| SIAGEmpilhadeira_PerdaDaMaquina
| SIAGEmpilhadeira_PrevistoERealizado
| sp_siag_relatorio_entresetoresporagrupador
| SIAGBilateral_status_maquinas
| SIAGBilateral_atividades_troca_corredores
| SIAGBilateral_movimentacao_pallets_acumulado_mes
| SIAGBilateral_movimentacao_pallets
| SIAGBilateral_login_logoff
| SIAGBilateral_tempos_movimento
| sp_siag_relatorio_carregamentodetalhado
| sp_siag_relatorio_carregamentodetalhado_BKP
| SIAGBilateral_atividade_nivel
| SIAGBilateral_eficiencia_por_hora
| SIAGBilateral_operacoes_produtivas
| SIAGBilateral_eficiencia_crono
| SIAGBilateral_eficiencia_carga
| SIAGBilateral_perdas_maquinas
| sp_siag_impressaoordem
| sp_siag_lista_carga_pendente
| sp_ms_sorter_destinocaixa
| sp_siag_consulta_log_caracol_incorreto_hygor
| sp_siag_verificainconsistenciaordem
| sp_siag_verificainconsistenciaordem_BKP_02-07-24
| sp_siag_palletsarmazenados
| sp_siag_palletsarmazenados_BKP_28-06-24
| sp_InsertIntoLogErros
| sp_ms_sorter_destinocaixa_bkp04062024
| sp_siag_consulta_log_caracol_incorreto
| sp_siag_consulta_log_caracol_incorreto_bkp03062024
| sp_siag_desalocareserva
| sp_siag_calcula_nao_faturados_ordem
| sp_siag_importa_dados_caixa
| sp_siag_importa_dados_notafiscal
| sp_siag_importa_dados_notafiscal_02052024
| sp_siag_importa_dados_caixa_02052024
| sp_supervisoriomaquina_status_maquina_BKP_08-04-2024
| sp_siag_impressaoordem_hygor
| sp_siag_insere_caixas_nao_faturadas
| sp_siag_impressaoordem_bkp_27-03-24
| sp_InsertIntoSorterLogProc
| sp_siag_importa_dados_pedido
| sp_siag_processa_pedidos_800
| sp_siag_alocacaoautomaticalotes
| sp_portoes_ExpedirCaixa_n_encerrar_exp_nf_canc
| sp_portoes_ExpedirCaixa_BKP_20022024
| sp_rfid_marca_programa
| sp_siag_telao_equipamentobilateral
| sp_sorter_consultaarealivre_regra8
| sp_sorter_consultaarealivre_regra8_teste
| sp_siag_expedicaocontrolaordens
| sp_siag_importa_dados_pedido_bkp_21122023
| sp_siag_impressaoordem_
| sp_siag_expedicaocontrolaordens_bkp_23112023
| sp_portoes_interrompeexpedicao
| sp_util_posicoessempendenciasorter
| sp_portoes_LiberarPallet
| sp_siag_expedicaolimpapallet
| sp_siag_relatorio_entresetoresporagrupador_bkp10102023
| sp_siag_alocacaoautomaticalotes_bkp06102023_1413
| sp_ms_sorter_confirmaleituraportal
| sp_ms_sorter_log
| sp_siag_listapedidopendente
| sp_siag_importa_dados_caixa_agrupador_programa
| sp_siag_importa_dados_caixa_agrupador_programa_bkp21092023
| sp_siag_importa_dados_caixa_bkp21092023
| sp_rfid_volta_caixa
| sp_sorter_desalocacaopornivelagrupador
| sp_sorter_consultaarealivre_regra8_v2
| sp_sorter_consultaarealivre_regra8_bkp12072023
| sp_sorter_consultaarealivre
| sp_siag_importa_dados_bkp06062023
| sp_siag_importa_dados_bkp16052023
| sp_siag_importa_dados_programa
| sp_siag_importa_dados_programa_bkp_17042023
| sp_sorter_acaosorter
| sp_siag_listapedidopendente_v3
| sp_siag_listacargaspendentes
| sp_siag_listacargasporordem
| sp_siag_listapedidopendente_bkp_25112022
| SP_SUPERVISORIOMAQUINA_TEMPODEOPERACAO
| sp_rotina_leiturabuffer
| LogIneficienciaTranspaleteira
| sp_portoes_ExpedirCaixa_bkp_16092022_1650
| sp_siag_integracao_pedidos_qualidade
| SIAGEmpilhadeira_MovimentosPalletsMes_Acumulado_Mes
| sp_supervisoriomaquina_previsto_realizado
| sp_siag_listapedidopendente_proposta_cd_nf_p_cd_pedido
| sp_siag_listapedidopendente_bkp_30062022_08-28
| sp_siag_liberachamadasdependentes_recursivo
| sp_siag_liberachamadasdependentes
| sp_siag_criachamadasdependentes
| sp_siag_criachamadasdependentes_bkp_16052022
| sp_siag_importa_dados_pedido+bkp_03_05_2022_09_43
| sp_siag_criachamada_20-04-2022_BOX3
| sp_portoes_ExpedirCaixa_BKP_06-04-2022
| sp_portoes_ExpedirCaixa_Ver_gera_2_lados_def
| sp_siag_expedicaocaixa_bkp_09_03_2022_13_50
| sp_siag_importa_dados_BKP_28072021_1022
| sp_siag_enviaremailordemconcluida
| sp_siag_importa_dados_pedido_V25_02_2021_11HS
| sp_siag_consultacaixasinconsistentes
| sp_siag_consultacaixasinconsistentes_V1
| sp_sorter_destinocaixa_bkp_20201223
| sp_siag_expedicaoabastecepallets
| sp_siag_expedicaoabastecepallets_seq_OK_09072020
| sp_siag_expedicaoabastecepallets_OFI_V3_09072020
| sp_siag_expedicaoabastecepallets_com_sequencia_v1-07072020
| sp_siag_impressaoordem_ofi_V1_07072020
| sp_siag_expedicaoabastecepallets_V2
| sp_siag_importa_dados_caixa_020720200805
| sp_siag_expedicaoabastecepallets_OFI_27012020
| sp_siag_listapedidopendente_v240120
| sp_siag_importa_dados_caixa_agrupador
| sp_siag_criaagrupadorsku
| sp_siag_criaagrupadorsku_V3
| sp_portoes_ExpedirCaixa_V0
| sp_rotina_leituradoca
| sp_rotina_defineexpedicao
| sp_rotina_armazenapallet
| sp_rotina_verificadesalocapalletorigem
| sp_rotina_leiturastagein
| sp_rotina_alocapallet
| sp_rotina_alocapalletendereco
| sp_rotina_alocapalletendereco_V0
| sp_rotina_alocapalletendereco_V1
| sp_rotina_alocapalletlivre
| sp_rotina_alocapalletlivre_V1
| sp_rotina_alocapalletlivre_V0
| sp_rotina_verificadesalocapalletorigem_V0
| sp_rotina_verificadesalocapalletorigem_V1
| sp_siag_criaagrupadorpedido
| sp_siag_import_dados_corrigeagrupadorsku123
| sp_siag_consultapalletsdoca
| sp_prioridade_sequenciaincorretatipocarga
| sp_siag_expedicaocontrolaordens_V0
| sp_siag_expedicaocontrolaordens_V1
| sp_siag_importa_dados_lote
| sp_siag_importa_dados_fatias
| sp_siag_importa_dados_caixa_V3
| sp_siag_importa_dados_cliente
| sp_siag_importa_dados_produto
| sp_siag_gestaovisual_consultasorterprevisao
| sp_siag_gestaovisual_consultasorterprevisao_OLD
| sp_siag_relatorio_entresetores
| sp_siag_relatorio_entresetoresporagrupador_interno
| sp_gta_telacaracol
| sp_siag_impressaoromaneio
| sp_siag_existeromaneio
| sp_siag_sequenciaexpedicao
| sp_siag_criaagrupadorsku_V2
| sp_siag_importa_dados_notafiscal_v1
| sp_siag_impressaoromaneio_V1
| sp_siag_impressaoromaneio_V0
| sp_siag_expedicaoabastecepallets_V1
| sp_siag_expedicaoabastecepallets_V0
| sp_siag_existeromaneio_V1
| sp_siag_existeromaneio_V0
| sp_siag_importa_dados_caixa_V2
| sp_siag_criaagrupadorpedido_V2
| sp_siag_importa_dados_caixa_agrupador_V2
| sp_sorter_confirmacaixaestufada
| sp_ajuste_trocapalletquebrado
| sp_EmbalarCaixa
| sp_siag_importa_dados_caixa_V0
| sp_siag_importa_dados_cliente_V0
| sp_siag_importa_dados_lote_V0
| sp_siag_importa_dados_notafiscal_V0
| sp_siag_importa_dados_pedido_V0
| sp_siag_importa_dados_programa_V0
| sp_siag_importa_dados_produto_V0
| sp_siag_importa_dados_V0
| sp_siag_importa_dados_caixa_agrupador_programa_V0
| sp_siag_gestaovisual_performancecarregamento
| sp_siag_gestaovisual_previsaoveiculo
| sp_siag_gestaovisual_previsaoveiculo_V0
| sp_siag_gestaovisual_previsaoveiculo_V1
| sp_siag_gestaovisual_previsaoveiculo_salvar
| sp_siag_tempochamadasoperador
| sp_siag_lotesatribuidos_V0
| sp_siag_lotesatribuidos_V1
| sp_siag_lotesatribuidos
| sp_siag_performanceoperador
| sp_siag_consultapalletsordem
| sp_sorter_consultaarealivre_regra7
| sp_siag_consultapalletsordem_V1
| sp_siag_consultapalletsordem_V0
| sp_siag_importa_dados_caixa_agrupador_V1
| sp_siag_importa_dados_caixa_V1
| sp_siag_criaagrupadorsku_V1
| sp_siag_criaagrupadorpedido_V1
| sp_siag_importa_dados_caixa_agrupador_V0
| sp_siag_criaagrupadorsku_V0
| sp_siag_criaagrupadorpedido_V0
| sp_siag_expedicaodesalocacaixa
| sp_siag_finalizaordem
| sp_siag_erroclassificacao
| sp_siag_atualizastatuscaixasee
| sp_siag_caixaeeajustestatus
| sp_siag_caixadataestoqueestrategico
| sp_siag_performanceoperador_hora
| sp_siag_performanceoperador_hora_media
| sp_sorter_trocadecaracol
| sp_siag_habilitacaracol
| sp_siag_desabilitacaracol
| sp_siag_performanceoperador_sumarizado
| sp_siag_performanceoperador_sumarizado_media
| Sp_reindex_all_tablesIndex
| sp_siag_performanceoperador_geral
| sp_siag_performanceoperador_geral_media
| fs_gta_registra_opensession
| sp_rotina_leiturabuffer_ordemtransferencia
| sp_siag_acompanhamentocarregamento
| sp_rotina_leituradoca_ordeminterna
| fs_opensessions
| fs_runningstatements
| fs_findlocks
| fs_indexfragmentation
| sp_siag_gestaovisual_consultaarmazemporhora
| sp_siag_gestaovisual_consultasorterporhora
| sp_siag_gestaovisual_consultaperformancearmazem
| sp_siag_expedicaoretrabalhacaixa
| sp_siag_gestaovisual_trocacaracolmapa
| sp_siag_gestaovisual_trocacaracolresumo
| sp_siag_gestaovisual_trocacaracolandamento
| sp_siag_gestaovisual_trocacaracolfinalizada
| sp_siag_tempologadooperador
| sp_siag_importa_dados_visaoembarque
| sp_siag_tempomovimentooperador
| sp_sorter_tempodeslocamentocaracol
| sp_sorter_existepalletgaiola
| sp_sorter_inserefilacaracol
| sp_sorter_consultaarealivre_regra4
| sp_sorter_consultaarealivre_regra5
| sp_sorter_consultaarealivre_regra6
| sp_sorter_removefilacaixa
| sp_sorter_descontacaixacaracol
| sp_sorter_consultaarealivre_regra2
| sp_sorter_consultaarealivre_regra3
| sp_siag_lotepiloto
| sp_sorter_consultaarealivre_regra1
| sp_sorter_trocadecaracolfinalizar
| sp_siag_desempenhotrocacaracol
| sp_siag_gestaovisual_consultasorteroperadoreslogados
| fs_query_stats
| sp_siag_gestaovisual_gravaperformance_trocadecaracol
| sp_siag_gestaovisual_consultasorterocupacao
| sp_siag_gestaovisual_consultaperformancesorter
| sp_siag_gestaovisual_consultaarmazemprevisao
| sp_sorter_validalogin
| sp_siag_importa_dados_visaoembarque_predatapordia
| sp_sorter_inserecaixascaracol
| sp_prioridade_caracolcheio
| sp_sorter_trocadecaracolverificar
| sp_rotina_cargasorter
| sp_siag_telao_equipamentobilateralresumo
| sp_rotina_performancesumarizadados
| sp_rotina_expurga_leituras_caixas
| sp_util_desbloqueiachamada
| sp_prioridade_tarefadocaemandamento
| sp_siag_cancelarchamada
| sp_siag_selecionapalletsestoque
| sp_siag_insereimagem
| sp_qualidade_pedido_caixas
| sp_qualidade_pedidos_clientes_especiais
| sp_siag_gestaovisual_mensagemmotorista
| sp_siag_visaoembarque
| sp_siag_gestaovisual_consultaPortaPalletStageOut
| sp_siag_gestaovisual_consultaPortaPalletStageIn
| st_siag_tempologadooperador
| sp_siag_consultaparesprograma
| sp_siag_importa_dados_pedidocongelado
| sp_util_reiniciagta
| sp_siag_importa_dados_predata
| sp_siag_importa_dados_visaoembarque_predata
| sp_siag_consultaimagenscaixasordem
| sp_siag_alterarordeminterrupcao
| sp_siag_consultaparesarmazenados
| sp_siag_consultapalletsordeminterna
| sp_siag_buscaordemativaendereco
| sp_siag_relatorio_pedidosentresetores
| sp_util_consulta_pallets_stagein_sem_chamadas
| sp_util_criar_chamada_pallet_stageIn
| sp_util_ordens_com_interrupcao
| sp_util_inconsistencias
| sp_prioridade_nenhumatarefadoca
| sp_util_criachamadaposicaolivresorter
| sp_util_posicoesvaziassorter
| sp_util_consulta_reserva_indevida
| sp_siag_expedicaosequenciatipocarga
| sp_prioridade_enderecodestinoocupado
| sp_siag_criachamadasuspensa
| sp_siag_prioridadechamada
| sp_siag_agrupadorpedido
| sp_rotina_alocapalletsorter
| sp_prioridade_stageoutocupado
| sp_prioridade_stageinocupado
| sp_siag_calculareserva
| sp_rotina_definestageout
| sp_siag_expedicaocaixa
| sp_siag_verificaordemmista
| sp_sorter_trocadecaracolverificar_WILL
| sp_siag_performanceoperadorgrafico
| sp_util_chamadasdependenteserradas
| sp_insere_equipamentoStatus
| stp_verifica_ordem_mista
| stp_modifica_utilizacao_lados_doca
| sp_siag_modificaladodoca
| sp_siag_chamadasfinalizadashora
| sp_prioridade_abastecimentodoca
| sp_rotina_validaenderecoexatodestino
| sp_portlet_chamadaequipamento
| sp_util_reabrebox
| sp_siag_ativachamada
| sp_siag_suspendechamada
| sp_siag_ordemativaendereco
| spUtil_ReIndexDatabase_UpdateStats
| sp_siag_expedicaochamapallets
| sp_prioridade_ordemcarregamento
| sp_siag_expedicaorecolhepallets
| sp_prioridade_destravaoutroendereco
| sp_siag_expedicaofinalizaordem
| sp_siag_interrompeexpedicao
| sp_prioridade_ordemcarregamentoprioritaria
| sp_prioridade_equipamentoultimoendereco
| sp_rotina_validaenderecoexatoorigem
| sp_sorter_registrahistorico
| sp_rotina_reservaareadestino
| sp_siag_expedicaolimpaendereco
| sp_sorter_codificaendereco
| sp_ft
| sp_prioridade_sempreok
| sp_rotina_sempreok
| sp_rotina_redefinepalletdestino
| sp_rotina_desalocaendereco
| sp_rotina_validaenderecoorigem
| sp_rotina_desalocapallet
| sp_rotina_redefineareaorigem
| sp_rotina_removeenderecopallet
| sp_rotina_atribuienderecopallet
| sp_rotina_validapalletorigem
| sp_rotina_validapalletlivre
| sp_rotina_validapalletdestino
| sp_rotina_validaenderecodestino
| sp_rotina_testaleiturapallet
| sp_rotina_testaleituraendereco
| sp_rotina_atribuipalletorigem
| sp_rotina_atribuipalletlivre
| sp_rotina_atribuipalletdestino
| sp_rotina_atribuienderecoorigem
| sp_rotina_atribuienderecodestino
| sp_siag_agrupador
| sp_rotina_agendaTarefa
