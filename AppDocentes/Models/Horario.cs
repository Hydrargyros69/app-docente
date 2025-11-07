using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Horario", Schema = "adminom")]
public partial class Horario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdHorario { get; set; }

    [Required(ErrorMessage = "La descripción del horario es obligatoria.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "La descripción debe tener entre 3 y 50 caracteres.")]
    [Display(Name = "Descripción")]
    public string NomHorario { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.MovDocente.IdHorarioNavigation))]
    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
