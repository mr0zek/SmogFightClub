using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

    public async Task Handle(RegisterMeasurementCommand command, CancellationToken cancellationToken)
    {
      Sensor sensor = await _sensorRepository.Get(command.SensorId);
      if (sensor == null)
      {
        throw new UnknownSensorException(command.SensorId);
      }
      foreach (var element in command.Elements)
      {
        await _measurementRepository.Add(command.SensorId, command.Date, element.Key, element.Value);
      }

      if (!_checkAcceptabelNorms.All(f=>f.Verify(command.Elements)))
      {        
        await _eventBus.Publish(new AcceptableLevelExceededEvent(sensor.ZipCode));
      }
    }
  }
}