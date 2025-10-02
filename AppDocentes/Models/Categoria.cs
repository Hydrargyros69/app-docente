using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NomCategoria { get; set; } = null!;

    public virtual ICollection<Docente> Docentes { get; set; } = new List<Docente>();
}
