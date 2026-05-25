using VideoManager_API.DTOs.Usuarios;
using VideoManager_API.Interface.IServices;
using VideoManager_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<ActionResult<ResponseModel<UsuarioRespostaDto>>> AdicionarUsuario(UsuarioCadastroDto cadastroDto)
        {
            var usuario = await _usuarioService.AdicionarUsuario(cadastroDto);
            if (usuario == null) return BadRequest(new ResponseModel<UsuarioRespostaDto>
            {
                Status = false,
                Mensagem = "Email já cadastrado",
                Dados = null
            });
            return Ok(new ResponseModel<UsuarioRespostaDto>
            {
                Status = true,
                Mensagem = "Usuário adicionado com sucesso",
                Dados = usuario
            });
        }

        [Authorize]
        [HttpGet("BuscarTodosUsuarios")]
        public async Task<ActionResult<ResponseModel<List<UsuarioRespostaDto>>>> ObterTodosUsuarios()
        {
            var usuarios = await _usuarioService.ObterTodos();
            return Ok(new ResponseModel<List<UsuarioRespostaDto>>
            {
                Status = true,
                Mensagem = "Usuários obtidos com sucesso",
                Dados = usuarios.ToList()
            });
        }

        [Authorize]
        [HttpGet("BuscarUsuarioPorID/{usuarioId}")]
        public async Task<ActionResult<ResponseModel<UsuarioRespostaDto>>> ObterUsuarioPorId(int usuarioId)
        {
            var usuario = await _usuarioService.ObterUsuarioPorId(usuarioId);
            if (usuario == null) return NotFound(new ResponseModel<UsuarioRespostaDto>
            {
                Status = false,
                Mensagem = "Usuário não encontrado",
                Dados = null
            });
            return Ok(new ResponseModel<UsuarioRespostaDto>
            {
                Status = true,
                Mensagem = "Usuário obtido com sucesso",
                Dados = usuario
            });
        }

        [Authorize]
        [HttpGet("BuscarUsuarioPorEmail/{usuarioEmail}")]
        public async Task<ActionResult<ResponseModel<UsuarioRespostaDto>>> ObterUsuarioPorEmail(string usuarioEmail)
        {
            var usuario = await _usuarioService.ObterUsuarioPorEmail(usuarioEmail);
            if (usuario == null) return NotFound(new ResponseModel<UsuarioRespostaDto>
            {
                Status = false,
                Mensagem = $"Usuário com email {usuarioEmail} não encontrado",
                Dados = null
            });
            return Ok(new ResponseModel<UsuarioRespostaDto>
            {
                Status = true,
                Mensagem = "Usuário obtido com sucesso",
                Dados = usuario
            });
        }
        [Authorize]
        [HttpGet("BuscarUsuarioPorNome/{usuarioNome}")]
        public async Task<ActionResult<ResponseModel<List<UsuarioRespostaDto>>>> ObterUsuarioPorNome(string usuarioNome)
        {
            var usuarios = await _usuarioService.ObterUsuarioPorNome(usuarioNome);
            if (usuarios == null || !usuarios.Any()) return NotFound(new ResponseModel<List<UsuarioRespostaDto>>
            {
                Status = false,
                Mensagem = $"Nenhum usuário com nome {usuarioNome} encontrado",
                Dados = null
            });
            return Ok(new ResponseModel<List<UsuarioRespostaDto>>
            {
                Status = true,
                Mensagem = "Usuários obtidos com sucesso",
                Dados = usuarios.ToList()
            });
        }

        [Authorize]
        [HttpPut("EditarUsuario/{usuarioId}")]
        public async Task<ActionResult<ResponseModel<UsuarioRespostaDto>>> EditarUsuario(int usuarioId, UsuarioAtualizacaoDto usuarioAtualizacaoDto)
        {
            var usuario = await _usuarioService.EditarUsuario(usuarioId, usuarioAtualizacaoDto);
            if (usuario == null) return NotFound(new ResponseModel<UsuarioRespostaDto>
            {
                Status = false,
                Mensagem = $"Usuário com ID {usuarioId} não encontrado ou email já cadastrado",
                Dados = null
            });
            return Ok(new ResponseModel<UsuarioRespostaDto>
            {
                Status = true,
                Mensagem = "Usuário editado com sucesso",
                Dados = usuario
            });
        }

        [Authorize]
        [HttpDelete("RemoverUsuario/{usuarioId}")]
        public async Task<ActionResult<ResponseModel<bool>>> RemoverUsuario(int usuarioId)
        {
            var resultado = await _usuarioService.RemoverUsuario(usuarioId);
            if (!resultado) return NotFound(new ResponseModel<bool>
            {
                Status = false,
                Mensagem = $"Usuário com ID {usuarioId} não encontrado",
                Dados = false
            });
            return Ok(new ResponseModel<bool>
            {
                Status = true,
                Mensagem = "Usuário removido com sucesso",
                Dados = true
            });
        }
    }
}