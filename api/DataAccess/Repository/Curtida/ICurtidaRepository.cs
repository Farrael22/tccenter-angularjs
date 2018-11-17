namespace tccenter.api.DataAccess.Repository.Curtida
{
    public interface ICurtidaRepository
    {
        int VerificarPublicacaoCurtida(int idUsuario, int idPublicacao);
        void DescurtirPublicacao(int idUsuario, int idPublicacao);
        int CurtirPublicacao(int idUsuario, int idPublicacao);
    }
}