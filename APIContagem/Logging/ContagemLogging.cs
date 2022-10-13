namespace APIContagem.Logging;

public static partial class ContagemLogging
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information,
        Message = "{metodo} Contador - Valor atual: {valorAtual}")]
    public static partial void LogValorAtual(
        this ILogger logger, string metodo, int valorAtual);
}