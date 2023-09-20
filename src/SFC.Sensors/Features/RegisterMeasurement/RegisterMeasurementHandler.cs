using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Command;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  internal class RegisterMeasurementHandler : ICommandHandler<RegisterMeasurementCommand>
  {
    private readonly IMeasurementRepository _measurementRepository;
    private readonly ISensorRepository _sensorRepository;

    public RegisterMeasurementHandler(IMeasurementRepository measurementRepository, ISensorRepository sensorRepository)
    {
      _measurementRepository = measurementRepository;
      _sensorRepository = sensorRepository;
    }

    public void Handle(RegisterMeasurementCommand command)
    {
      if (_sensorRepository.Exits(command.SensorId))
      {
        throw new UnknownSensorException(command.SensorId);
      }
      foreach (var element in command.Elements)
      {
        _measurementRepository.Add(command.SensorId, command.Date, element.Key, element.Value);
      }
    }
  }
}