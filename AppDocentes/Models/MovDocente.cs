using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class MovDocente
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Docente))]
    [Display(Name = "Código")]
    public int IdDocente { get; set; }

    [ForeignKey(nameof(Modulo))]
    [Display(Name = "Módulo")]
    public int IdModulo { get; set; }

    [ForeignKey(nameof(Escuela))]
    [Display(Name = "Escuela")]
    public int IdEscuela { get; set; }

    [ForeignKey(nameof(Sede))]
    [Display(Name = "Sede")]
    public int IdSede { get; set; }

    [ForeignKey(nameof(Horario))]
    [Display(Name = "Horario")]
    public int IdHorario { get; set; }

    [ForeignKey(nameof(Semestre))]
    [Display(Name = "Semestre")]
    public int IdSemestre { get; set; }

    [Display(Name = "Carrera")]
    public int? IdCarrera { get; set; }

    [Required]
    [Display(Name = "Horas")]
    public int Horas { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
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
