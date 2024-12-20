# 📦 GTA SIAG SOBRAL

## 🌀 Caracol

### ▶️ Início

1. **Controle de Chamadas START**
   - **Temporizador**
   - Executa: `exec sp_siag_expedicaocontrolaordens`
   - Envio de **SMS**
   - Envio de **Email Transportadora**
     - **OBS:** *DESATIVADO!* Quando publicar o SIAG com o handler chamado aqui, remover a condição que impede de executar.

2. **Caracol START**
   - Recebe **Leitura Caracol (Socket)**
   - Processa: **Caracol + Leitura**
   - **Transforma Registro em Entidade**
     - Executa: `exec sp_sorter_leituracaracol ':caracol', ':leitura'`
   - Processa **Resultado**
   - Define **Campo Posição Destino**
   - Processa **Gaiola**
   - Envia **Gaiola Destino**
   - Atribui valor da leitura para a entidade
   - Registra a caixa:
     - Executa: `exec sp_sorter_confirmaleituracaracol '#endereco#', '#leitura#', '#gaiola#'`
   - Consulta agrupador:
     - Executa: `exec sp_sorter_existecaixapendente ':id_caixa'`
   - Define **Campo Retorno**
     - *(Caixas pendentes do agrupador)*
   - Processa **Pallet Cheio**
     - Confirma: `exec sp_sorter_confirmapalletcheio #caracol#, #gaiola#`
   - Envia sinal de pallet concluído

3. **Controle de Eficiência START**
   - Temporizador: **10 minutos**
   - Registra desempenho online:
     - Executa: `exec sp_siag_gestaovisual_gravaperformance_online`
   - Expurgo de dados:
     - Executa: `exec sp_gta_expurgodados`

4. **Caracol Pronto NOVO**
   - Temporizador de consulta do caracol
   - **Leitura DB3** (Todos ramais)
   - Converte dados do CLP para entidade **ConfirmacaoCaracol**
   - Filtra entidades com valores preenchidos
   - Apaga tabela de referência:
     - Executa: `delete from tmp_acaosorter`
   - Insere registros alterados:
     - Executa: `insert into tmp_acaosorter (...) values (...)`
   - Processa dados:
     - Executa: `exec sp_sorter_acaosorterclp`
   - Grava dados no CLP conforme SP anterior
   - Trunca tabela:
     - Executa: `truncate table sorteracaoclp`

5. **Agrupadores Sem Pendência START**
   - Temporizador: **60 minutos**
   - Finaliza agrupadores sem pendência:
     - Executa: `exec sp_sorter_desalocaagrupadoressempendencia`

---

### ⏹️ Fim

1. **Caracol STOP**
   - Fecha leitura do caracol
2. **Caracol Pronto STOP**
   - Para o temporizador do caracol
3. **Controle de Chamadas STOP**
   - Para o temporizador
4. **Controle de Eficiência STOP**
   - Para o timerEficiencia
5. **Agrupadores Sem Pendência STOP**
   - Para o timerAgrupadores

---

## 🚦 Portal do Sorter

### ▶️ Portal START

1. Inicia o **Portal de Leitura**
2. Verifica destino da caixa:
   - Executa: `CONCAT("exec sp_sorter_destinocaixa '",#1.Retorno#,"'")`
3. Busca o primeiro registro da lista
4. Adiciona na fila (**CLP Virtual**)
5. Lê posição fila
6. Define fila: **0 a 49**
7. Grava destino
8. Grava posição fila
9. Atribui valor na trigger
10. Converte destino + trigger
11. Grava destino + trigger
12. Registra no **LOG Portal**
13. Atualiza dados da caixa:
    - Executa: `CONCAT("exec sp_sorter_confirmaleituraportal '",#1.Retorno#,"'")`

---

### ⏹️ Portal STOP

1. Para leitura do portal
