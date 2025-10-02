using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Modulo
{
    public int IdModulo { get; set; }

    public int IdCarrera { get; set; }

    public string NomModulo { get; set; } = null!;

    public int? Horas { get; set; }

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
