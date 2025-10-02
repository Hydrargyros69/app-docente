using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Semestre
{
    public int IdSemestre { get; set; }

    public string NomSemestre { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
