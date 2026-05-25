using VideoManager_API.Model;
using VideoManager_API.Model.Enums;

namespace VideoManager_API.Interface.IRepository
{
    public interface IVideosRepository
    {
        Task<List<VideosModel>> BuscarTodosVideos();
        Task<VideosModel?> BuscarVideoPorId(int videoID);
        Task<List<VideosModel>> BuscarVideosPorTitulo(string videoNome);
        Task<List<VideosModel>> BuscarVideosPorCategoria(CategoriaVideo categoria);
        Task<List<VideosModel>> BuscarVideoPorUsuarioID(int usuarioID);
        Task<VideosModel> AdicionarVideo(VideosModel video);
        Task<VideosModel> EditarVideo(VideosModel video);
        Task RemoverVideo(VideosModel video);
    }
}
