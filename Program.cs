using JobFit.Lab.Models;
using JobFit.Lab.Services;

var perfil = new Perfil { Nome = "Jimy", AnosExperiencia = 0 };
perfil.AdicionarSkill("C#");
perfil.AdicionarSkill("SQL Server");
perfil.AdicionarSkill("Git");

var vagaA = new Vaga("Estágio Backend .NET", new List<Requisito>
{
    new Requisito("C#", true),
    new Requisito("SQL Server", true),
    new Requisito("APIs REST", true),
    new Requisito("Docker", false),
    new Requisito("Git", false)
});

var vagaB = new Vaga("Júnior Full Stack", new List<Requisito>
{
    new Requisito("C#", true),
    new Requisito("JavaScript", true),
    new Requisito("React", true)
});

var vagaC = new Vaga("Pleno Backend .NET", new List<Requisito>
{
    new Requisito("C#", true),
    new Requisito("SQL Server", true),
    new Requisito("Azure", true),
    new Requisito("Kafka", false),
    new Requisito("Docker", false)
});

var vagaD = new Vaga("Só desejáveis", new List<Requisito>
{
    new Requisito("Docker", false)
});

var vagaE = new Vaga("Sem requisitos", new List<Requisito>());

var vagas = new List<Vaga> { vagaA, vagaB, vagaC, vagaD, vagaE };
var calculadora = new CalculadoraAderencia();

foreach (var vaga in vagas)
{
    MostrarResultado(vaga, perfil, calculadora);
}

// ---------- MÉTODO ----------

void MostrarResultado(Vaga vaga, Perfil perfil, CalculadoraAderencia calculadora)
{
    double aderencia = calculadora.Calcular(vaga, perfil);

    var faltando = vaga.Requisitos
        .Where(r => !perfil.Skills.Contains(r.Nome.Trim(), StringComparer.OrdinalIgnoreCase))
        .Select(r => r.Nome);

    Console.WriteLine($"=== {vaga.Titulo} ===");
    Console.WriteLine($"Aderência: {aderencia:F0}%");
    Console.WriteLine($"Faltando: {string.Join(", ", faltando)}");
    Console.WriteLine();
}