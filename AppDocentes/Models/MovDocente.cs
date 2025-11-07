using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("MovDocente", Schema = "adminom")]
public partial class MovDocente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required(ErrorMessage = "El docente es obligatorio.")]
    [ForeignKey(nameof(IdDocenteNavigation))]
    [Display(Name = "Código")]
    public int IdDocente { get; set; }

    [ForeignKey(nameof(IdModuloNavigation))]
    [Display(Name = "Módulo")]
    public int IdModulo { get; set; }

    [ForeignKey(nameof(IdSedeNavigation))]
    [Display(Name = "Escuela")]
    public int IdEscuela { get; set; }

    [ForeignKey(nameof(IdSede1))]
    [Display(Name = "Sede")]
    public int IdSede { get; set; }

    [ForeignKey(nameof(IdHorarioNavigation))]
    [Display(Name = "Horario")]
    public int IdHorario { get; set; }

    [ForeignKey(nameof(IdSemestreNavigation))]
    [Display(Name = "Semestre")]
    public int IdSemestre { get; set; }

    [Column("idCarrera")]
    [Display(Name = "Carrera")]
    public int? IdCarrera { get; set; }

    [Required(ErrorMessage = "Las horas son obligatorias.")]
    [Range(0, int.MaxValue, ErrorMessage = "Las horas deben ser un número entero no negativo.")]
    [Display(Name = "Horas")]
    public int Horas { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly Fecha { get; set; }

    [InverseProperty(nameof(Docente.MovDocentes))]
    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    [InverseProperty(nameof(Horario.MovDocentes))]
    public virtual Horario IdHorarioNavigation { get; set; } = null!;

    [InverseProperty(nameof(Modulo.MovDocentes))]
    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    [InverseProperty(nameof(Sede.MovDocentes))]
    public virtual Sede IdSede1 { get; set; } = null!;

    [InverseProperty(nameof(Escuela.MovDocentes))]
    public virtual Escuela IdSedeNavigation { get; set; } = null!;

    [InverseProperty(nameof(Semestre.MovDocentes))]
    public virtual Semestre IdSemestreNavigation { get; set; } = null!;
}
