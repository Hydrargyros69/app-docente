using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Grado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdGrado { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string NomGrado { get; set; } = null!;

    public virtual ICollection<Dato> Datos { get; set; } = new List<Dato>();
}
