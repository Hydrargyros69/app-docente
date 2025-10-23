using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Carrera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código")]
    public int IdCarrera { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string NomCarrera { get; set; } = null!;

    [InverseProperty("IdCarreraNavigation")]
    public virtual ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();
}
