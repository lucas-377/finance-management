using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManagement.Models.Entities;

namespace FinanceManagement.Controllers
{
    public class SalariesController : Controller
    {
        private readonly Context _context;

        public SalariesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var context = _context.Salaries.Include(s => s.Month);

            ViewData["MonthId"] = new SelectList(_context.Months.Where(s => s.MonthId != s.Salaries.MonthId), "MonthId", "Name"); // Send months without salary to view

            return View(await context.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchTxt)
        {
            if (!string.IsNullOrEmpty(searchTxt))
            {
                return View(await _context.Salaries.Include(s => s.Month).Where(m => m.Month.Name.ToUpper().Contains(searchTxt.ToUpper())).ToListAsync());
            }

            return View(await _context.Salaries.Include(s => s.Month).ToListAsync());
        }

        public JsonResult AddSalary(int monthId, double salaryValue)
        {
            Salary salary = new Salary();

            salary.MonthId = monthId;
            salary.Value = salaryValue;

            _context.Add(salary);
            _context.SaveChanges();

            TempData["Confirmation"] = "R$ " + salary.Value + " in " + _context.Months.Where(m => m.MonthId == salary.MonthId).FirstOrDefault().Name + " was created.";

            return Json(true);
        }

        public async Task<JsonResult> EditSalaryAsync(int id, double salaryValue)
        {
            var salary = await _context.Salaries.FindAsync(id);

            salary.Value = salaryValue;

            _context.Update(salary);
            await _context.SaveChangesAsync();

            TempData["Confirmation"] = "R$ " + _context.Salaries.Find(id).Value + " in " + _context.Months.Where(m => m.MonthId == salary.MonthId).FirstOrDefault().Name + " was edited.";

            return Json(true);
        }

        public async Task<JsonResult> Delete(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            TempData["Confirmation"] = _context.Months.Where(m => m.MonthId == salary.MonthId).FirstOrDefault().Name + " was deleted.";
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return Json(_context.Months.Where(m => m.MonthId == salary.MonthId).FirstOrDefault().Name + "deleted!");
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.SalaryId == id);
        }
    }
}
