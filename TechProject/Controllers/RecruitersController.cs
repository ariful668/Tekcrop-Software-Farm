//[Arif 01-04-19]
using MailKit.Net.Smtp; //[NOTE: for email]
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;  //[NOTE: for email]
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechProject.Models;
using TechProject.ViewModels;

namespace TechProject.Controllers
{
    public class RecruitersController : Controller
    {
        private readonly TekcropContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public RecruitersController(TekcropContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // POST: Recruiters
        [HttpPost]
        public IActionResult JobApplied(CustomRecruiters obj)
        {
            var lastId = _context.Recruiter.ToList().Count == 0 ? 0 : _context.Recruiter.ToList().Last().Id;
            obj.Recruiter.Id = lastId + 1;

            //[NOTE:Configure Sender and Receiver details.]
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Tech Training", "16203087@iubat.edu"));
            msg.To.Add(new MailboxAddress(obj.Recruiter.Name, obj.Recruiter.Email));
            msg.Subject = "Successful applied message.";
            msg.Body = new TextPart("plain")
            {
                Text = "Hey, Greeetings... You have succefully applied for the Job Number " + obj.Recruiter.JobId + ". Please be patience while your CV is processing by our team."
            };
            //[NOTE:Configure smtp and Send Mail]
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("16203087@iubat.edu", "shanto668");
                client.Send(msg);
                client.Disconnect(true);
            }

            //[NOTE: Create Directory]
            string folderName = "CVFiles/";
            string path = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            Directory.CreateDirectory(path);

            //[NOTE: Getting file extension]
            string getExtension = Path.GetExtension(obj.ActualFile.FileName);
            var savingPath = path + obj.Recruiter.Id + "_" + obj.Recruiter.JobId + "_" + obj.Recruiter.Name + getExtension;

            //[NOTE: Copy file in specific location]
            using (var stream = new FileStream(savingPath, FileMode.Create))
            {
                obj.ActualFile.CopyTo(stream);
            }

            //[NOTE: Saving data into database.]
            obj.Recruiter.Cv = folderName + obj.Recruiter.Id + "_" + obj.Recruiter.JobId + "_" + obj.Recruiter.Name + getExtension;

            _context.Add(obj.Recruiter);
            _context.SaveChanges();
            return Content($"Succesfully Submitted Your Cv. Check your email to get more details.");
        }

        // GET: Recruiters
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recruiter.OrderByDescending(x => x.JobId).ToListAsync());
        }

        // GET: JobPosts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var re = await _context.Recruiter
                .FirstOrDefaultAsync(m => m.Id == id);

            var path = Path.Combine(_hostingEnvironment.WebRootPath, re.Cv);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Recruiter.Remove(re);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
//[~Arif 01-04-19]