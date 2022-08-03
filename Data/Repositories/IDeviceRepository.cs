using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IDeviceRepository
    {
        Task<bool> DeleteDeviceAsync(Device device);
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task<Device> InsertDeviceAsync(Device device);
        Task InsertListOfDevicesAsync(List<Device> deviceList);
        Task<bool> UpdateDeviceAsync(Device device);
    }
}