using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManagement.Models.Entities;
using X.PagedList;
using FinanceManagement.Models.ViewModels;

namespace FinanceManagement.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly Context _context;

        public ExpensesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            const int pageItems = 10; // Quantity of items per page in pagination.
            int pageNumber = (page ?? 1); // If page doesnt has a number receive 1 by default.

            var context = _context.Expenses.Include(e => e.ExpenseTypes).Include(e => e.Month).OrderBy(e => e.MonthId);

            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "Name");
            ViewData["TypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "Name");

            return View(await context.ToPagedListAsync(pageNumber, pageItems));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? page, string searchTxt)
        {
            const int pageItems = 10; // Quantity of items per page in pagination.
            int pageNumber = (page ?? 1); // If page doesnt has a number receive 1 by default.

            if (!string.IsNullOrEmpty(searchTxt))
            {
                return View(await _context.Expenses.Include(e => e.ExpenseTypes).Include(e => e.Month).Where(m => m.Month.Name.ToUpper().Contains(searchTxt.ToUpper())).ToPagedListAsync(pageNumber, pageItems));
            }

            return View(await _context.Expenses.Include(e => e.ExpenseTypes).Include(e => e.Month).ToPagedListAsync(pageNumber, pageItems));
        }

        public JsonResult AddExpense(int monthId, int expenseId, double expenseValue)
        {
            Expense expense = new Expense();

            expense.MonthId = monthId;
            expense.ExpenseTypeId = expenseId;
            expense.Value = expenseValue;

            _context.Add(expense);
            _context.SaveChanges();

            TempData["Confirmation"] = "R$ " + expense.Value + " of " + _context.ExpenseTypes.Where(et => et.ExpenseTypeId == expense.ExpenseTypeId).FirstOrDefault().Name + " in " + _context.Months.Where(m => m.MonthId == expense.MonthId).FirstOrDefault().Name + " was created.";

            return Json(true);
        }

        public JsonResult EditExpense(int id, int monthId, int expenseId, double expenseValue)
        {
            var expense = _context.Expenses.Find(id);

            expense.MonthId = monthId;
            expense.ExpenseTypeId = expenseId;
            expense.Value = expenseValue;

            _context.Update(expense);
            _context.SaveChanges();

            TempData["Confirmation"] = "R$ " + expense.Value + " of " + _context.ExpenseTypes.Where(et => et.ExpenseTypeId == expense.ExpenseTypeId).FirstOrDefault().Name + " in " + _context.Months.Where(m => m.MonthId == expense.MonthId).FirstOrDefault().Name + " was edited.";

            return Json(true);
        }

        public async Task<JsonResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            TempData["Confirmation"] = "R$ " + expense.Value + " of " + _context.ExpenseTypes.Where(et => et.ExpenseTypeId == expense.ExpenseTypeId).FirstOrDefault().Name + " in " + _context.Months.Where(m => m.MonthId == expense.MonthId).FirstOrDefault().Name + " was deleted.";
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return Json("deleted!");
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }

        public JsonResult MonthExpenses(int monthId)
        {
            TotalMonthExpensesViewModel expenses = new TotalMonthExpensesViewModel();

            expenses.TotalValue = _context.Expenses.Where(e => e.Month.MonthId == monthId).Sum(e => e.Value);
            expenses.Salary = _context.Salaries.Where(s => s.Month.MonthId == monthId).Select(s => s.Value).FirstOrDefault();

            return Json(expenses);
        }
    }
}
