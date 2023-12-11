using Domain.Entidades;
using Domain.Interfaces;

namespace Business
{
    public class ProtocolBusiness : IProtocolBusiness
    {
        private readonly IProtocolData _data;

        public ProtocolBusiness(IProtocolData data)
        {
            _data = data;
        }

        public void Create(Protocol protocol)
        {
            ValidateRequiredFields(protocol);
            ValidateUniqueProtocolNumber(protocol.ProtocolNumber);
            ValidateUniquePersonalId(protocol.PersonalId, protocol.DocumentVersion);
            _data.Create(protocol);
        }

        public List<Protocol> Find(string? protocol = null, string? cpf = null, string? rg = null)
        {
            return _data.Find(protocol, cpf, rg);
        }

        static void ValidateRequiredFields(Protocol protocol)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrEmpty(protocol.ProtocolNumber))
                missingFields.Add("Número do Protocolo");

            if (string.IsNullOrEmpty(protocol.DocumentVersion))
                missingFields.Add("Número da Via do Documento");

            if (string.IsNullOrEmpty(protocol.CPF))
                missingFields.Add("CPF");

            if (string.IsNullOrEmpty(protocol.PersonalId))
                missingFields.Add("RG");

            if (string.IsNullOrEmpty(protocol.Name))
                missingFields.Add("Nome");

            if (string.IsNullOrEmpty(protocol.Image))
                missingFields.Add("Foto");

            if (missingFields.Count > 0)
            {
                var message = ($"Os seguintes campos não foram preenchidos e são obrigatórios: {string.Join(", ", missingFields)}");
                throw new ApplicationException(message);
            }
        }

        private void ValidateUniqueProtocolNumber(string protocolNumber)
        {
            var existingProtocolCount = _data.Find(protocol: protocolNumber);
            if (existingProtocolCount.Any())
            {
                throw new ApplicationException($"Protocol number '{protocolNumber}' already exists.");
            }
        }

        private void ValidateUniquePersonalId(string personalId, string documentVersion)
        {
            var existingPersonalIdCount = _data.Find(rg: personalId);
            if (existingPersonalIdCount.Any(a => a.DocumentVersion == documentVersion))
            {
                throw new ApplicationException($"RG '{personalId}' already exists for document version '{documentVersion}'.");
            }
        }
    }
}
