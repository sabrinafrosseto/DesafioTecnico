using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IProtocolBusiness
    {
        void Create(Protocol protocol);
        List<Protocol> Find(string? protocol = null, string? cpf = null, string? rg = null);
    }
}