using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NomUsuario { get; set; } = null!;

    public string CorrUsuario { get; set; } = null!;

    public string PassUsuario { get; set; } = null!;
}
