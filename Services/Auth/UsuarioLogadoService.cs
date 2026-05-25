using VideoManager_API.Interface.IServices.IAuth;

namespace VideoManager_API.Services.Auth
{
    public class UsuarioLogadoService : IUsuarioLogadoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioLogadoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetUsuarioId()
        {
            var usuario = _httpContextAccessor.HttpContext?.User;

            if(usuario == null || !usuario.Identity.IsAuthenticated)
            {
                return null;
            }

            var usuarioIdClaim = usuario.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim.Value, out int usuarioId))
            {
                return usuarioId;
            }

            return null;
        }
    }
}
