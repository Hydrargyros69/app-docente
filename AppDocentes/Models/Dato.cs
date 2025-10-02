using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Dato
{
    public int IdDatos { get; set; }

    public int IdGrados { get; set; }

    public int IdDocentes { get; set; }

    public string DatosAcademicos { get; set; } = null!;

    public virtual Docente IdDocentesNavigation { get; set; } = null!;

    public virtual Grado IdGradosNavigation { get; set; } = null!;
}
