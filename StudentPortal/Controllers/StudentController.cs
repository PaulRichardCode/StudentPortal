using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;

namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //RETRIVING DATA
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        //SUBMITTING DATA
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }

        //RETRIV
        [HttpGet]
        public async Task<IActionResult> List()
        {
           var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }
        //press update button come here
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Phone = viewModel.Phone;
                student.Email = viewModel.Email;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
        //pressed delete button come here
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel) //student is an updated data e.g name 
        {
            //find the updated data
            var student = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if(student is not null)
            {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        } 
    } 
}
