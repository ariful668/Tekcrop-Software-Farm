using Microsoft.EntityFrameworkCore;

namespace TechProject.Models
{
    public partial class TekcropContext : DbContext
    {
        public TekcropContext(DbContextOptions<TekcropContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactPerson> ContactPerson { get; set; }
        public virtual DbSet<JobPost> JobPost { get; set; }
        public virtual DbSet<Recruiter> Recruiter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-8RCCAHG;Database=Techcrop;Trusted_Connection=true");
        }
    }
}
