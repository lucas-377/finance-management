using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManagement.Models.Entities;

namespace FinanceManagement.Controllers
{
    public class ExpenseTypesController : Controller
    {
        private Context _context;

        public ExpenseTypesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseTypes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchTxt)
        {
            if (!string.IsNullOrEmpty(searchTxt))
            {
                return View(await _context.ExpenseTypes.Where(et => et.Name.ToUpper().Contains(searchTxt.ToUpper())).ToListAsync());
            }

            return View(await _context.ExpenseTypes.ToListAsync());
        }

        public async Task<JsonResult> ExpenseTypeExists(string name)
        {
            if (await _context.ExpenseTypes.AnyAsync(et => et.Name.ToUpper() == name.ToUpper())){
                return Json("This type already exists!");
            }

            return Json(true);
        }

        public JsonResult AddExpenseType (string expenseTxt)
        {
            if (!string.IsNullOrEmpty(expenseTxt))
            {
                if(!_context.ExpenseTypes.Any(et => et.Name.ToUpper() == expenseTxt.ToUpper()))
                {
                    ExpenseType expenseType = new ExpenseType();

                    expenseType.Name = expenseTxt;

                    _context.Add(expenseType);
                    _context.SaveChanges();

                    TempData["Confirmation"] = expenseType.Name + " was created.";

                    return Json(true);
                }
            }
            return Json(false);
        }

        public async Task<JsonResult> EditExpenseTypeAsync(int? id, string expenseTxt)
        {
            var expenseType = await _context.ExpenseTypes.FirstOrDefaultAsync(et => id.HasValue && et.ExpenseTypeId.Equals(id));

            if (!string.IsNullOrEmpty(expenseTxt))
            {
                if (!_context.ExpenseTypes.Any(et => et.Name.ToUpper().Equals(expenseTxt.ToUpper()) && !et.ExpenseTypeId.Equals(id)))
                {
                    expenseType.Name = expenseTxt;

                    //_context.Update(expenseType);
                    _context.Set<ExpenseType>().Update(expenseType);
                    await _context.SaveChangesAsync();

                    TempData["Confirmation"] = expenseType.Name + " was edited.";

                    return Json(true);
                }
            }
            return Json(false);
        }

        // POST: ExpenseTypes/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            TempData["Confirmation"] = expenseType.Name + " was deleted.";
            _context.ExpenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();
            return Json(expenseType.Name + "deleted!");
        }

        private bool ExpenseTypeExists(int id)
        {
            return _context.ExpenseTypes.Any(e => e.ExpenseTypeId == id);
        }
    }
}
