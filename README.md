# SIAG CRATO

## APIs
| CaixaController                 | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| CarregarCaixasOrdem             | HttpGet     | long ordem
| ExpedirCaixas                   | HttpPost    | List<CaixasNaOrdemAPI> caixas
| EmbalarCaixas                   | HttpPost    | List<CaixaAEmbalar> caixas
| IncluirHistorico                | HttpPost    | string caixa, DateTime dataLeitura, TipoCaixaLeitura tipoCaixaLeitura, StatusCaixaLeitura statusCaixaLeitura

---

| ChamadaController               | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| CriarChamadasPorLado            | HttpGet     | long endereco, int lado, long ordem

---

| EquipamentoController           | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| ListaEquipamentosPorSetorModelo | HttpGet     | int setor, int modelo

---

| MotivoInterrupcaoController     | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| ListaEquipamentosPorSetorModelo | HttpGet     |

---

| MotoristaController             | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| MotoristaOrdem                  | HttpGet     | long ordem

---

| OperadorController              | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| VerificaOperador                | HttpGet     | long operador
| AtribuirHistoricoOperador       | HttpPost    | long operador, int endereco, EventoOperador motivo, DateTime dtEvento

---

| OrdemController                 | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| OrdensAlocadas                  | HttpGet     | int portao
| interromperOrdem                | HttpGet     | long ordem, int id_motivointerrupcao, string dt_interrupcao, int qtCaixas
| InsereOrdemSequencia            | HttpPost    | models.SequenciaTipoCargaOrdem sequenciaTipoCargaOrdem
| InsereOrdemSequencia            | HttpPost(/Ordem/v2/InsereOrdemSequencia)    | SequenciaTipoCargaOrdem sequenciaTipoCargaOrdem
| AlteraStatusOrdem               | HttpPost    | Ordem ordem

---

| PalletController                | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| LiberarPallet                   | HttpGet     | int pallet, long ordem, int portao

---

| ParametroController             | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| ValorParametro                  | HttpGet     | int id_parametro

---

| PortaoController                | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| VerificarPortao                 | HttpGet     | int id
| ListarPortoes                   | HttpGet     | int Setor

---

| UtilController                  | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| GetWebPageToImage               | HttpPost    | object data

---

| UtilController                  | Método      | Parâmetros
|---------------------------------|-------------|-------------|
| VeiculoOrdem                    | HttpPost    | long ordem
| IncluirVeiculo                  | HttpPost    | VeiculoAPI dados
| EditarVeiculo                   | HttpPost    | VeiculoAPI dados
| ExcluirVeiculo                  | HttpPost    | VeiculoAPI dados
| ContratarVeiculo                | HttpPost    | VeiculoAPI dados
| DescontratarVeiculo             | HttpPost    | Ordem dados

## Tabelas
| Tabela crud                     | Número de Linhas 
|---------------------------------|------------------
| caixa                           | 40.679.049       
| chamadatarefa                   | 15.868.400       
| chamada                         | 7.440.963        
| pedido                          | 6.849.191        
| programa                        | 5.242.580        
| ordemcarga                      | 2.565.822        
| enderecoarmazenagem             | 1.991.996        
| notafiscal                      | 1.712.639      
| agrupadorativo                  | 1.099.761        
| veiculotipocarga                | 371.715          
| previsaoveiculo                 | 105.189   
| cliente                         | 62.232           
| pallet                          | 31.299           
| areaarmazenagem                 | 30.342             
| ordem                           | 25.264           
| ordemsequencia                  | 21.235         
| ordeminterrupcao                | 20.715       
| veiculo                         | 18.316          
| motoristacontato                | 9.859      
| motorista                       | 9.463               
| operador                        | 2.766            
| monitorexecucao                 | 1.886       
| portletconfig                   | 1.861            
| portletativo                    | 1.317         
| transportadoratipocarga         | 581              
| metaembarque                    | 252                 
| equipamento                     | 184                    
| parametro                       | 92          
| equipamentoendereco             | 69                  
| endereco                        | 66               
| transportadora                  | 61               
| equipamentoatividade            | 60          
| preenchimento                   | 46     
| atividadeprioridade             | 39                              
| atividadeTarefa                 | 37        
| programacaotela                 | 29    
| equipamentoChecklist            | 28               
| uf                              | 27               
| atividade                       | 22      
| canal                           | 21     
| turnoParada                     | 21   
| tela                            | 15               
| motivoInterrupcao               | 13                 
| equipamentoModelo               | 9                
| operadorResponsavel             | 6                
| tempoatividade                  | 6                
| hierarquiaTurnosetor            | 5                
| tipoVeiculo                     | 5                
| setorTrabalho                   | 4                
| tipoendereco                    | 4                
| hierarquiaTurno                 | 3                
| tipoarea                        | 3                
| turno                           | 3                
| deposito                        | 2                
| atividadeRejeicao               | 1                
| turnoexcecao                    | 1                
| regiaotrabalho                  | 1       
| auditoria                       | 0                
| auditoriacaixa                  | 0                
| centrodecustos                  | 0                         

---

| Tabela log                      | Número de Linhas 
|---------------------------------|------------------
| logSIAG                         | 123.915          

---

| Tabelas usadas sistema Caracol  | Número de Linhas 
|---------------------------------|------------------
| caixaleitura                    | 151.437.816      
| logCaracol                      | 145.885.396      
| desempenho                      | 43.578.748       
| siaglog                         | 7.608.224        
| operadorhistorico               | 5.892.427        
| lidervirtual                    | 192.997          
| prioridadesareasarmazenagem     | 1.099            
| parametromensagemCaracol        | 28               
| posicaoCaracolRefugo            | 10               
| niveisAgrupadores               | 8                
| status_leitor                   | 4                  
| caixahistorico                  | 0              

---

| Tabelas usadas sistema Chamadas  | Número de Linhas 
|---------------------------------|------------------
| historicopallet                 | 7.584.244              
| tmp_transicaochamada            | 174        
| atividadeRotina                 | 35                   

---

| Tabelas sem uso definido        | Número de Linhas 
|---------------------------------|------------------
| historico                       | 5.782.885        
| equipamentochecklistoperador    | 1.088.705        
| ordemhistorico                  | 108.697
| visaoembarque_predatapordia     | 87.969        
| lote                            | 17.446          
| visaoembarque_predata           | 16.932           
| produto                         | 13.184
| visaoembarque_carregamento      | 4.418              
| visaoembarque_faturamento       | 3.662              
| fatia                           | 3.441      
| operadorDiariobordo             | 272               
| equipamentoStatus               | 17               
| convocacaoPrioridade            | 13                 
| portlet                         | 11           
| previsaoveiculousuario          | 11               
| utilitario                      | 10               
| ordemretrabalho                 | 0                
| ordemexportacao                 | 0                
| agendamento                     | 0                
| desempenhotrocacaracol          | 0                
| caixaimagem                     | 0              

---

| Tabelas não usadas API SIAG     | Número de Linhas 
|---------------------------------|------------------
| tmp_dadosclp                    | 75.395.657       
| caixatemp                       | 10.189.966       
| tmp_dadosclpautomatico          | 57.771           
| tmp_ezequiel                    | 106              
| tmp_acaosorter_historico        | 48               
| tmp_acaosorter_int              | 45               
| tmp_transicaochamadaBCK         | 18               
| tmp_ezequiel1                   | 0                
| tmp_acaosorter                  | 0      
|---------------------------------|------------------
| tmp_acaosorter_bkp_int          | 19      
|---------------------------------|------------------
| tmp_impcaixaprocesso            | 24               
| tmp_impagrupador                | 6        
|---------------------------------|------------------
| imp_caixa_historico_            | 63.073.103       
| imp_caixa_historico             | 28.782.483         
| imp_pedidopredata               | 111.235          
| imp_caixa_sum                   | 13.861           
| imp_caixa                       | 8.485            
| imp_pedido_baca                 | 4.147            
| imp_fatia                       | 3.310            
| imp_pedidocongelado             | 1.892            
| imp_visaoembarque_carregamento  | 278              
| imp_visaoembarque_faturamento   | 229              
| imp_pedido                      | 77               
| imp_caixa_ee_ajuste_status      | 69               
| imp_visaoembarque_predatapordia | 38               
| imp_programa                    | 33               
| imp_cliente                     | 26               
| imp_lote                        | 11               
| imp_visaoembarque_predata       | 5                
| imp_programa_dimensoes          | 0                
| imp_produto                     | 0                
| imp_caixa_gr_item               | 0                
| imp_pedido_remove_notafiscal    | 0                
| imp_caixa_ivp                   | 0                
| imp_notafiscal                  | 0       
|---------------------------------|------------------     
| imp_log_sucesso                 | 112.238          
| imp_log_falha                   | 111.176        
|---------------------------------|------------------    
| logSorterBack                   | 20.275.620       
| ms_sorter_log_proc              | 5.754.023        
| logSorterBackSick               | 5.290.681        
| ms_sorter_log                   | 3.213.397        
| logSorterErros                  | 1.814.185        
| logProcedure                    | 955.390          
| logPortalSorter                 | 952.108          
| fs_logconsultas1                | 171.430          
| logcaixaembalagem               | 33.413           
| logImportacao                   | 29.539           
| logprogramadocumento            | 18.915           
| logCLP                          | 11.348           
| logPedido                       | 655              
| logcaixaestufada                | 2                
| logtrocadecaracolchamada        | 0                
| logtrocadecaracolexecucao       | 0                
| logvisaoembarque                | 0                
| caixa_status_log                | 0                
| log_chamada_finalizada          | 0                
|---------------------------------|------------------    
| logSIAG_bkp                     | 19.671          
|---------------------------------|------------------          
| caixa_bkp                       | 11.939.059       
| caixa_bkp_Artur                 | 5.869.725        
| pedido_bkp                      | 4.121.124        
| pedido_bkp_Artur                | 3.428.309        
| notafiscal_bkp                  | 999.001          
|---------------------------------|------------------    
| caixaintegracao                 | 53.566.708       
| chamadatarefa_historico         | 9.742.909        
| chamada_historico               | 4.728.126        
| caixaleituraauditoria           | 3.862.433        
| caixadataembalagem              | 1.913.863        
| ordemInternaAux                 | 1.113.194        
| chamadadependencia              | 546.612          
| sorterfilacaixa                 | 446.923          
| chamadadependencia_historico    | 390.646          
| equipamentoineficiencia         | 150.050          
| embalagemrfid                   | 135.355          
| pedidopredata                   | 74.284           
| desempenhocaixa                 | 55.005           
| pedidocongelado                 | 19.175           
| alocacaoautomaticafatiaendereco | 12.414           
| desempenhoonline                | 3.694            
| caixadataestoqueestrategico     | 2.540            
| equipamentoManutencao           | 881              
| MotivoOrdem                     | 834              
| alocacaoautomaticafatia         | 611              
| sorterAcaoclp                   | 154              
| sortercaixascaracol             | 110              
| caixas_nao_faturadas            | 97               
| funcoessistema                  | 96               
| embalagemEquipamento            | 60               
| SolicitanteOrdem                | 54               
| usoDeAtividadePorModelo         | 27               
| equipamentoenderecoPrioridade   | 19               
| visaoembarque_carregamento_composicao | 12         
| fluxoFabrica                    | 9                
| tipoEventosSorter               | 5                
| USODEATIVIDADE                  | 2                
| gta_projeto                     | 0                
| caixadestino                    | 0                
| notafiscal_bck_ajuste_dt_embarque | 0              
| equipamentotroca                | 0             
| embalagemduplicada              | 0                 