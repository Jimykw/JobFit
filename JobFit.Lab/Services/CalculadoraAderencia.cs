using JobFit.Lab.Models;

namespace JobFit.Lab.Services;

public class CalculadoraAderencia
{
    public double Calcular(Vaga vaga, Perfil perfil)
    {
        var requisitosUnicos = vaga.Requisitos
            .DistinctBy(r => r.Nome.Trim().ToLower())
            .ToList();

            var obrigatorios = requisitosUnicos.Where(r => r.Obrigatorio).ToList();
            var desejaveis = requisitosUnicos.Where(r => !r.Obrigatorio).ToList();

            double notaObrigatorios = Proporcao(ContarAtendidos(obrigatorios, perfil.Skills), obrigatorios.Count);
            double notaDesejaveis = Proporcao(ContarAtendidos(desejaveis, perfil.Skills), desejaveis.Count);

            if (obrigatorios.Count == 0 && desejaveis.Count == 0)
            {
                return 0;
            }

            if (obrigatorios.Count == 0)
            {
                return notaDesejaveis * 100;
            }

            if (desejaveis.Count == 0)
            {
                return notaObrigatorios * 100;
            }

            return (notaObrigatorios * 0.7 + notaDesejaveis * 0.3) * 100;
 }

    private int ContarAtendidos(List<Requisito> grupo, List<string> skills)
    {
            var skillsLimpas = skills.Select(s => s.Trim()).ToList();

            return grupo.Count(r => skillsLimpas.Contains(r.Nome.Trim(), StringComparer.OrdinalIgnoreCase)); 
    }

    private double Proporcao(int atendidos, int total)
    {
            if (total == 0)
        {
            return 0;
        }

            return (double)atendidos / total;
    }
}