using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class DeviceHealthRepository : IDeviceHealthRepository
    {
        private readonly EDBContext _context;

        public DeviceHealthRepository(EDBContext context)
        {
            _context = context;
        }

        public async Task<List<DeviceHealth>> GetAllHealtStatesAsync()
        {
            return await _context.DeviceHealths.ToListAsync();
        }

        public async Task<DeviceHealth> GetHealtStateByIdAsync(string id)
        {
            return await _context.DeviceHealths.FindAsync(id);
        }

        public async Task<DeviceHealth> GetHealtStateByStatusNameAsync(string statusName)
        {
            return await _context.DeviceHealths.FirstOrDefaultAsync(h => h.Status == statusName);
        }

        public async Task InsertListOfHealthStates(List<DeviceHealth> stateList)
        {
            await _context.DeviceHealths.AddRangeAsync(stateList);
            await _context.SaveChangesAsync();
        }

        public async Task<DeviceHealth> InsertHealthState(DeviceHealth state)
        {
            var insert = await _context.DeviceHealths.AddAsync(state);
            await _context.SaveChangesAsync();
            return insert.Entity;
        }
    }
}
