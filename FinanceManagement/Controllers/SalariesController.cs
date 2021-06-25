﻿using System.Linq;
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

        // GET: Salaries/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_context.Months.Where(s => s.MonthId != s.Salaries.MonthId), "MonthId", "Name");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryId,MonthId,Value")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_context.Months.Where(s => s.MonthId != s.Salaries.MonthId), "MonthId", "Name", salary.MonthId);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_context.Months.Where(s => s.MonthId == s.Salaries.MonthId), "MonthId", "Name", salary.MonthId);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaryId,MonthId,Value")] Salary salary)
        {
            if (id != salary.SalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.SalaryId))
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
            ViewData["MonthId"] = new SelectList(_context.Months.Where(s => s.MonthId == s.Salaries.MonthId), "MonthId", "Name", salary.MonthId);
            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.SalaryId == id);
        }
    }
}