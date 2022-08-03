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
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        private readonly EDBContext _context;

        public DeviceTypeRepository(EDBContext context)
        {
            _context = context;
        }

        public async Task<List<DeviceType>> GetAllDeviceTypes()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

        public async Task<DeviceType> GetDeviceTypeByIdAsync(int id)
        {
            return await _context.DeviceTypes.FindAsync(id);
        }

        public async Task<DeviceType> GetDeviceTypeByNameAsync(string name)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<DeviceType> InsertDeviceType(DeviceType type)
        {
            var insert = await _context.DeviceTypes.AddAsync(type);
            await _context.SaveChangesAsync();
            return insert.Entity;
        }

        public async Task InsertListOfDeviceTypes(List<DeviceType> typeList)
        {
            await _context.DeviceTypes.AddRangeAsync(typeList);
            await _context.SaveChangesAsync();

        }
    }
}
