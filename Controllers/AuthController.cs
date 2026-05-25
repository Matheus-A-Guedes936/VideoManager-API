using VideoManager_API.DTOs.Usuarios;
using VideoManager_API.Interface.IServices;
using VideoManager_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseModel<string>>> Login(UsuarioLoginDto loginDto)
        {
            var response = await _usuarioService.Login(loginDto);
            if (!response.Status)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
