using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManagement.Models.Entities;
using X.PagedList;

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

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "Name");
            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,MonthId,ExpenseTypeId,Value")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmation"] = _context.ExpenseTypes.Find(expense.ExpenseTypeId).Name + " in " + _context.Months.Find(expense.MonthId).Name + " was created.";
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "Name", expense.ExpenseTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "Name", expense.MonthId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "Name", expense.ExpenseTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "Name", expense.MonthId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,MonthId,ExpenseTypeId,Value")] Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmation"] = "Expense was updated.";
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "Name", expense.ExpenseTypeId);
            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "Name", expense.MonthId);
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
