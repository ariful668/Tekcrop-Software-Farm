using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechProject.Models;

namespace TechProject.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class JobPostsController : Controller
    {
        private readonly TekcropContext _context;

        public JobPostsController(TekcropContext context)
        {
            _context = context;
        }



        // GET: JobPosts
        public async Task<IActionResult> Index()
        {

            return View(await _context.JobPost.OrderByDescending(x => x.AddedDate).ToListAsync());
        }



        // GET: JobPosts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // GET: JobPosts/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Desp,Position,Skill,Vacancy,Nature,Experience,Requirements,Salary,AddedDate,Address,LastDate")] JobPost jobPost)
        {

            if (ModelState.IsValid)
            {
                jobPost.AddedDate = DateTime.Now;
                _context.Add(jobPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(jobPost);
        }

        // GET: JobPosts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost.FindAsync(id);
            if (jobPost == null)
            {
                return NotFound();
            }
            return View(jobPost);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Desp,Position,Skill,Vacancy,Nature,Experience,Requirements,Salary,AddedDate,Address,LastDate")] JobPost jobPost)
        {
            if (id != jobPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostExists(jobPost.Id))
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
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jobPost = await _context.JobPost.FindAsync(id);
            _context.JobPost.Remove(jobPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostExists(long id)
        {
            return _context.JobPost.Any(e => e.Id == id);
        }

        //[Arif 07-04-19]
        [HttpPost]
        public JsonResult AutoDelete()
        {
            var result = (dynamic)null;

            var getJobPost = _context.JobPost.OrderByDescending(x => x.AddedDate).ToList();
            foreach (var item in getJobPost)
            {
                if (item.LastDate.Date == DateTime.Now.Date)
                {
                    _context.JobPost.Remove(item);
                    _context.SaveChanges();
                    return result = new JsonResult(new { success = true, msg = "Time is Expired" });
                }
                else result = new JsonResult(new { success = true, errorMessage = "Failed" });

            }
            return result;
        }
        //[~Arif 07-04-19]


    }
}
