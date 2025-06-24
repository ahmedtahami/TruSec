using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client;
using System.Text;
using System.Text.Json;
using TruSec.BLL.DTOs;
using TruSec.DAL.DbContexts;
using TruSec.DAL.Entities;

namespace TruSec.Backend
{
    public class MqttListenerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public MqttListenerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();

            client.ApplicationMessageReceivedAsync += async e =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var dto = JsonSerializer.Deserialize<DeviceTelemetryDto>(json);

                    if (dto != null)
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        var device = await db.Devices.FirstOrDefaultAsync(d => d.SerialNumber == dto.DeviceSerial, stoppingToken);
                        if (device == null)
                        {
                            device = new Device
                            {
                                Id = Guid.NewGuid(),
                                SerialNumber = dto.DeviceSerial
                            };
                            db.Devices.Add(device);
                            await db.SaveChangesAsync(stoppingToken);
                        }

                        db.Telemetries.Add(new DeviceTelemetry
                        {
                            Id = Guid.NewGuid(),
                            DeviceId = device.Id,
                            Timestamp = dto.Timestamp,
                            Voltage = dto.Voltage,
                            Current = dto.Current,
                            FuseStatus = dto.FuseStatus
                        });

                        await db.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle error
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("test.mosquitto.org", 1883)
                .WithClientId("dotnet-mqtt-listener")
                .Build();

            await client.ConnectAsync(options, stoppingToken);
            await client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("factory/telemetry/+").Build());
        }
    }

}
