using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Grado", Schema = "adminom")]
public partial class Grado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdGrado { get; set; }

    [Required(ErrorMessage = "El nombre del grado es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Nombre")]
    public string NomGrado { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.Dato.IdGradosNavigation))]
    public virtual ICollection<Dato> Datos { get; set; } = new List<Dato>();
}
