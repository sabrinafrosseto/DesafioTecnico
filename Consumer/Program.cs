using Business;
using Consumer;
using Data;
using Domain.Entidades;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Text.Json;

var logger = CreateLogger();

var receive = new Receive();

var onMessageReceived = (string message) => 
{
	try
	{
		var protocol = JsonSerializer.Deserialize<Protocol>(message);

		var protocolData = new ProtocolData();

		var protocolBusiness = new ProtocolBusiness(protocolData);

		protocolBusiness.Create(protocol!);
	}
	catch (Exception ex)
	{
		logger.Error(ex, "ERRO");
    }
};

await Task.Run(() => receive.ReceiveMessage(onMessageReceived));

static Logger CreateLogger()
{
    return new LoggerConfiguration()
                    .MinimumLevel.Information()                    
					.WriteTo.Console()
					.WriteTo.File(path: "Logs\\Log.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();
}