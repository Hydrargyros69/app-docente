using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public string NomHorario { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
