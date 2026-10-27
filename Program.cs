var meusSkills = new List<string> { "C#", "SQL Server", "Git" };

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

MostrarResultado(vagaA, meusSkills);
MostrarResultado(vagaB, meusSkills);
MostrarResultado(vagaC, meusSkills);
MostrarResultado(vagaD, meusSkills);
MostrarResultado(vagaE, meusSkills);

// ---------- MÉTODOS ----------

int ContarAtendidos(List<Requisito> grupo, List<string> skills)
{
    var skillsLimpas = skills.Select(s => s.Trim()).ToList();

    return grupo.Count(r => skillsLimpas.Contains(r.Nome, StringComparer.OrdinalIgnoreCase));
}

double Proporcao(int atendidos, int total)
{
    if (total == 0)
    {
        return 0;
    }

    return (double)atendidos / total;
}
double CalcularAderencia(Vaga vaga, List<string> skills)
{
  
    var requisitosUnicos = vaga.Requisitos
        .DistinctBy(r => r.Nome.Trim().ToLower())
        .ToList();
    
    var obrigatorios = requisitosUnicos.Where(r => r.Obrigatorio).ToList();
    var desejaveis = requisitosUnicos.Where(r => !r.Obrigatorio).ToList();

    double notaObrigatorios = Proporcao(ContarAtendidos(obrigatorios, skills), obrigatorios.Count);
    double notaDesejaveis = Proporcao(ContarAtendidos(desejaveis, skills), desejaveis.Count);

    // Vaga sem nenhum requisito: não há o que avaliar
    if (obrigatorios.Count == 0 && desejaveis.Count == 0)
    {
        return 0;
    }

    // Vaga só com desejáveis: a nota é só a deles
    if (obrigatorios.Count == 0)
    {
        return notaDesejaveis * 100;
    }

    // Só obrigatórios: a nota é só a deles
    if (desejaveis.Count == 0)
    {
        return notaObrigatorios * 100;
    }

    return (notaObrigatorios * 0.7 + notaDesejaveis * 0.3) * 100;
}

void MostrarResultado(Vaga vaga, List<string> skills)
{
    double aderencia = CalcularAderencia(vaga, skills);

    var faltando = vaga.Requisitos
        .Where(r => !skills.Contains(r.Nome, StringComparer.OrdinalIgnoreCase))
        .Select(r => r.Nome);

    Console.WriteLine($"=== {vaga.Titulo} ===");
    Console.WriteLine($"Aderência: {aderencia:F0}%");
    Console.WriteLine($"Faltando: {string.Join(", ", faltando)}");
    Console.WriteLine();
}

// ---------- TIPOS (sempre no final) ----------

record Requisito(string Nome, bool Obrigatorio);
record Vaga(string Titulo, List<Requisito> Requisitos);