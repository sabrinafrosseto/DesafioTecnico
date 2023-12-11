using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IProtocolData
    {
        void Create(Protocol protocol);
        List<Protocol> Find(string? protocol = null, string? cpf = null, string? rg = null);
    }
}