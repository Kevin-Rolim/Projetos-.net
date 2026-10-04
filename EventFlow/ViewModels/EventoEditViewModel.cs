using System.ComponentModel.DataAnnotations;
public class EventoEditViewModel
{
    public int Id {get;set;}

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 50 caracteres.")]
    public string Titulo {get;set;} = string.Empty;
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    public string? Descricao {get;set;}
    [Required(ErrorMessage = "A data é obrigatória.")]
    [DataType(DataType.DateTime)]
    public DateTime DataHora {get;set;}
    [Required(ErrorMessage = "O local é obrigatório.")]
    [StringLength(100, ErrorMessage = "O local deve ter no máximo 100 caracteres.")]
    public string Local {get;set;} = string.Empty;
    [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
    [Range(1, 999999, ErrorMessage = "A capacidade máxima deve ter entre 1 a 999999 unidades.")]
    public int CapacidadeMaxima {get;set;}
}