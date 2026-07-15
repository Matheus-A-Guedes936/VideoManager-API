using VideoManager_API.DTOs.Videos;
using VideoManager_API.Interface.IRepository;
using VideoManager_API.Interface.IServices;
using VideoManager_API.Interface.IServices.IAuth;
using VideoManager_API.Model;
using VideoManager_API.Model.Enums;
using System.Runtime;

namespace VideoManager_API.Services
{
    public class VideosService : IVideosService
    {
        private readonly IVideosRepository _videosRepository;   
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public VideosService(IVideosRepository videosRepository , IUsuarioRepository usuarioRepository, IUsuarioLogadoService usuarioLogadoService)
        {
            _videosRepository = videosRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<IEnumerable<VideosRespostaDto>> BuscarTodosVideos()
        {
            var videosModel = await _videosRepository.BuscarTodosVideos();

            return MapeamentoTodosParaDto(videosModel);
        }

        public async Task<VideosRespostaDto?> BuscarVideoPorId(int videoID)
        {
            var videoModel = await _videosRepository.BuscarVideoPorId(videoID);
            if (videoModel == null)
                return null;
            return MapeamentoParaDto(videoModel);
        }  

        public async Task<IEnumerable<VideosRespostaDto>> BuscarVideosPorTitulo(string titulo)
        {
            var videoModel = await _videosRepository.BuscarVideosPorTitulo(titulo);
            return MapeamentoTodosParaDto(videoModel);
        }

        public async Task<IEnumerable<VideosRespostaDto>> BuscarVideosPorCategoria(CategoriaVideo categoria)
        {
            var videoModel = await _videosRepository.BuscarVideosPorCategoria(categoria);
            return MapeamentoTodosParaDto(videoModel);
        }

        public async Task<IEnumerable<VideosRespostaDto>> BuscarVideoPorUsuarioID(int usuarioID)
        {
            var videoModel = await _videosRepository.BuscarVideoPorUsuarioID(usuarioID);
            return MapeamentoTodosParaDto(videoModel);
        }

        public async Task<VideosRespostaDto?> AdicionarVideo(VideosCriacaoDto adicionarVideoDto)
        {
            int? usuarioLogado = _usuarioLogadoService.GetUsuarioId();

            if(!usuarioLogado.HasValue)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var usuarioExiste = await _usuarioRepository.ObterUsuarioPorId(usuarioLogado.Value);
            if (usuarioExiste == null)
            {
                throw new KeyNotFoundException($"O usuário com ID {usuarioLogado.Value} não existe no sistema.");
            }

            if (adicionarVideoDto.ArquivoVideo.Length < 1024) 
            {
                throw new BadHttpRequestException("O arquivo enviado é pequeno demais para ser um vídeo válido.");
            }

            if (adicionarVideoDto.ArquivoThumb.Length < 1024)
            {
                throw new BadHttpRequestException("O arquivo de thumbnail enviado é pequeno demais para ser válido.");
            }

            var pastaUsuario = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "videos",
                usuarioLogado.Value.ToString());

            var pastaThumbs = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "thumbnails",
                usuarioLogado.Value.ToString());

            if (!Directory.Exists(pastaUsuario)){
                Directory.CreateDirectory(pastaUsuario);
            }

            if (!Directory.Exists(pastaThumbs)){
                Directory.CreateDirectory(pastaThumbs);
            }

            var nomeArquivo = $"{Guid.NewGuid().ToString() + Path.GetExtension(adicionarVideoDto.ArquivoVideo.FileName)}";

            var caminhoCompleto = Path.Combine(pastaUsuario, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await adicionarVideoDto.ArquivoVideo.CopyToAsync(stream);
            }

            var nomeArquivoThumb = $"{Guid.NewGuid().ToString() + Path.GetExtension(adicionarVideoDto.ArquivoThumb.FileName)}";

            var caminhoCompletoThumb = Path.Combine(pastaThumbs, nomeArquivoThumb);

            using (var stream = new FileStream(caminhoCompletoThumb, FileMode.Create))
            {
                await adicionarVideoDto.ArquivoThumb.CopyToAsync(stream);
            }

            VideosModel videosModel = new VideosModel
            {
                Titulo = adicionarVideoDto.Titulo,
                Categoria = adicionarVideoDto.Categoria,
                UsuarioID = usuarioLogado.Value,
                CaminhoVideo = $"videos/{usuarioLogado.Value}/{nomeArquivo}",
                CaminhoVideoThumbnail = $"thumbnails/{usuarioLogado.Value}/{nomeArquivoThumb}"
            };

            var videoCriado = await _videosRepository.AdicionarVideo(videosModel);

            return MapeamentoParaDto(videoCriado);
        }

        public async Task<VideosRespostaDto> EditarVideo(VideosAtualizacaoDto atualizarVideoDto, int videoID)
        {
            var videoExistente = await _videosRepository.BuscarVideoPorId(videoID);
            if (videoExistente == null) return null;

            if (!string.IsNullOrWhiteSpace(atualizarVideoDto.Titulo))
            {
                videoExistente.Titulo = atualizarVideoDto.Titulo;
            }

            videoExistente.Categoria = atualizarVideoDto.Categoria;

            var videoAtualizado = await _videosRepository.EditarVideo(videoExistente);
            return MapeamentoParaDto(videoAtualizado);
        }

        public async Task<bool> RemoverVideo(int videoID)
        {
            var videoExistente = await _videosRepository.BuscarVideoPorId(videoID);
            if (videoExistente == null) return false;

            try
            {
                var caminhoRelativo = videoExistente.CaminhoVideo.TrimStart('/');
                var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativo);

                if (File.Exists(caminhoCompleto))
                {
                    File.Delete(caminhoCompleto);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Erro ao tentar excluir o arquivo de vídeo.", ex);
            }

            await _videosRepository.RemoverVideo(videoExistente);
            return true;
        }


        private IEnumerable<VideosRespostaDto> MapeamentoTodosParaDto(List<VideosModel> videos)
        {
            return videos.Select(video => MapeamentoParaDto(video)).ToList();
        }

        private VideosRespostaDto MapeamentoParaDto(VideosModel video)
        {
            return new VideosRespostaDto
            {
                Id = video.Id,
                Titulo = video.Titulo,
                Categoria = video.Categoria.ToString(),
                CaminhoVideo = video.CaminhoVideo,
                CaminhoVideoThumbnail = video.CaminhoVideoThumbnail,
                UsuarioID = video.UsuarioID
            }
            ;
        }
    }
}
