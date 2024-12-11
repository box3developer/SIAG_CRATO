using System.ComponentModel.DataAnnotations;

namespace SIAG_CRATO.Data;

public enum Ativo
{
    [Display(Name = "Inativo")]
    Inativo = 0,

    [Display(Name = "Ativo")]
    Ativo = 1
}
