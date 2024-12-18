using HW_ASP_11._2.Data;
using HW_ASP_11._2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_ASP_11._2.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string statusFilter)
        {
            var todoItems = _context.TodoItems.AsQueryable();

            // Фильтрация
            if (!string.IsNullOrEmpty(statusFilter))
            {
                todoItems = todoItems.Where(t => t.Status == statusFilter);
            }

            // Сортировка
            switch (sortOrder)
            {
                case "date_asc":
                    todoItems = todoItems.OrderBy(t => t.DueDate);
                    break;
                case "date_desc":
                    todoItems = todoItems.OrderByDescending(t => t.DueDate);
                    break;
            }

            return View(await todoItems.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();
            return View(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();
            return View(todoItem);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
