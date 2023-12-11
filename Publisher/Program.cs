using Domain.Entidades;
using Publisher;

var send = new Send();

for (int i = 0; i < 10; i++)
{
    var protocol = CreateProtocolModel();
    send.SendMessage(protocol);
}
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static Protocol CreateProtocolModel()
{
    return new Protocol
    {
        ProtocolNumber = DateTime.Now.ToString("ddMMyyyyHHmmssfff"),
        //ProtocolNumber = "06122023191004426",
        DocumentVersion = "1",
        CPF = CreateRandomNumber(11),
        PersonalId = CreateRandomNumber(8),
        //PersonalId = "30844270",
        Name = "Name",
        MotherName = "MotherName",
        FatherName = "FatherName",
        Image = "Image"
    };
}

static string CreateRandomNumber(int size)
{
    Random random = new Random();
    char[] numbers = new char[size];

    for (int i = 0; i < size; i++)
    {
        numbers[i] = (char)random.Next('0', '9' + 1);
    }

    return new string(numbers);
}