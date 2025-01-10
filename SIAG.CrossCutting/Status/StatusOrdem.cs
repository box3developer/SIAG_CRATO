namespace SIAG.CrossCutting.Status
{
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

}
