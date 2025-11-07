using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Modulo", Schema = "adminom")]
public partial class Modulo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdModulo { get; set; }

    [Required(ErrorMessage = "La carrera es obligatoria.")]
    [Display(Name = "Carrera")]
    [ForeignKey(nameof(IdCarreraNavigation))]
    public int IdCarrera { get; set; }

    [Required(ErrorMessage = "El nombre del módulo es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [Display(Name = "Nombre Módulo")]
    public string NomModulo { get; set; } = null!;

    [Column("horas")]
    [Range(0, int.MaxValue, ErrorMessage = "Las horas deben ser un número entero no negativo.")]
    [Display(Name = "Horas")]
    public int? Horas { get; set; }

    [InverseProperty(nameof(AppDocentes.Models.Carrera.Modulos))]
    public virtual Carrera? IdCarreraNavigation { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.MovDocente.IdModuloNavigation))]
    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
