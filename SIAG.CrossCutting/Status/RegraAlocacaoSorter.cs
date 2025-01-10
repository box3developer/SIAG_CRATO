namespace SIAG.CrossCutting.Status
{
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

}
