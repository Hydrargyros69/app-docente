using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Grado
{
    public int IdGrado { get; set; }

    public string NomGrado { get; set; } = null!;

    public virtual ICollection<Dato> Datos { get; set; } = new List<Dato>();
}
