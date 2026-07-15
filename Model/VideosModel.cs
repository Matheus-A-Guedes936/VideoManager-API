using VideoManager_API.Model.Enums;

namespace VideoManager_API.Model
{
    public class VideosModel
    {
        public UsuarioModel Usuario { get; set; }

        public int UsuarioID { get; set; }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public CategoriaVideo Categoria { get; set; }
        public string? CaminhoVideo { get; set; }
        public string? CaminhoVideoThumbnail { get; set; }
    }
}
