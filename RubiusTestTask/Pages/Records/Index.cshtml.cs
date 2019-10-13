using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using RubiusTestTask.Models;

namespace RubiusTestTask.Pages.Records
{
    public class IndexModel : PageModel
    {
        private readonly RubiusTestTask.Data.RecordContext _context;

        public IndexModel(RubiusTestTask.Data.RecordContext context)
        {
            _context = context;
        }

        public PaginatedList<Record> Record { get; set; }
        public string ProjectSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder,
    string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            ProjectSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Record> recordIq = from s in _context.Records
                                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                recordIq = recordIq.Where(s => s.Project.Contains(searchString)
                                       || s.Comments.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    recordIq = recordIq.OrderByDescending(s => s.Project);
                    break;
                case "Date":
                    recordIq = recordIq.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    recordIq = recordIq.OrderByDescending(s => s.Date);
                    break;
                default:
                    recordIq = recordIq.OrderBy(s => s.Project);
                    break;
            }

            int pageSize = 5;
            Record = await PaginatedList<Record>.CreateAsync(
                recordIq.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
