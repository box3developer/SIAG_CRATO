using Microsoft.EntityFrameworkCore;
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
        public DbSet<Posicaocaracolrefugo> Posicaocaracolrefugo  { get; set; }
        public DbSet<Parametromensagemcaracol> Parametromensagemcaracol { get; set; }
        public DbSet<Status_leitor> Status_leitor { get; set; }
        public DbSet<Lidervirtual> Lidervirtual { get; set; }
        public DbSet<Niveisagrupadores> Niveisagrupadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SetDeleteNoRestrict(modelBuilder);
            SetIdentity(modelBuilder);

            modelBuilder.Entity<Operador>()
                .Property(o => o.IdOperador)
                .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }

        private void SetIdentity(ModelBuilder modelBuilder)
        {
            var primaryKeys = modelBuilder.Model.GetEntityTypes()
                                               .Select(entity => entity.FindPrimaryKey())
                                               .Where(pk => pk != null);

            foreach (var primaryKey in primaryKeys)
            {
                if (primaryKey != null)
                {
                    foreach (var property in primaryKey.Properties)
                    {
                        property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                        property.IsNullable = false;
                    }
                }
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
