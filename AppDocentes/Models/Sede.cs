using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Sede
{
    public int IdSede { get; set; }

    public string Comuna { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string NomSede { get; set; } = null!;

    public string NomCiudad { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
