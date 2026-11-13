namespace JobFit.Api.Dtos;

public record RequisitoDto(string Nome, bool Obrigatorio);

public record AderenciaRequest(
    string TituloVaga,
    List<RequisitoDto> Requisitos,
    List<string> Skills);

public record AderenciaResponse(
    string TituloVaga,
    double Aderencia,
    List<string> Faltando);