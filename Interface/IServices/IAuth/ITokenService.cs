using VideoManager_API.Model;

namespace VideoManager_API.Interface.IServices.IAuth
{
    public interface ITokenService
    {
        string GerarToken(UsuarioModel usuario);
    }
}
