using VideoManager_API.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace VideoManager_API.DTOs.Videos
{
    public class VideosCriacaoDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MinLength(4, ErrorMessage ="O titulo deverá conter ao menos 4 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria válida.")]
        [EnumDataType(typeof(CategoriaVideo), ErrorMessage = "Categoria invalida.")]
        public CategoriaVideo Categoria { get; set; }

        [Required(ErrorMessage = "Arquivo invalido.")]
        public IFormFile ArquivoVideo { get; set; }
    }
}
