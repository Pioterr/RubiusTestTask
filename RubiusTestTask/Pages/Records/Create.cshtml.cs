using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RubiusTestTask.Data;
using RubiusTestTask.Models;

namespace RubiusTestTask.Pages.Records
{
    public class CreateModel : PageModel
    {
        private readonly RubiusTestTask.Data.RecordContext _context;

        public CreateModel(RubiusTestTask.Data.RecordContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Record Record { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyRecord = new Record();
            
            if (await TryUpdateModelAsync<Record>(
                emptyRecord,
                "record",
                s => s.Comments, s => s.Project, s => s.Date))
            {
                _context.Records.Add(emptyRecord);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return null;
        }
    }
}