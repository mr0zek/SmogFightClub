using SFC.SharedKernel;
using System;

namespace SFC.Sensors.Infratructure
{
    internal interface ISensorRepository
    {
        void Add(Guid sensorId, ZipCode zipCode, LoginName loginName);
        bool Exits(Guid sensorId);
    }
}