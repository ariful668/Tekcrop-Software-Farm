//[Arif 01-04-19]
using Microsoft.AspNetCore.Http;
using TechProject.Models;

namespace TechProject.ViewModels
{
    public partial class CustomRecruiters
    {
        public IFormFile ActualFile { get; set; }

        public Recruiter Recruiter { get; set; }
        public JobPost JobPost { get; set; } 
    }
}
//[~Arif 01-04-19]