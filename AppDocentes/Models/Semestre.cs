using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Semestre", Schema = "adminom")]
public partial class Semestre
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdSemestre { get; set; }

    [Required(ErrorMessage = "La descripción del semestre es obligatoria.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "La descripción debe tener entre 3 y 50 caracteres.")]
    [Display(Name = "Descripción")]
    public string NomSemestre { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.MovDocente.IdSemestreNavigation))]
    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
