using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Modulo
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdModulo { get; set; }

    [Required]
    [ForeignKey(nameof(IdCarreraNavigation))]
    [Display(Name = "Carrera")]
    public int IdCarrera { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre Módulo")]
    public string NomModulo { get; set; } = null!;

    [Range(0, int.MaxValue)]
    [Display(Name = "Horas")]
    public int? Horas { get; set; }

    public virtual Carrera? IdCarreraNavigation { get; set; } = null!;

     public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
