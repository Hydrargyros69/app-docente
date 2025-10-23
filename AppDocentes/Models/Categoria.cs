using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdCategoria { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string NomCategoria { get; set; } = null!;

    public virtual ICollection<Docente> Docentes { get; set; } = new List<Docente>();
}
