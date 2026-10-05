using System.Text.RegularExpressions;

public class ValidarModels
{
    private static readonly Regex RegexCaracteresRepetidos =
        new Regex(@"([\p{L}\s])\1{5,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex RegexMuitasConsoantes =
        new Regex(@"[^aeiouáéíóúâêîôûãõàèìòùüç\s'’-]{6,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
    private static readonly Regex RegexCaracteresValidos =
        new Regex(@"^[\p{L}'’\-\s]+$", RegexOptions.Compiled);
    private static readonly Regex RegexDigitosInvalidos =
        new Regex( @"^\d{11}$", RegexOptions.Compiled);

    // Retorna TRUE se for nulo, vazio ou espaços
    private static string ValidateNullOrWhiteSpace(string entity)
    {
        if (string.IsNullOrWhiteSpace(entity))
        {
            throw new ArgumentException("Não pode ser nulo, vazio ou conter apenas espaços.", nameof(entity));
        }
        return entity;
    }

    // Permite letras, espaços, hífens e apóstrofos
    public static void ValidarCaracteresValidos(string entity)
    {
        if (!RegexCaracteresValidos.IsMatch(ValidateNullOrWhiteSpace(entity)))
        {
            throw new ArgumentException("Contém caracteres inválidos.", nameof(entity));
        }
    }

    // Detecta se qualquer caractere se repete 4 ou mais vezes seguidas (ex: "aaaaa")
    public static void ValidarCaracteresRepetidos(string entity)
    {
        if (RegexCaracteresRepetidos.IsMatch(ValidateNullOrWhiteSpace(entity)))
        {
            throw new ArgumentException("Contém caracteres repetidos.", nameof(entity));
        }
    }

    // Detecta sequências longas de consoantes sem nenhuma vogal por perto (ex: 5 consoantes)
    public static void ValidarMuitasConsoantes(string entity)
    {
        if (RegexMuitasConsoantes.IsMatch(ValidateNullOrWhiteSpace(entity)))
        {
            throw new ArgumentException("Contém muitas consoantes sem vogal próxima.", nameof(entity));
        }
    }
        public static void ValidarDigitos(string entity)
    {
        
        if (RegexDigitosInvalidos.IsMatch(ValidateNullOrWhiteSpace(entity)))
        {
            throw new ArgumentException("O número deve conter exatamente 11 dígitos. DDD + Número Ex.: 11987654321", nameof(entity));
        }
    }
}
