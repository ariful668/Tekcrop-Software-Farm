using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechProject.Models;
using TechProject.ViewModels;

namespace TechProject.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        public IActionResult AdminIndex()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private readonly TekcropContext _context;

        public HomeController(TekcropContext context)
        {
            _context = context;
        }

        // GET: JobPosts
        public async Task<IActionResult> Career()
        {
            return View(await _context.JobPost.OrderByDescending(x => x.AddedDate).ToListAsync());
        }

        // GET: JobPosts/Details/5
        public async Task<IActionResult> JobDetails(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost
                .FirstOrDefaultAsync(m => m.Id == id);
            //[Arif 01-04-19]
            var jPost = new JobPost();
            jPost = jobPost;

            var model = new CustomRecruiters()
            {
                JobPost = jobPost
            };
            //[~Arif 01-04-19]

            if (jobPost == null)
            {
                return NotFound();
            }
            //[Arif 01-04-19]
            //return View(jobPost);
            return View(model);
            //[~Arif 01-04-19]
        }
        // GET: ContactPersons/Create
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Id,Name,Phone,Email,Message")] ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactPerson);
                await _context.SaveChangesAsync();
                return Content($"Succesfully Submitted");
              //return RedirectToAction(nameof(Contact));
            }
            return View("Home", "Contact");
        }
    }
}
