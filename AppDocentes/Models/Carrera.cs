using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppDocentes.Models;

public partial class Carrera
{
    [Display(Name = "Código")]
    public int IdCarrera { get; set; }

    [Display(Name = "Nombre")]
    public string NomCarrera { get; set; } = null!;

   
    public virtual ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();
}
