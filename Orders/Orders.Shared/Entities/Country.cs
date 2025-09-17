using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities;

public class Country // Clase que representa un país.
{
    public int Id { get; set; } // Identificador único del país.

    [Display(Name = "País")] // Nombre para mostrar en la interfaz de usuario.
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")] // Máximo 100 caracteres.
    [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Campo obligatorio.
    public string Name { get; set; } = null!; // Nombre del país.
}