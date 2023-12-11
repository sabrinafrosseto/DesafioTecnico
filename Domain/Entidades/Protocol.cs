using System.Text.Json.Serialization;

namespace Domain.Entidades
{
    public class Protocol
    {
        [JsonPropertyName("numeroProtocolo")]
        public string ProtocolNumber { get; set; }
        [JsonPropertyName("numeroViaDocumento")]
        public string DocumentVersion { get; set; }
        [JsonPropertyName("cpf")]
        public string CPF { get; set; }
        [JsonPropertyName("rg")]
        public string PersonalId { get; set; }
        [JsonPropertyName("nomeCompleto")]
        public string Name { get; set; }
        [JsonPropertyName("nomeMae")]
        public string MotherName { get; set; }
        [JsonPropertyName("nomePai")]
        public string FatherName { get; set; }
        [JsonPropertyName("foto")]
        public string Image { get; set; }
        [JsonPropertyName("ID")]
        public string ID { get; set; }
    }
}
