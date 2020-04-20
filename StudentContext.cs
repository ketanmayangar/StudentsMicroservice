using Microsoft.EntityFrameworkCore;
using StudentsMicroservice.Models;

namespace StudentsMicroservice.DBContexts
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        //public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //no need to run this as the data are posted through page
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                   Name = "stud1",
                    Class = "Class 1",
                },
                new Student
                {
                   Id = 2,
                   Name = "stud2",
                    Class = "Class 2",
                },
                new Student
                {
                    Id = 3,
                   Name = "stud3",
                    Class = "Class 3",
                }
            );
        }

    }
}
