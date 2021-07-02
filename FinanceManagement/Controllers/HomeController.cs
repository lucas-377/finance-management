using FinanceManagement.Models;
using FinanceManagement.Models.Entities;
using FinanceManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;

namespace FinanceManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Months"] = new SelectList(_context.Months.Where(x => x.MonthId == x.Salaries.MonthId), "MonthId", "Name");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
