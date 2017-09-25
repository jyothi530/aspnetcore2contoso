using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using ContosoUniversity.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var noOfStudentsByEnrollmentDate = from student in _context.Students
                                               group student by student.EnrollmentDate into groupExp
                                               select new EnrollmentDateGroup
                                               {
                                                   EnrollmentDate = groupExp.Key,
                                                   StudentCount = groupExp.Count()
                                               };



            //await _context.Students
            //                                    .GroupBy(student => student.EnrollmentDate)
            //                                    .Select(groupExp => new EnrollmentDateGroup
            //                                    {
            //                                        EnrollmentDate = groupExp.Key,
            //                                        StudentCount = groupExp.Count()
            //                                    }).ToListAsync()

            return View(await noOfStudentsByEnrollmentDate.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
