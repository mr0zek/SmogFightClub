using Dapper;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using SFC.Sensors.Features.RegisterSensor.Contract;

namespace SFC.Sensors.Features.RegisterSensor
{
    internal class RegisterSensorHandler : ICommandHandler<RegisterSensorCommand>
  {
    private readonly ISensorRepository _sensorRepository;

    public RegisterSensorHandler(ISensorRepository sensorRepository)
    {      
      _sensorRepository = sensorRepository;
    }

    public void Handle(RegisterSensorCommand command)
    {
      if(_sensorRepository.Exits(command.Id))
      {
        throw new SensorAlreadyExistsException(command.Id);
      }
      _sensorRepository.Add(command.Id, command.ZipCode, command.LoginName);
    }
  }
}