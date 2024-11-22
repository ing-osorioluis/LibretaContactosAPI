using System.ComponentModel.DataAnnotations;

namespace LibretaContactosAPI.Models
{
    public class Contactos
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string ?nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string ?apellido { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no es válido.")]
        public string ?telefono { get; set; }
        
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string? correo { get; set; }
    }
}
