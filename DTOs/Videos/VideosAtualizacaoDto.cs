using VideoManager_API.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace VideoManager_API.DTOs.Videos
{
    public class VideosAtualizacaoDto
    {
        [MinLength(4, ErrorMessage = "O titulo deverá conter ao menos 4 caracteres")]
        public string? Titulo { get; set; }

        [EnumDataType(typeof(CategoriaVideo), ErrorMessage = "Categoria invalida.")]
        public CategoriaVideo Categoria { get; set; }
    }
}
