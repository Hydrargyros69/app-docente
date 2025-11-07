using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Carrera", Schema = "adminom")]
public partial class Carrera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdCarrera { get; set; }

    [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [Display(Name = "Nombre")]
    public string NomCarrera { get; set; } = null!;

    [InverseProperty("IdCarreraNavigation")]
    public virtual ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();
}
