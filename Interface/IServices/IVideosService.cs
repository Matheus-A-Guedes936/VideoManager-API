using VideoManager_API.DTOs.Videos;
using VideoManager_API.Model.Enums;

namespace VideoManager_API.Interface.IServices
{
    public interface IVideosService
    {
        Task<IEnumerable<VideosRespostaDto>> BuscarTodosVideos();
        Task<VideosRespostaDto?> BuscarVideoPorId(int videoID);
        Task<IEnumerable<VideosRespostaDto>> BuscarVideosPorTitulo(string titulo);
        Task<IEnumerable<VideosRespostaDto>> BuscarVideosPorCategoria(CategoriaVideo categoria);
        Task<IEnumerable<VideosRespostaDto>> BuscarVideoPorUsuarioID(int usuarioID);
        Task<VideosRespostaDto?> AdicionarVideo(VideosCriacaoDto adicionarVideoDto);
        Task<VideosRespostaDto> EditarVideo(VideosAtualizacaoDto atualizarVideoDto, int videoID);
        Task<bool> RemoverVideo(int videoID);
    }
}
