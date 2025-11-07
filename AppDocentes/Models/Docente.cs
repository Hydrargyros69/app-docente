using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

[Table("Docente", Schema = "adminom")]
public partial class Docente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Código")]
    public int IdDocente { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria.")]
    [Display(Name = "Categoría")]
    [ForeignKey(nameof(IdCategoriaNavigation))]
    public int IdCategoria { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "Número de teléfono no válido.")]
    [StringLength(50, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 50 caracteres.")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = null!;

    [Required(ErrorMessage = "El nombre del docente es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Docente")]
    public string NomDocente { get; set; } = null!;

    [Required(ErrorMessage = "El RUT es obligatorio.")]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "El RUT debe tener entre 7 y 20 caracteres.")]
    [Display(Name = "Rut")]
    public string Rut { get; set; } = null!;

    [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido paterno debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Apellido Paterno")]
    public string PatDocente { get; set; } = null!;

    [Required(ErrorMessage = "El apellido materno es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido materno debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Apellido Materno")]
    public string MatDocente { get; set; } = null!;

    [Required(ErrorMessage = "El estado civil es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El estado civil debe tener entre 3 y 50 caracteres.")]
    [Display(Name = "Estado Civil")]
    public string EstadoCivil { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha Nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La nacionalidad es obligatoria.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La nacionalidad debe tener entre 2 y 50 caracteres.")]
    [Display(Name = "Nacionalidad")]
    public string Nacionalidad { get; set; } = null!;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 50 caracteres.")]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
    [StringLength(50, ErrorMessage = "El correo no puede exceder 50 caracteres.")]
    [Display(Name = "Correo")]
    public string? Correo { get; set; }

    [InverseProperty(nameof(AppDocentes.Models.Dato.IdDocentesNavigation))]
    public virtual ICollection<Dato> Datos { get; set; } = new List<Dato>();

    [InverseProperty(nameof(AppDocentes.Models.Categoria.Docentes))]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    [InverseProperty(nameof(AppDocentes.Models.MovDocente.IdDocenteNavigation))]
    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
