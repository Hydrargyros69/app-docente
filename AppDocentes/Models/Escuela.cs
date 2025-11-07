using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Escuela", Schema = "adminom")]
public partial class Escuela
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdEscuela { get; set; }

    [Required(ErrorMessage = "El nombre de la escuela es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Nombre")]
    public string NomEscuela { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.MovDocente.IdSedeNavigation))]
    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
