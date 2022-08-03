using Data.Entities;
using Data.Helper;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eppendorf_CRUD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataHelper _dataHelper;
        private readonly IDeviceRepository _deviceRepo;

        public List<Device> Devices { get; set; }
        public List<DeviceHealth> HealthStates { get; set; }
        public List<DeviceType> DeviceTypes { get; set; }

        public IFormFile DataFile { get; set; }

        public int DeviceCount { get; set; }

        public IndexModel(ILogger<IndexModel> logger,DataHelper dataHelper, IDeviceRepository deviceRepo)
        {
            _logger = logger;
            _dataHelper = dataHelper;
            _deviceRepo = deviceRepo;
        }

        public async Task OnGet()
        {
            Devices = await _deviceRepo.GetAllDevicesAsync();
            HealthStates = await _dataHelper.GetDeviceHealthsAsync();
            DeviceTypes = await _dataHelper.GetDeviceTypesAsync();
            DeviceCount = Devices.Count;
        }

        public async Task<IActionResult> OnPostUpload()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            using (var stream = DataFile.OpenReadStream())
            {
                using(StreamReader sr = new StreamReader(stream))
                {
                    var dataJson = sr.ReadToEnd();
                    var deviceData = JsonConvert.DeserializeObject<List<DeviceJson>>(dataJson);

                    await _dataHelper.PropagateDeviceTypesAsync(deviceData);
                    await _dataHelper.PropagateDeviceHealthStatesAsync(deviceData);
                    await _dataHelper.PropagateDevices(deviceData);
                }            
                
            }
            return RedirectToPage("/Index");
        }

        public string GetHealthName(int id)
        {
            return HealthStates.Find(t => t.Id == id)?.Status;
        }

        public string GetTypeName(int id)
        {
            return DeviceTypes.Find(t => t.Id == id)?.Name;
        }

    }
}