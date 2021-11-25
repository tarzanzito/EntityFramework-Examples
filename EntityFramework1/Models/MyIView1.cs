using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework1.Models
{
    [Table("View_1")] //or   .ToView("View_1"); in DbContext - OnModelCreating
    [Keyless]  //or   .HasNoKey(); in DbContext - OnModelCreating - because the view not have primary key
    public class MyView1
    {
        public int StudentID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentID { get; set; } 
        public Grade? Grade { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
