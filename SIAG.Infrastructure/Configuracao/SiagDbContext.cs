using Microsoft.EntityFrameworkCore;
using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Domain.Armazenagem.Core.Models;
using System.Reflection;

namespace SIAG.Infrastructure.Configuracao
{
    public class SiagDbContext : DbContext
    {
        public SiagDbContext(DbContextOptions<SiagDbContext> options) : base(options) { }

        public DbSet<AgrupadorAtivo> AgrupadorAtivo { get; set; }
        public DbSet<AreaArmazenagem> AreaArmazenagem { get; set; }
        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<AtividadePrioridade> AtividadePrioridade { get; set; }
        public DbSet<AtividadeRejeicao> AtividadeRejeicaoM { get; set; }
        public DbSet<AtividadeRotina> AtividadeRotina { get; set; }
        public DbSet<AtividadeTarefa> AtividadeTarefa { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<CaixaLeitura> CaixaLeitura { get; set; }
        public DbSet<Chamada> Chamada { get; set; }
        public DbSet<ChamadaTarefa> ChamadaTarefa { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<Desempenho> Desempenho { get; set; }
        public DbSet<DesempenhoCaixa> DesempenhoCaixa { get; set; }
        public DbSet<DesempenhoOnline> DesempenhoOnline { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Equipamento> Equipamento { get; set; }
        public DbSet<EquipamentoChecklist> EquipamentoChecklist { get; set; }
        public DbSet<EquipamentoCheckListOperador> EquipamentoCheckListOperador { get; set; }
        public DbSet<EquipamentoEndereco> EquipamentoEndereco { get; set; }
        public DbSet<EquipamentoEnderecoPrioridade> EquipamentoEnderecoPrioridade { get; set; }
        public DbSet<EquipamentoManutencao> EquipamentoManutencao { get; set; }
        public DbSet<EquipamentoModelo> EquipamentoModelo { get; set; }
        public DbSet<LiderVirtual> LiderVirtual { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<LogCaracol> LogCaracol { get; set; }
        public DbSet<NiveisAgrupadores> NiveisAgrupadores { get; set; }
        public DbSet<Operador> Operador { get; set; }
        public DbSet<OperadorHistorico> OperadorHistorico { get; set; }
        public DbSet<Ordem> Ordem { get; set; }
        public DbSet<OrdemCarga> OrdemCarga { get; set; }
        public DbSet<OrdemSequencia> OrdemSequencia { get; set; }
        public DbSet<Pallet> Pallet { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<ParametroMensagemCaracol> ParametroMensagemCaracol { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PosicaoCaracolRefugo> PosicaoCaracolRefugo { get; set; }
        public DbSet<Programa> Programa { get; set; }
        public DbSet<RegiaoTrabalho> RegiaoTrabalho { get; set; }
        public DbSet<SetorTrabalho> SetorTrabalho { get; set; }
        public DbSet<StatusLeitor> StatusLeitor { get; set; }
        public DbSet<StatusLuzVerde> StatusLuzVerde { get; set; }
        public DbSet<StatusLuzVermelha> StatusLuzVermelha { get; set; }
        public DbSet<TempoAtividade> TempoAtividade { get; set; }
        public DbSet<TipoArea> TipoArea { get; set; }
        public DbSet<TipoEndereco> TipoEndereco { get; set; }
        public DbSet<TransportadoraTipoCarga> TransportadoraTipoCarga { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<Uf> Uf { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SetDeleteNoRestrict(modelBuilder);
            SetIdentity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetIdentity(ModelBuilder modelBuilder)
        {
            // Configurações para tabelas básicas
            var basicEntities = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.GetCustomAttributes(typeof(BasicEntityAttribute), true).Any());

            foreach (var entityType in basicEntities)
            {
                var primaryKey = entityType.FindPrimaryKey();

                if (primaryKey != null)
                {
                    foreach (var property in primaryKey.Properties)
                    {
                        property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                        property.IsNullable = false; // Garante que a chave não aceita valores nulos
                    }
                }
            }

            // Configurações para tabelas com chave personalizável
            var customKeyEntities = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.GetCustomAttributes(typeof(CustomKeyEntityAttribute), true).Any());

            foreach (var entityType in customKeyEntities)
            {
                var primaryKey = entityType.FindPrimaryKey();

                if (primaryKey != null)
                {
                    foreach (var property in primaryKey.Properties)
                    {
                        property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
                        property.IsNullable = false;
                    }
                }
            }

            // Configurações para tabelas sem chave
            var keylessEntities = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.GetCustomAttributes(typeof(KeylessEntityAttribute), true).Any());

            foreach (var entityType in keylessEntities)
            {
                modelBuilder.Entity(entityType.ClrType).HasNoKey();
            }
        }

        private void SetDeleteNoRestrict(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                              .SelectMany(t => t.GetForeignKeys())
                                              .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
