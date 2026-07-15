namespace VideoManager_API.DTOs.Videos
{
    public class VideosRespostaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Categoria { get; set; }

        public string CaminhoVideo { get; set; }

        public string CaminhoVideoThumbnail { get; set; }

        public int UsuarioID { get; set; }
    }
}
