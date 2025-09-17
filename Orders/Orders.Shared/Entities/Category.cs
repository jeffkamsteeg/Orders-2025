using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Entities;

public class Category // Entidad que representa una categoría de productos.
{
    public int Id { get; set; } //Identificador único de la categoría.

    [Display(Name = "Categoría")] // Nombre de la categoría que se mostrará en la interfaz de usuario.
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} carácter.")] // Longitud máxima del nombre de la categoría.
    [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Validación para asegurar que el nombre de la categoría es obligatorio.
    public string Name { get; set; } = null!; // Nombre de la categoría, no puede ser nulo.
}