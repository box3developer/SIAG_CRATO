using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class AreaArmazenagem
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Codigo { get; private set; }

        public AreaArmazenagem(string nome, string codigo)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Codigo = codigo;
        }

        public void Atualizar(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }
    }
}
