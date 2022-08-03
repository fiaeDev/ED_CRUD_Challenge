using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IDeviceHealthRepository
    {
        Task<List<DeviceHealth>> GetAllHealtStatesAsync();
        Task<DeviceHealth> GetHealtStateByIdAsync(string id);
        Task<DeviceHealth> GetHealtStateByStatusNameAsync(string statusName);
        Task<DeviceHealth> InsertHealthState(DeviceHealth state);
        Task InsertListOfHealthStates(List<DeviceHealth> stateList);
    }
}