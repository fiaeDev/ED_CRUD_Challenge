using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IDeviceTypeRepository
    {
        Task<List<DeviceType>> GetAllDeviceTypes();
        Task<DeviceType> GetDeviceTypeByIdAsync(int id);
        Task<DeviceType> GetDeviceTypeByNameAsync(string name);
        Task<DeviceType> InsertDeviceType(DeviceType type);
        Task InsertListOfDeviceTypes(List<DeviceType> typeList);
    }
}