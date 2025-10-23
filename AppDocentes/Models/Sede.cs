using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Sede
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdSede { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Comuna")]
    public string Comuna { get; set; } = null!;

    [Required]
    [StringLength(200)]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = null!;

    [Required]
    [StringLength(100)]
    [Display(Name = "Sede")]
    public string NomSede { get; set; } = null!;

    [Required]
    [StringLength(100)]
    [Display(Name = "Ciudad")]
    public string NomCiudad { get; set; } = null!;

    public virtual ICollection<MovDocente> MovDocentes { get; set; } = new List<MovDocente>();
}
