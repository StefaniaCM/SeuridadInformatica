using System.ComponentModel.DataAnnotations;

namespace GestionSchool.Models
{
    public class LoginViewModel
    {
        [Required]

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{5,}$", ErrorMessage = "El nombre de usuario debe tener al menos una letra mayúscula, un número y al menos 5 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe tener al menos una letra, un número, un carácter especial y al menos 8 caracteres.")]
        public string Contrasena { get; set; }
    }
}
