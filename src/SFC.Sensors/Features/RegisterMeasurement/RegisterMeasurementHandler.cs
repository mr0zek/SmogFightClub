using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using System.Collections.Generic;
using System.Linq;

namespace SFC.Sensors.Features.RegisterMeasurement
{
    internal class RegisterMeasurementHandler : ICommandHandler<RegisterMeasurementCommand>
  {
    private readonly IMeasurementRepository _measurementRepository;
    private readonly ISensorRepository _sensorRepository;
    private readonly IEventBus _eventBus;
    private readonly IEnumerable<IAcceptableNormsVerificator> _checkAcceptabelNorms;

    public RegisterMeasurementHandler(IMeasurementRepository measurementRepository, ISensorRepository sensorRepository, IEventBus eventBus, IEnumerable<IAcceptableNormsVerificator> checkAcceptabelNorms)
    {
      _measurementRepository = measurementRepository;
      _sensorRepository = sensorRepository;
      _eventBus = eventBus;
      _checkAcceptabelNorms = checkAcceptabelNorms;
    }

    public void Handle(RegisterMeasurementCommand command)
    {
      Sensor sensor = _sensorRepository.Get(command.SensorId);
      if (sensor == null)
      {
        throw new UnknownSensorException(command.SensorId);
      }
      foreach (var element in command.Elements)
      {
        _measurementRepository.Add(command.SensorId, command.Date, element.Key, element.Value);
      }

      if (!_checkAcceptabelNorms.All(f=>f.Verify(command.Elements)))
      {        
        _eventBus.Publish(new AcceptableLevelExceededEvent(sensor.ZipCode));
      }
    }
  }
}