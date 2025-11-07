using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Datos", Schema = "adminom")]
public partial class Dato
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdDatos { get; set; }

    [Required(ErrorMessage = "El grado es obligatorio.")]
    [Display(Name = "Grado")]
    [ForeignKey(nameof(IdGradosNavigation))]
    public int IdGrados { get; set; }

    [Required(ErrorMessage = "El docente es obligatorio.")]
    [Display(Name = "Docente")]
    [ForeignKey(nameof(IdDocentesNavigation))]
    public int IdDocentes { get; set; }

    [Required(ErrorMessage = "Los datos académicos son obligatorios.")]
    [StringLength(150, MinimumLength = 5, ErrorMessage = "Los datos académicos deben tener entre 5 y 150 caracteres.")]
    [Display(Name = "Datos Académicos")]
    public string DatosAcademicos { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.Docente.Datos))]
    public virtual Docente IdDocentesNavigation { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.Grado.Datos))]
    public virtual Grado IdGradosNavigation { get; set; } = null!;
}
