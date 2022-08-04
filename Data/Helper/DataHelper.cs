using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public class DataHelper
    {
        private readonly IDeviceRepository _deviceRepo;
        private readonly IDeviceTypeRepository _typeRepo;
        private readonly IDeviceHealthRepository _healthRepo;
        public DataHelper(IDeviceRepository deviceRepo, IDeviceTypeRepository typeRepo, IDeviceHealthRepository healthRepo)
        {
            _deviceRepo = deviceRepo;
            _typeRepo = typeRepo;
            _healthRepo = healthRepo;
        }
        public async Task PropagateDeviceTypesAsync(List<DeviceJson> dataList)
        {
            if (dataList == null)
                throw new ArgumentNullException(nameof(dataList));

            var types = dataList.Select(d => d.type).Distinct();
            if (!types.Any())
                return;

            var existingTypes = await _typeRepo.GetAllDeviceTypes();
            if(existingTypes.Any())
            {
                var existingNames = existingTypes.Select(d => d.Name).Distinct();
                types = types.Except(existingNames);
            }           

            List<DeviceType> typesList = new List<DeviceType>();

            foreach (var type in types)
            {
                var typeEntity = new DeviceType()
                {
                    Name = type
                };
                typesList.Add(typeEntity);
            }
            if (typesList.Any())
            {
                await _typeRepo.InsertListOfDeviceTypes(typesList);
            }
        }

        public async Task PropagateDeviceHealthStatesAsync(List<DeviceJson> dataList)
        {
            if(dataList == null)
                throw new ArgumentNullException(nameof(dataList));

            var healthStates = dataList.Select(d => d.device_health).Distinct();
            if (!healthStates.Any())
                return;

            var existingHealths = await _healthRepo.GetAllHealtStatesAsync();
            if(existingHealths.Any())
            {
                var existingHealthsStates = existingHealths.Select(d => d.Status).Distinct();
                healthStates = healthStates.Except(existingHealthsStates);
            }          
          
            List<DeviceHealth> healthStatesList = new List<DeviceHealth>();
            foreach (var deviceHealth in healthStates)
            {
                var healthEntity = new DeviceHealth()
                {
                    Status = deviceHealth
                };
                healthStatesList.Add(healthEntity);
            }
            if (healthStatesList.Any())
            {
                await _healthRepo.InsertListOfHealthStates(healthStatesList);
            }
        }

        public async Task PropagateDevices(List<DeviceJson> dataList)
        {
            if (dataList == null)
                throw new ArgumentNullException(nameof(dataList));

            List<Device> DeviceEntities = new List<Device>();
            foreach (var deviceData in dataList)
            {
                var health = await _healthRepo.GetHealtStateByStatusNameAsync(deviceData.device_health);
                var type = await _typeRepo.GetDeviceTypeByNameAsync(deviceData.type);

                DeviceEntities.Add(new Device()
                {
                    Location = deviceData.location,
                    HealthId = health.Id,
                    TypeId = type.Id,
                    LastUsed = deviceData.last_used,
                    Price = deviceData.price,
                    ColorHexValue = deviceData.color
                });
            }

            if(DeviceEntities.Any())
            {
               await _deviceRepo.InsertListOfDevicesAsync(DeviceEntities);
            }
        }

        public async Task<List<DeviceHealth>> GetDeviceHealthsAsync()
        {
            return await _healthRepo.GetAllHealtStatesAsync();
        }

        public async Task<List<DeviceType>> GetDeviceTypesAsync()
        {
            return await _typeRepo.GetAllDeviceTypes();
        }
    }
}
