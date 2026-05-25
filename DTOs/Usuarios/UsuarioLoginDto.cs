using System.ComponentModel.DataAnnotations;

namespace VideoManager_API.DTOs.Usuarios
{
    public class UsuarioLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Porfavor Digite Sua senha")]
        public string Senha { get; set; }
    }
}
