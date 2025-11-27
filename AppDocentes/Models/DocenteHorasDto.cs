namespace AppDocentes.Models
{
    // DTO: resultado agrupado Docente -> Carrera -> Módulo -> TotalHoras
    public class DocenteHorasDto
    {
        public int IdDocente { get; set; }
        public string NomDocente { get; set; } = string.Empty;

        public int IdCarrera { get; set; }
        public string NomCarrera { get; set; } = string.Empty;

        public int IdModulo { get; set; }
        public string NomModulo { get; set; } = string.Empty;

        // Si MovDocente.Horas es nullable usa int? y en la suma g.Sum(x => x.Horas ?? 0)
        public int TotalHoras { get; set; }
    }
}