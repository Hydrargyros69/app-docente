using System;
using System.Collections.Generic;

namespace AppDocentes.Models;

public partial class Docente
{
    public int IdDocente { get; set; }

    public int IdCategoria { get; set; }

    public string Telefono { get; set; } = null!;

    public string NomDocente { get; set; } = null!;

    public string Rut { get; set; } = null!;

    public string PatDocente { get; set; } = null!;

    public string MatDocente { get; set; } = null!;

    public string EstadoCivil { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Nacionalidad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Correo { get; set; }

    public virtual ICollection<Dato> Datos { get; set; } = new List<Dato>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
