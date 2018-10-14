using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Login
{
    public interface ILoginBusiness
    {
        int EfetuarLogin(LoginDTO infoLogin);
    }
}