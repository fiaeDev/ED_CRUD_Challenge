using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Data.Helper;
using Eppendorf_CRUD_Frontend.PageModels;

namespace Eppendorf_CRUD_Frontend.Pages.DevicePages
{
    public class CreateModel : DevicePageModel
    {     

        public CreateModel(IDeviceRepository deviceRepo, DataHelper dataHelper) : base(deviceRepo, dataHelper)
        {
            
        }

        public async Task<IActionResult> OnGet()
        {
            await InitHealtsAndTypes();
            ViewData["HealthId"] = new SelectList(HealthStates, "Id", "Status");
            ViewData["TypeId"] = new SelectList(DeviceTypes, "Id", "Name");
            return Page();
        }   

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _deviceRepo.InsertDeviceAsync(Device);

            return RedirectToPage("./Index");
        }
    }
}
