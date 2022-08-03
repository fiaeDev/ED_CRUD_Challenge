using Data.Entities;
using Data.Helper;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eppendorf_CRUD_Frontend.PageModels
{
    public class DevicePageModel : PageModel
    {

        protected readonly IDeviceRepository _deviceRepo;
        protected readonly DataHelper _dataHelper;

        public List<DeviceHealth> HealthStates { get; set; }
        public List<DeviceType> DeviceTypes { get; set; }

        [BindProperty]
        public Device Device { get; set; }

        public DevicePageModel(IDeviceRepository deviceRepo, DataHelper dataHelper)
        {
            _deviceRepo = deviceRepo;
            _dataHelper = dataHelper;
        }

        public async Task InitHealtsAndTypes()
        {
            HealthStates = await _dataHelper.GetDeviceHealthsAsync();
            DeviceTypes = await _dataHelper.GetDeviceTypesAsync();
        }

        public string GetHealthName(int id)
        {
            return HealthStates?.Find(t => t.Id == id)?.Status;
        }

        public string GetTypeName(int id)
        {
            return DeviceTypes?.Find(t => t.Id == id)?.Name;
        }
    }
}
