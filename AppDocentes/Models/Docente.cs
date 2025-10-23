using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Docente
{
    [Key]
    [Display(Name = "Código")]
    public int IdDocente { get; set; }

    [ForeignKey(nameof(IdCategoriaNavigation))]
    [Required]
    [Display(Name = "Categoria")]
    public int IdCategoria { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = null!;

    [Required]
    [StringLength(100)]
    [Display(Name = "Docente")]
    public string NomDocente { get; set; } = null!;

    [Required]
    [StringLength(12)]
    [Display(Name = "Rut")]
    public string Rut { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Display(Name = "Apellido Paterno")]
    public string PatDocente { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Display(Name = "Apellido Materno")]
    public string MatDocente { get; set; } = null!;

    [Required]
    [StringLength(20)]
    [Display(Name = "Estado Civil")]
    public string EstadoCivil { get; set; } = null!;

    [Required]
    [Display(Name = "Fecha Nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Nacionalidad")]
    public string Nacionalidad { get; set; } = null!;

    [Required]
    [StringLength(200)]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = null!;

    [EmailAddress]
    [StringLength(100)]
    [Display(Name = "Correo")]
    public string? Correo { get; set; }

    public virtual ICollection<Dato> Datos { get; set; } = new List<Dato>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
