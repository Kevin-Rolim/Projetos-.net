using System.ComponentModel.DataAnnotations;

public class ParticipanteCreateViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public  string Nome {get;set;} = string.Empty;
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O email deve ter entre 3 e 100 caracteres.")]
    public string Email {get;set;} = string.Empty;
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [RegularExpression(@"^\d{11}$",ErrorMessage = "O telefone deve ter 11 digitos.")]
    public string Telefone {get;set;} = string.Empty;
}