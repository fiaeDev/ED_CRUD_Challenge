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
    public class DetailsModel : DevicePageModel
    {     
        public DetailsModel(IDeviceRepository deviceRepo, DataHelper dataHelper) : base(deviceRepo, dataHelper)
        {

        }      

        public async Task<IActionResult> OnGetAsync(int id)
        {  
            var device = await _deviceRepo.GetDeviceByIdAsync(id);
            
            if (device == null)
            {
                return NotFound();
            }
            else 
            {
                await InitHealtsAndTypes();
                Device = device;
                
            }
            return Page();
        }
    }
}
