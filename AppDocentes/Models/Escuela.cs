using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Escuela
{
    public int IdEscuela { get; set; }

    public string NomEscuela { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
