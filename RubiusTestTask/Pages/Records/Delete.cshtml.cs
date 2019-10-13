using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RubiusTestTask.Data;
using RubiusTestTask.Models;

namespace RubiusTestTask.Pages.Records
{
    public class DeleteModel : PageModel
    {
        private readonly RubiusTestTask.Data.RecordContext _context;

        public DeleteModel(RubiusTestTask.Data.RecordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Record Record { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Record = await _context.Records
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Record == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (record == null)
            {
                return NotFound();
            }

            try
            {
                _context.Records.Remove(record);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id = id, saveChangesError = true });
            }
        }
    }
}
