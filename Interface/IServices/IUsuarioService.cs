using VideoManager_API.DTOs.Usuarios;
using VideoManager_API.Model;

namespace VideoManager_API.Interface.IServices
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioRespostaDto>> ObterTodos();
        Task<ResponseModel<string>> Login(UsuarioLoginDto loginDto);
        Task<UsuarioRespostaDto> ObterUsuarioPorId(int id);
        Task<UsuarioRespostaDto> ObterUsuarioPorEmail(string email);
        Task<IEnumerable<UsuarioRespostaDto>> ObterUsuarioPorNome(string nome);
        Task<UsuarioRespostaDto> AdicionarUsuario(UsuarioCadastroDto cadastroDto);
        Task<UsuarioRespostaDto> EditarUsuario(int id, UsuarioAtualizacaoDto usuarioAtualizacaoDto);
        Task<bool> RemoverUsuario(int id);


    }
}
