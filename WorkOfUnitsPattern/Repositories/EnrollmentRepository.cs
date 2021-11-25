using EntityFramework1.Data;
using EntityFramework1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework1.Repositories
{
    public interface IEnrollmentRepository
    {
        void Save(Enrollment enrollment);
    }

    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly SchoolContext _schoolContext;

        public EnrollmentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void Save(Enrollment enrollment)
        {
            _schoolContext.Enrollments.Add(enrollment);
            //_schoolContext.SaveChanges(); //pass to UnitOfWork
        }
    }


}
