using JobFit.Lab.Models;
using JobFit.Lab.Services;

namespace JobFit.Tests;

public class CalculadoraAderenciaTests
{
    private static Perfil CriarPerfilPadrao()
    {
        var perfil = new Perfil();
        perfil.AdicionarSkill("C#");
        perfil.AdicionarSkill("SQL Server");
        perfil.AdicionarSkill("Git");
        return perfil;
    }

    [Fact]
    public void  Calcular_VagaComTresObrigatoriosEDoisDesejaveis_Retorna61Virgula67()
    {
        var perfil = CriarPerfilPadrao();
        var vaga = new Vaga("Estagio Backend .NET", new List<Requisito>
        {
            new Requisito("C#", true),
            new Requisito("SQL Server", true),
            new Requisito("APIs Rest", true),
            new Requisito("Docker", false),
            new Requisito("Git", false)
        });

        double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

        Assert.Equal(61.67, resultado, 2);
    }
    [Fact]
    public void Calcular_VagaSoComObrigatorios_Retorna33Virgula33()
    {
        var perfil = CriarPerfilPadrao();
        var vaga = new Vaga("Junior Full Stack", new List<Requisito>
        {
            new Requisito("C#", true),
            new Requisito("JavaScript", true),
            new Requisito("React", true)
        });

        double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

        Assert.Equal(33.33, resultado, 2);
    }

    [Fact]
    public void Calcular_DoisDeTresObrigatoriosENenhumDesejavel_Retorna46Virgula67()
    {
        var perfil = CriarPerfilPadrao();
        var vaga = new Vaga("Pleno Backend .NET", new List<Requisito>
        {
            new Requisito("C#", true),
            new Requisito("SQL Server", true),
            new Requisito("Azure", true),
            new Requisito("Kafka", false),
            new Requisito("Docker", false)
        });

        double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

        Assert.Equal(46.67, resultado, 2);
    }

    [Fact]
    public void Calcular_VagaComDesejaveisNaoAtendidos_Retorna0()
    {
        var perfil = CriarPerfilPadrao();
        var vaga = new Vaga("So desejaveis", new List<Requisito>
        {
            new Requisito("Docker", false)
        });

        double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

        Assert.Equal(0, resultado, 2);
   }
   [Fact]
   public void Calcular_VagaSemRequisitos_Retorna0()
   {

        var perfil = CriarPerfilPadrao();
        var vaga = new Vaga("Sem requisitos", new List<Requisito>());
   
   double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

   Assert.Equal(0, resultado, 2);
   }

   [Fact]
   public void Calcular_RequisitoRepetido_NaoInflaANota()
   {
        var perfil = CriarPerfilPadrao();
        var vaga = new Vaga("Com repetido", new List<Requisito>
        {
            new Requisito("C#", true),
            new Requisito("C#", true),
            new Requisito("Kubernetes", true)
        });

        double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

        Assert.Equal(50, resultado, 2);
   }

   [Theory]
   [InlineData("C#")]
   [InlineData("c#")]
   [InlineData("  C#  ")]
   [InlineData("C#  ")]
   public void Calcular_SkillComMaiusculasOuEspacos_AindaAtendeORequisito(string skill)
    {
        var perfil = new Perfil();
        perfil.AdicionarSkill(skill);

        var vaga = new Vaga("So C#", new List<Requisito>
        {
            new Requisito("C#", true)
        });

        double resultado = new CalculadoraAderencia().Calcular(vaga, perfil);

        Assert.Equal(100, resultado, 2);
    }
}