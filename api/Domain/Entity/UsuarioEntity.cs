﻿namespace tccenter.api.Domain.Entity
{
    public class UsuarioEntity
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Avatar { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Profissao { get; set; }
    }
}