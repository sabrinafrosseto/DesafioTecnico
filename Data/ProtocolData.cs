using Domain.Entidades;
using Domain.Interfaces;
using MySql.Data.MySqlClient;
using System.Net.Http;

namespace Data
{
    public class ProtocolData : IProtocolData
    {
        private const string ConnectionString = @"server=mysql; port= 3306;user id=root;password=dbpassword;database=DesafioTecnico;";

        public void Create(Protocol protocol)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            {
                var command = connection.CreateCommand();
                command.CommandText =
                            @"
                          INSERT INTO Protocols (
                          ProtocolNumber,
                          DocumentVersion,
                          CPF,
                          PersonalId,
                          Name,
                          MotherName,
                          FatherName,
                          Image
                      )
                      VALUES (
                          @ProtocolNumber,
                          @DocumentVersion,
                          @CPF,
                          @PersonalId,
                          @Name,
                          @MotherName,
                          @FatherName,
                          @Image
                      );
                            ";
                command.Parameters.AddWithValue("@ProtocolNumber", protocol.ProtocolNumber);
                command.Parameters.AddWithValue("@DocumentVersion", protocol.DocumentVersion);
                command.Parameters.AddWithValue("@CPF", protocol.CPF);
                command.Parameters.AddWithValue("@PersonalId", protocol.PersonalId);
                command.Parameters.AddWithValue("@Name", protocol.Name);
                command.Parameters.AddWithValue("@MotherName", protocol.MotherName);
                command.Parameters.AddWithValue("@FatherName", protocol.FatherName);
                command.Parameters.AddWithValue("@Image", protocol.Image);

                command.ExecuteNonQuery();
            }
        }

        public List<Protocol> Find(string? protocol = null, string? cpf = null, string? rg = null)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            {
                var command = connection.CreateCommand();

                command.CommandText = @"
                SELECT 
                    ProtocolNumber,
                    DocumentVersion,
                    CPF,
                    PersonalId,
                    Name,
                    MotherName,
                    FatherName,
                    Image,
                    ID
                FROM 
                    Protocols
                WHERE
                    1 = 1 ";

                if (!string.IsNullOrEmpty(protocol))
                    command.CommandText += "AND ProtocolNumber = @ProtocolNumber ";

                if (!string.IsNullOrEmpty(cpf))
                    command.CommandText += "AND CPF = @CPF ";

                if (!string.IsNullOrEmpty(rg))
                    command.CommandText += "AND PersonalId = @PersonalId ";

                if (!string.IsNullOrEmpty(protocol))
                    command.Parameters.AddWithValue("@ProtocolNumber", protocol);

                if (!string.IsNullOrEmpty(cpf))
                    command.Parameters.AddWithValue("@CPF", cpf);

                if (!string.IsNullOrEmpty(rg))
                    command.Parameters.AddWithValue("@PersonalId", rg);

                var retorno = new List<Protocol>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Protocol();

                        item.ProtocolNumber = reader["ProtocolNumber"].ToString()!;
                        item.DocumentVersion = reader["DocumentVersion"].ToString()!;
                        item.CPF = reader["CPF"].ToString()!;
                        item.PersonalId = reader["PersonalId"].ToString()!;
                        item.Name = reader["Name"].ToString()!;
                        item.MotherName = reader["MotherName"].ToString()!;
                        item.FatherName = reader["FatherName"].ToString()!;
                        item.Image = reader["image"].ToString()!;
                        item.ID = reader["ID"].ToString()!;

                        retorno.Add(item);
                    }
                }

                return retorno;
            }
        }
    }
}
