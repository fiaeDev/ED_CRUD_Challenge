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
    public class DeviceRepository : IDeviceRepository
    {
        private readonly EDBContext _context;

        public DeviceRepository(EDBContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices.Include(d => d.Health).Include(d => d.Type).ToListAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices.Include(d => d.Health).Include(d => d.Type).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Device> InsertDeviceAsync(Device device)
        {
            var insert = await _context.AddAsync(device);
            await _context.SaveChangesAsync();
            return insert.Entity;
        }

        public async Task InsertListOfDevicesAsync(List<Device> deviceList)
        {
            await _context.AddRangeAsync(deviceList);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDeviceAsync(Device device)
        {
            var modified = _context.Devices.Update(device);
            await _context.SaveChangesAsync();
            return modified.State == EntityState.Modified;
        }

        public async Task<bool> DeleteDeviceAsync(Device device)
        {
            var deleted = _context.Devices.Remove(device);
            var res = await _context.SaveChangesAsync();
            return deleted.State == EntityState.Deleted;
        }

    }
}
