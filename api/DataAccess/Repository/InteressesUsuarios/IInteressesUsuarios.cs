namespace tccenter.api.DataAccess.Repository.InteressesUsuarios
{
    public interface IInteressesUsuarios
    {
        int CadastrarTopicoInteressante(int idUsuario, int idTopicoMestre, int idTopicoInteresse);
        void DeletarInteressesUsuario(int idUsuario);
    }
}