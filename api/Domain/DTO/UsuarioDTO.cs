using System.Collections.Generic;

namespace tccenter.api.Domain.DTO
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Profissao { get; set; }
        public List<InteressesUsuariosDTO> InteressesUsuario { get; set; }
    }
}