using Microsoft.AspNetCore.Mvc;
using APIContagem.Models;
using APIContagem.Logging;
using Groffe.AspNetCore.ApiKeyChecking;

namespace APIContagem.Controllers;

[ApiController]
[Route("[controller]")]
[MiddlewareFilter(typeof(ApiKeyCheckingPipeline))]
public class ContadorController : ControllerBase
{
    private static readonly Contador _CONTADOR = new Contador();
    private readonly ILogger<ContadorController> _logger;
    private readonly IConfiguration _configuration;

    public ContadorController(ILogger<ContadorController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public ResultadoContador Get()
    {
        int valorAtualContador;

        lock (_CONTADOR)
        {
            _CONTADOR.Incrementar();
            valorAtualContador = _CONTADOR.ValorAtual;
        }

        _logger.LogValorAtual(nameof(Get), valorAtualContador);

        return new()
        {
            ValorAtual = valorAtualContador,
            Producer = _CONTADOR.Local,
            Kernel = _CONTADOR.Kernel,
            Framework = _CONTADOR.Framework,
            Mensagem = _configuration["MensagemVariavel"]
        };
    }
}