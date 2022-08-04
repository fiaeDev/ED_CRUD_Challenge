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
       

        public IFormFile DataFile { get; set; }

        public int DeviceCount { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDeviceRepository deviceRepo)
        {
            _logger = logger;            
            _deviceRepo = deviceRepo;
        }

        public async Task OnGet()
        {
            Devices = await _deviceRepo.GetAllDevicesAsync();           
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

                    if(deviceData == null)
                    {
                        return Page();
                    }
                    await _dataHelper.PropagateDeviceTypesAsync(deviceData);
                    await _dataHelper.PropagateDeviceHealthStatesAsync(deviceData);
                    await _dataHelper.PropagateDevices(deviceData);
                }            
                
            }
            return RedirectToPage("/Index");
        }


    }
}