using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManagement.Models.Entities;

namespace FinanceManagement.Controllers
{
    public class ExpenseTypesController : Controller
    {
        private readonly Context _context;

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

        // GET: ExpenseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseTypeId,Name")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmation"] = expenseType.Name + " was created.";
                _context.Add(expenseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        public JsonResult AddExpenseType (string expenseTxt)
        {
            if (!string.IsNullOrEmpty(expenseTxt))
            {
                if(!_context.ExpenseTypes.Any(et => et.Name.ToUpper() == expenseTxt))
                {
                    ExpenseType expenseType = new ExpenseType();

                    expenseType.Name = expenseTxt;

                    _context.Add(expenseType);
                    _context.SaveChanges();

                    return Json(true);
                }
            }
            return Json(false);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseTypeId,Name")] ExpenseType expenseType)
        {
            if (id != expenseType.ExpenseTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmation"] = expenseType.Name + " was edited.";
                    _context.Update(expenseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(expenseType.ExpenseTypeId))
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
            return View(expenseType);
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
