using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Data.Helper;
using Eppendorf_CRUD_Frontend.PageModels;

namespace Eppendorf_CRUD_Frontend.Pages.DevicePages
{
    public class EditModel : DevicePageModel
    {
        public EditModel(IDeviceRepository deviceRepo, DataHelper dataHelper): base(deviceRepo, dataHelper)
        {
           
        }       

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var device = await _deviceRepo.GetDeviceByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            await InitHealtsAndTypes();

            Device = device;
            ViewData["HealthId"] = new SelectList(HealthStates, "Id", "Status");
            ViewData["TypeId"] = new SelectList(DeviceTypes, "Id", "Name");
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _deviceRepo.UpdateDeviceAsync(Device);

            return RedirectToPage("/Index");
        }


    }
}
