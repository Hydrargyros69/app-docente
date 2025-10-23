using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocentes.Models;

public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [Required]
    [StringLength(100)]
    public string NomUsuario { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string CorrUsuario { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string PassUsuario { get; set; } = null!;
}
