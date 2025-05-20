using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalDevEx.Data;
using StudentPortalDevEx.Models;
using StudentPortalDevEx.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortalDevEx.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // ✅ GET: Students/Index (for DevExtreme Grid)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students); // Views/Students/Index.cshtml with DevExtreme Grid
        }

        // ✅ API for DevExtreme remote data source (Optional)
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await dbContext.Students.ToListAsync();
            return Json(students);
        }

        // ✅ GET: Students/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // ✅ POST: Students/Add
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index","Home");
        }

        // ✅ GET: Students/List (for legacy jQuery DataTable version)
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        // ✅ GET: Students/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // ✅ POST: Students/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index,Home");
        }

        // ✅ POST: Students/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student != null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index,Home");
        }
    }
}
