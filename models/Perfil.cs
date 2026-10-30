namespace JobFit.Lab.Models;

public class Perfil
{
    public string Nome { get; set; } = "";
    public int AnosExperiacia { get; set; }
    public List<string> Skills { get; set; } = new List<string>();

    public void AdicionarSkill(string skills)
    {
        var limpa = skills.Trim();

        if (limpa == "")
        {
            return;
        }
        
        if (!Skills.Contains(limpa, StringComparer.OrdinalIgnoreCase))
        {
            Skills.Add(limpa);
        }

    }
}