

using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace Candal.Data
{
    //https://www.youtube.com/watch?v=SSwuwIzQZvE
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MyIdentityUser> GetUsers { get; set; }
        public DbSet<MyView1> GetView1 { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    //base.OnConfiguring(options);
        //    options.UseSqlServer("Data Source=PC-I5\\SQLEXPRESS;Initial Catalog=IdentityASP;Integrated Security=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             //https://www.michalbialecki.com/2020/09/09/working-with-views-in-entity-framework-core-5/
            modelBuilder.Entity<MyView1>(entity =>
            {
                ////entity.HasKey(e => e.ID);
                //entity.HasNoKey(); /// important or [Keyless] in model. give all diferent records
                //entity.ToView("View_1");  //or [Table("View_1")] in model
            });
        }
    }
}
