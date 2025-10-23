using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Dato
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdDatos { get; set; }

    [ForeignKey(nameof(Grado))]
    [Required]
    [Display(Name = "Código")]
    public int IdGrados { get; set; }

    [ForeignKey(nameof(Docente))]
    [Required]
    [Display(Name = "Docente")]
    public int IdDocentes { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "Datos Académicos")]
    public string DatosAcademicos { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.Docente.Datos))]
    public virtual Docente IdDocentesNavigation { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.Grado.Datos))]
    public virtual Grado IdGradosNavigation { get; set; } = null!;
}
