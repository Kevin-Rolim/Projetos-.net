public class Evento
{
    public int Id {get;set;}
    public string Titulo {get;set;} = string.Empty;
    public string? Descricao {get;set;}
    public DateTime DataHora {get;set;}
    public int Situacao {get;set;}
    public string Local {get;set;} = string.Empty;
    public int CapacidadeMaxima {get;set;}
}