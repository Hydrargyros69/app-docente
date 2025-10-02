using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class MovDocente
{
    public int Id { get; set; }

    public int IdDocente { get; set; }

    public int? IdCarrera { get; set; }

    public int IdModulo { get; set; }

    public int IdEscuela { get; set; }

    public int IdSede { get; set; }

    public int IdHorario { get; set; }

    public int IdSemestre { get; set; }

    public int Horas { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    public virtual Horario IdHorarioNavigation { get; set; } = null!;

    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    public virtual Sede IdSede1 { get; set; } = null!;

    public virtual Escuela IdSedeNavigation { get; set; } = null!;

    public virtual Semestre IdSemestreNavigation { get; set; } = null!;
}
