using System.Text.RegularExpressions;

public class ValidarModels
{ 
    public static readonly Regex RegexCaracteresRepetidos = 
        new Regex(@"([\p{L}\s])\1{2,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexMuitasConsoantes = 
        new Regex(@"[^aeiouáéíóúâêîôûãõàèìòùüç\s'’-]{5,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
    public static readonly Regex RegexCaracteresValidos = 
        new Regex(@"^[\p{L}'’\-\s]+$", RegexOptions.Compiled);

    // Retorna TRUE se for nulo, vazio ou espaços
    public static string ValidateNullOrWhiteSpace(string entity)
    {
        if (string.IsNullOrWhiteSpace(entity))
        {
            throw new ArgumentException("O entity não pode ser nulo, vazio ou conter apenas espaços.", nameof(entity));
        }
        return entity.Trim();
    }

    // Permite letras, espaços, hífens e apóstrofos
    public static string ValidarCaracteresValidos(string entity)
    {
        // SE NÃO for nulo ou vazio, valida a Regex
        if (ValidateNullOrWhiteSpace(entity)) 
        {
            throw new ArgumentException("O entity não pode ser nulo, vazio ou conter apenas espaços.", nameof(entity));
        }
        return entity.Trim(); // Se for nulo/vazio, é inválido
    }

    // Detecta se qualquer caractere se repete 3 ou mais vezes seguidas (ex: "aaa")
    public bool ValidarCaracteresRepetidos(string entity)
    {
        // SE NÃO for nulo ou vazio, valida a Regex
        if (!ValidateNullOrWhiteSpace(entity))
        {
            string entityTratado = entity.Trim();
            return RegexCaracteresRepetidos.IsMatch(entityTratado);
        }
        return entity.Trim(); 
    }

    // Detecta sequências longas de consoantes sem nenhuma vogal por perto (ex: 5 consoantes)
    public bool ValidarMuitasConsoantes(string entity)
    {
        // SE NÃO for nulo ou vazio, valida a Regex
        if (!ValidateNullOrWhiteSpace(entity))
        {
            string entityTratado = entity.Trim();
            return RegexMuitasConsoantes.IsMatch(entityTratado);
        }
        return entity.Trim();
    }
}
