using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Entities;
using Eppendorf_CRUD_Frontend.PageModels;
using Data.Repositories;
using Data.Helper;

namespace Eppendorf_CRUD_Frontend.Pages.DevicePages
{
    public class IndexModel : DevicePageModel
    {      

        public IndexModel(IDeviceRepository deviceRepo, DataHelper dataHelper) : base(deviceRepo, dataHelper)
        {

        }

        public IList<Device> Devices { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Devices = await _deviceRepo.GetAllDevicesAsync();
            await InitHealtsAndTypes();
        }

       
    }
}
