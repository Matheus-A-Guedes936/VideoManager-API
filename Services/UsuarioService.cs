using VideoManager_API.DTOs.Usuarios;
using VideoManager_API.Interface.IRepository;
using VideoManager_API.Interface.IServices;
using VideoManager_API.Interface.IServices.IAuth;
using VideoManager_API.Model;

namespace VideoManager_API.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;   

        public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<ResponseModel<string>> Login(UsuarioLoginDto loginDto)
            {
            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(loginDto.Email);

            if (usuario == null)
            {
                return new ResponseModel<string>
                {
                    Status = false,
                    Mensagem = "Email inválidos."
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.Senha))
            {
                return new ResponseModel<string>
                {
                    Status = false,
                    Mensagem = "Senha inválida."
                };
            }

            var token = _tokenService.GerarToken(usuario);

            return new ResponseModel<string>
            {
                Dados = token,
                Status = true,
                Mensagem = "Login realizado com sucesso."
            };
        }

        public async Task<IEnumerable<UsuarioRespostaDto>> ObterTodos()
        {
            var usuariosModel = await _usuarioRepository.ObterTodosUsuarios();

            return MapeamentoTodosParaDto(usuariosModel);
        }

        public async Task<UsuarioRespostaDto> ObterUsuarioPorId(int id)
        {
            var usuarioModel = await _usuarioRepository.ObterUsuarioPorId(id);

            if (usuarioModel == null) return null;

            return MapeamentoParaDto(usuarioModel);
        }

        public async Task<UsuarioRespostaDto> ObterUsuarioPorEmail(string email)
        {
            var usuarioModel = await _usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuarioModel != null) return MapeamentoParaDto(usuarioModel);

            return null;
        }

        public async Task<IEnumerable<UsuarioRespostaDto>> ObterUsuarioPorNome(string nome)
        {
            var usuariosModel = await _usuarioRepository.ObterUsuarioPorNome(nome);

            return MapeamentoTodosParaDto(usuariosModel);
            
        }

        public async Task<UsuarioRespostaDto> AdicionarUsuario(UsuarioCadastroDto cadastroDto)
        {
            var verificacaoDeEmail = await ObterUsuarioPorEmail(cadastroDto.Email);

            if (verificacaoDeEmail != null) return null;

            UsuarioModel usuarioModel = new UsuarioModel
            {
                Nome = cadastroDto.Nome,
                Email = cadastroDto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(cadastroDto.Senha)
            };

            var usuario = await _usuarioRepository.AdicionarUsuario(usuarioModel);

            return MapeamentoParaDto(usuario);

        }

        public async Task<UsuarioRespostaDto> EditarUsuario(int id, UsuarioAtualizacaoDto usuarioAtualizacaoDto)
        {
            var usuarioModel = await _usuarioRepository.ObterUsuarioPorId(id);

            if (usuarioModel == null) return null;

            if(!string.IsNullOrWhiteSpace(usuarioAtualizacaoDto.Nome))
            {
                usuarioModel.Nome = usuarioAtualizacaoDto.Nome;
            }

            if (!string.IsNullOrWhiteSpace(usuarioAtualizacaoDto.Email))
            {
                usuarioModel.Email = usuarioAtualizacaoDto.Email;
            }

            if(!string.IsNullOrWhiteSpace(usuarioAtualizacaoDto.SenhaAtual))
            {
                usuarioModel.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioAtualizacaoDto.SenhaAtual);
            }

            await _usuarioRepository.EditarUsuario(usuarioModel);

            return MapeamentoParaDto(usuarioModel);

        }

        public async Task<bool> RemoverUsuario(int id)
        {
            var usuarioModel = await _usuarioRepository.ObterUsuarioPorId(id);

            if (usuarioModel == null) return false;

            await _usuarioRepository.RemoverUsuario(usuarioModel);

            return true;

        }


        private UsuarioRespostaDto MapeamentoParaDto(UsuarioModel usuarioModel)
        {
            return new UsuarioRespostaDto
            {
                Id = usuarioModel.Id,
                Nome = usuarioModel.Nome,
                Email = usuarioModel.Email,
            };
        }

        private IEnumerable<UsuarioRespostaDto> MapeamentoTodosParaDto(List<UsuarioModel> usuariosModel)
        {
            return usuariosModel.Select(u => MapeamentoParaDto(u));
        }
    }
}
