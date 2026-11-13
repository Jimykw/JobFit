using JobFit.Api.Dtos;
using JobFit.Lab.Models;
using JobFit.Lab.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobFit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AderenciaController : ControllerBase
{
    private readonly CalculadoraAderencia _calculadora = new CalculadoraAderencia();

    [HttpPost]
    public IActionResult Calcular([FromBody] AderenciaRequest request)
    {
        if (request.Requisitos.Count == 0)
        {
            return BadRequest("Informe ao menos um requisito.");
        }

        var perfil = new Perfil();
        foreach (var skill in request.Skills)
        {
            perfil.AdicionarSkill(skill);
        }

        var requisitos = request.Requisitos
            .Select(r => new Requisito(r.Nome, r.Obrigatorio))
            .ToList();

        var vaga = new Vaga(request.TituloVaga, requisitos);

        double aderencia = _calculadora.Calcular(vaga, perfil);

        var faltando = vaga.Requisitos
            .Where(r => !perfil.Skills.Contains(r.Nome.Trim(), StringComparer.OrdinalIgnoreCase))
            .Select(r=> r.Nome)
            .ToList();

        return Ok(new AderenciaResponse(vaga.Titulo, Math.Round(aderencia, 2), faltando));
    }
}