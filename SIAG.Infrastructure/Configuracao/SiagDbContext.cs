using Microsoft.EntityFrameworkCore;
using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System.Reflection;

namespace SIAG.Infrastructure.Configuracao
{
    public class SiagDbContext : DbContext
    {
        public SiagDbContext(DbContextOptions<SiagDbContext> options) : base(options) { }

        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<TipoEndereco> TipoEndereco { get; set; }
        public DbSet<SetorTrabalho> SetorTrabalho { get; set; }
        public DbSet<RegiaoTrabalho> RegiaoTrabalho { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<TipoArea> TipoArea { get; set; }
        public DbSet<AgrupadorAtivo> AgrupadorAtivo { get; set; }
        public DbSet<AreaArmazenagem> AreaArmazenagem { get; set; }
        public DbSet<Pallet> Pallet { get; set; }
        public DbSet<Programa> Programa { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<Equipamento> Equipamento { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<Operador> Operador { get; set; }

        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<Caixaleitura> Caixaleitura { get; set; }
        public DbSet<Chamada> Chamada { get; set; }
        public DbSet<Desempenho> Desempenho { get; set; }
        public DbSet<Desempenhocaixa> Desempenhocaixa { get; set; }
        public DbSet<Operadorhistorico> Operadorhistorico { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<Posicaocaracolrefugo> Posicaocaracolrefugo { get; set; }
        public DbSet<Parametromensagemcaracol> Parametromensagemcaracol { get; set; }
        public DbSet<Status_leitor> Status_leitor { get; set; }
        public DbSet<Lidervirtual> Lidervirtual { get; set; }
        public DbSet<Niveisagrupadores> Niveisagrupadores { get; set; }
        public DbSet<EquipamentoModelo> EquipamentoModelo { get; set; }

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
