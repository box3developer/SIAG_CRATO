using Microsoft.EntityFrameworkCore;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Infrastructure.Configuracao
{
    public class SiagDbContext:DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
