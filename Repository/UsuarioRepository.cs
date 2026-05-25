using VideoManager_API.Data;
using VideoManager_API.Interface.IRepository;
using VideoManager_API.Model;
using Microsoft.EntityFrameworkCore;

namespace VideoManager_API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<UsuarioModel>> ObterTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel?> ObterUsuarioPorId(int usuarioID)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioID);
        }

        public async Task<List<UsuarioModel>> ObterUsuarioPorNome(string usuarioNome)
        {
            var busca = usuarioNome.Trim();
            return await _context.Usuarios.Where(u => u.Nome.Contains(busca)).ToListAsync();
        }

        public async Task<UsuarioModel?> ObterUsuarioPorEmail(string usuarioEmail)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioEmail);
        }

        public async Task<UsuarioModel?> AdicionarUsuario(UsuarioModel usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> EditarUsuario( UsuarioModel usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task RemoverUsuario(UsuarioModel usuario)
        {
            _context.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
