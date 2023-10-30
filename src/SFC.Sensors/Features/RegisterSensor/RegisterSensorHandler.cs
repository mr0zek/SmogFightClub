using Dapper;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using SFC.Sensors.Features.RegisterSensor.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Sensors.Features.RegisterSensor
{
  internal class RegisterSensorHandler : ICommandHandler<RegisterSensorCommand>
  {
    private readonly ISensorRepository _sensorRepository;

    public RegisterSensorHandler(ISensorRepository sensorRepository)
    {      
      _sensorRepository = sensorRepository;
    }

    public async Task Handle(RegisterSensorCommand command, CancellationToken cancellationToken)
    {
      if(await _sensorRepository.Exits(command.SensorId))
      {
        throw new SensorAlreadyExistsException(command.SensorId);
      }
      await _sensorRepository.Add(command.SensorId, command.ZipCode, command.LoginName);
    }
  }
}