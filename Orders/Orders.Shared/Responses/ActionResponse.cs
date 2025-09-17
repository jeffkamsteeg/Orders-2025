using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Responses;

public class ActionResponse<T> // Clase genérica para manejar respuestas de acciones.
{
    public bool WasSuccess { get; set; } // Indica si la acción fue exitosa.
    public string? Message { get; set; } // Mensaje opcional para describir el resultado de la acción.
    public T? Result { get; set; } // Resultado de la acción, puede ser nulo si no hay resultado.
}