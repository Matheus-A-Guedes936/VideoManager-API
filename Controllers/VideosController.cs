using VideoManager_API.DTOs.Videos;
using VideoManager_API.Interface.IServices;
using VideoManager_API.Model;
using VideoManager_API.Model.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace VideoManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [Authorize]
        [HttpGet("BuscarTodosVideos")]
        public async Task<ActionResult<ResponseModel<List<VideosRespostaDto>>>> BuscarTodosVideos()
        {
            var videos = await _videosService.BuscarTodosVideos();
            return Ok(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = videos.ToList(),
                Mensagem = "Todos os vídeos foram buscados com sucesso.",
                Status = true
            });
        }

        [Authorize]
        [HttpGet("BuscarVideoPorID/{videoID}")]
        public async Task<ActionResult<ResponseModel<VideosRespostaDto>>> BuscarVideoPorId(int videoID)
        {
            var video = await _videosService.BuscarVideoPorId(videoID);
            if (video == null) return NotFound(new ResponseModel<VideosRespostaDto>
            {
                Dados = null,
                Mensagem = $"Vídeo com ID {videoID} não encontrado.",
                Status = false
            });
            return Ok(new ResponseModel<VideosRespostaDto>
            {
                Dados = video,
                Mensagem = "Vídeo buscado com sucesso.",
                Status = true
            });
        }

        [Authorize]
        [HttpGet("BuscarVideosPorTitulo/{titulo}")]
        public async Task<ActionResult<ResponseModel<List<VideosRespostaDto>>>> BuscarVideosPorTitulo(string titulo)
        {
            var videos = await _videosService.BuscarVideosPorTitulo(titulo);
            if (videos == null || !videos.Any()) return NotFound(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = null,
                Mensagem = $"Nenhum vídeo encontrado com o título '{titulo}' encontrado.",
                Status = false
            });
            return Ok(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = videos.ToList(),
                Mensagem = "Vídeos buscados com sucesso.",
                Status = true
            });
        }

        [Authorize]
        [HttpGet("BuscarVideosPorCategoria/{categoria}")]
        public async Task<ActionResult<ResponseModel<List<VideosRespostaDto>>>> BuscarVideosPorCategoria(CategoriaVideo categoria)
        {
            var videos = await _videosService.BuscarVideosPorCategoria(categoria);
            if (videos == null || !videos.Any()) return NotFound(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = null,
                Mensagem = $"Nenhum vídeo encontrado com a categoria '{categoria}' encontrado.",
                Status = false
            });
            return Ok(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = videos.ToList(),
                Mensagem = "Vídeos buscados com sucesso.",
                Status = true
            });
        }

        [Authorize]
        [HttpGet("BuscarVideosPorUsuarioID/{usuarioID}")]
        public async Task<ActionResult<ResponseModel<List<VideosRespostaDto>>>> BuscarVideosPorUsuarioID(int usuarioID)
        {
            var videos = await _videosService.BuscarVideoPorUsuarioID(usuarioID);
            if (videos == null || !videos.Any()) return NotFound(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = null,
                Mensagem = $"Nenhum vídeo encontrado para o usuário com ID {usuarioID}.",
                Status = false
            });
            return Ok(new ResponseModel<List<VideosRespostaDto>>
            {
                Dados = videos.ToList(),
                Mensagem = "Vídeos buscados com sucesso.",
                Status = true
            });
        }

        [Authorize]
        [HttpPost("AdicionarVideo")]
        public async Task<ActionResult<ResponseModel<VideosRespostaDto>>> AdicionarVideo([FromForm] VideosCriacaoDto adicionarVideoDto)
        {
            {
                try
                {
                    var video = await _videosService.AdicionarVideo(adicionarVideoDto);
                    return Ok(new ResponseModel<VideosRespostaDto>
                    {
                        Dados = video,
                        Mensagem = "Vídeo adicionado com sucesso.",
                        Status = true
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new ResponseModel<VideosRespostaDto>
                    {
                        Dados = null,
                        Mensagem = ex.Message,
                        Status = false
                    });
                }
            }
        }

        [Authorize]
        [HttpPut("EditarVideo/{videoId}")]
        public async Task<ActionResult<ResponseModel<VideosRespostaDto>>> EditarVideo(VideosAtualizacaoDto atualizarVideoDto, int videoId)
        {
            var video = await _videosService.EditarVideo(atualizarVideoDto, videoId);
            if (video == null) return BadRequest(new ResponseModel<VideosRespostaDto>
            {
                Dados = null,
                Mensagem = "Não foi possível editar o vídeo.",
                Status = false
            });
            return Ok(new ResponseModel<VideosRespostaDto>
            {
                Dados = video,
                Mensagem = "Vídeo editado com sucesso.",
                Status = true
            });
        }

        [Authorize]
        [HttpDelete("RemoverVideo/{videoId}")]
        public async Task<ActionResult<ResponseModel<string>>> RemoverVideo(int videoId)
        {
            var resultado = await _videosService.RemoverVideo(videoId);
            if (!resultado) return NotFound(new ResponseModel<string>
            {
                Dados = null,
                Mensagem = $"Vídeo com ID {videoId} não encontrado.",
                Status = false
            });
            return Ok(new ResponseModel<string>
            {
                Dados = null,
                Mensagem = "Vídeo removido com sucesso.",
                Status = true
            });
        }
    }
}