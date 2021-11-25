using EntityFramework1.Data;
using EntityFramework1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework1.Repositories
{
    public interface IStudentRepository
    {
        void Save(Student student);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _schoolContext;

        public StudentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void Save(Student student)
        {
            _schoolContext.Students.Add(student);
           //_schoolContext.SaveChanges(); //pass to UnitOfWork
        }
    }
}
