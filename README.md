# GTA SIAG SOBRAL

## Caracol

### Inicio
1. Controle de chamadas START
   1. temporizador
   2. exec sp_siag_expedicaocontrolaordens
   3. SMS
   4. Email Transportadora
    OBS: DESATIVADO! Quando publicar o SIAG com o handler chamado aqui, remover a condição que impede de executar
2. Caracol START
   1. Recebe Leitura Caracol (Socket)
   2. Caracol + Leitura
   3. Transforma Registro em entidade
      1. exec sp_sorter_leituracaracol ':caracol', ':leitura'
   4. Resultado
   5. Campo Posição Destino
   6. Gaiola
   7. Envia Gaiola Destino
   8. atribui o valor da leitura para a entidade
   9. registra a caixa
      1. exec sp_sorter_confirmaleituracaracol '#endereco#', '#leitura#', '#gaiola#'
   10. consulta se agrupador ainda está em aberto
       1.  exec sp_sorter_existecaixapendente ':id_caixa'
   11. campo retorno (existe caixas pendentes do agrupador)
   12. posicaoCheio
   13. confirma pallet cheio
       1. exec sp_sorter_confirmapalletcheio #caracol#, #gaiola#
   14. envia sinal de pallet concluido
3. Controlde de eficiência START
   1. timerEficiencia (10min)
   2. Registra desempenho online
      1. exec sp_siag_gestaovisual_gravaperformance_online
   3. expurgo de dados
      1. exec sp_gta_expurgodados
4. Caracol Pronto NOVO
   1. temporizador de consulta do caracol
   2. Leitura DB3 (todos ramais)
   3. converte dados do CLP para entidade ConfirmacaoCaracol
   4. filtra somente entidades que tenham algum valor preenchido
   5. apaga tabela de referencia
      1. delete from tmp_acaosorter
   6. insere registros alterados
      1. insert into tmp_acaosorter (registro,confirmado,cheio1,cheio2,cheio3,cheio4,cheio5,cheio6,cheio7,cheio8,cheio9,cheio10) values (#registro#,#confirmado#,#cheio1#,#cheio2#,#cheio3#,#cheio4#,#cheio5#,#cheio6#,#cheio7#,#cheio8#,#cheio9#,#cheio10#)
   7. processa dados
      1. exec sp_sorter_acaosorterclp
   8. grava dados no CLP (conforme SP anterior)
   9. truncate sorteracaoclp
      1. truncate table sorteracaoclp
5. Agrupadores Sem Pendencia START
   1. timerAgrupadores (60min)
   2. Finaliza agrupadores sem pendencia 
      1. exec sp_sorter_desalocaagrupadoressempendencia

### Fim
1. Caracol STOP
   1. fecha leitura do caracol
2. Caracol Pronto STOP
   1. para o temporizador do caracol
3. Controle de chamadas STOP
   1. Temporizador (parar)
4. Controle de eficiencia STOP
   1. timerEficiencia (parar)
5. Agrupadores Sem Pendencia STOP
   1. timerAgrupadores (parar)

## Portal do Sorter

### Portal START
1. portal de leitura
2. verifica destino da caixa
   1. CONCAT("exec sp_sorter_destinocaixa '",#1.Retorno#,"'")
3. busca o primeiro registro da lista
4. adiciona na fila (CLP Virtual)
5. le posição fila
6. fila de 0 a 49
7. grava destino
8. grava posição fila
9. atribui valor na trigger
10. converte destino+trigger
11. grava destino+trigger
12. LOG Portal
13. atualiza dados da caixa
    1.  CONCAT("exec sp_sorter_confirmaleituraportal '",#1.Retorno#,"'")

### Portal STOP
1. para leitura do portal