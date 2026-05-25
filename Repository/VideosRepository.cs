using VideoManager_API.Data;
using VideoManager_API.Interface.IRepository;
using VideoManager_API.Model;
using VideoManager_API.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace VideoManager_API.Repository
{
    public class VideosRepository : IVideosRepository
    {
        private readonly AppDbContext _context;

        public VideosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideosModel>> BuscarTodosVideos()
        {
           return await _context.Videos.ToListAsync();
        }

        public async Task<VideosModel?> BuscarVideoPorId(int videoID)
        {
            return await _context.Videos.FirstOrDefaultAsync(v => v.Id == videoID);
        }

        public async Task<List<VideosModel>> BuscarVideosPorTitulo(string videoNome)
        {
            var busca = videoNome.Trim();
            return await _context.Videos.Where(v => v.Titulo.Contains(busca)).ToListAsync();
        }

        public async Task<List<VideosModel>> BuscarVideosPorCategoria(CategoriaVideo categoria)
        {
            return await _context.Videos.Where(v => v.Categoria == categoria).ToListAsync();
        }

        public async Task<List<VideosModel>> BuscarVideoPorUsuarioID(int usuarioID)
        {
            return await _context.Videos.Where(v => v.UsuarioID == usuarioID).ToListAsync();
        }

        public async Task<VideosModel> AdicionarVideo(VideosModel video)
        {
            _context.Add(video);
            await _context.SaveChangesAsync();

            return video;
        }

        public async Task<VideosModel> EditarVideo(VideosModel video)
        {
            _context.Update(video);
            await _context.SaveChangesAsync();

            return video;
        }

        public async Task RemoverVideo(VideosModel video)
        {
            _context.Remove(video);
            await _context.SaveChangesAsync();
        }
    }
}
