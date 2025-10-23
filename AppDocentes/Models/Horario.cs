using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Horario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdHorario { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Descripción")]
    public string NomHorario { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
