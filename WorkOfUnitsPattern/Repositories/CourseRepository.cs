using EntityFramework1.Data;
using EntityFramework1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework1.Repositories
{
    public interface ICourseRepository
    {
        void Save(Course course);
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext _schoolContext;

        public CourseRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void Save(Course course)
        {
            _schoolContext.Courses.Add(course);
            //_schoolContext.SaveChanges(); //pass to UnitOfWork

        }
    }
}
