using VideoManager_API.Model;

namespace VideoManager_API.Interface.IRepository
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> ObterTodosUsuarios();
        Task<UsuarioModel?> ObterUsuarioPorId(int usuarioID);
        Task<List<UsuarioModel>> ObterUsuarioPorNome(string usuarioNome);
        Task<UsuarioModel?> ObterUsuarioPorEmail(string usuarioEmail);
        Task<UsuarioModel?> AdicionarUsuario(UsuarioModel usuario);
        Task<UsuarioModel> EditarUsuario(UsuarioModel usuario);
        Task RemoverUsuario(UsuarioModel usuario);
    }
}
