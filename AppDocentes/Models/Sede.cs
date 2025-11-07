using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Sede", Schema = "adminom")]
public partial class Sede
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdSede { get; set; }

    [Required(ErrorMessage = "La comuna es obligatoria.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La comuna debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Comuna")]
    public string Comuna { get; set; } = null!;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 50 caracteres.")]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = null!;

    [Required(ErrorMessage = "El nombre de la sede es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre de la sede debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Sede")]
    public string NomSede { get; set; } = null!;

    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La ciudad debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Ciudad")]
    public string NomCiudad { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.MovDocente.IdSede1))]
    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
