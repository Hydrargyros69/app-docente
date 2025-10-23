using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Escuela
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdEscuela { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string NomEscuela { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
