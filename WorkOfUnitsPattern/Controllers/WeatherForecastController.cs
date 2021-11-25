using EntityFramework1.Data;
using EntityFramework1.Models;
using EntityFramework1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOfUnitsPattern.Data;

namespace EntityFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly SchoolContext _context;

        //public WeatherForecastController(SchoolContext context)
        //{
        //    _context = context;
        //}


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            IEnumerable<Student> xx = _context.Students.ToList();

            return xx;
        }


        [HttpGet]
        [Route("products")]
        public ActionResult<dynamic> Get2()
        {
            return _context.Enrollments.ToList();


        }


        [HttpPost]
        [Route("Post1")]
        public ActionResult<Student> Post1([FromServices] IStudentRepository studentRepository, [FromServices] ICourseRepository courseRepository)
        {
            Student student = null;
            Course course = null;

            try
            {
                course = new Course()
                {
                    Title = "xuxa",
                    Credits = 10
                };

                //var aaa = new List<Enrollment>();
                //    aaa.Add(new Enrollment()
                //    {
                //     Course="aaa"});


                student = new Student()
                {
                    EnrollmentDate = DateTime.Now,
                    FirstMidName = "Paulo",
                    LastName = "Manuel"
                    //Enrollments = aaa
                };

                courseRepository.Save(course);
                studentRepository.Save(student);
            }
            catch
            {

            }

            return Ok(student);
        }

        [HttpPost]
        [Route("Post2")]
        public ActionResult<Student> Post2([FromServices] IStudentRepository studentRepository, 
            [FromServices] ICourseRepository courseRepository,
            [FromServices] IUnitOfWork uow)
        {
            Student student = null;
            Course course = null;

            try
            {
                course = new Course()
                {
                    Title = "xuxa",
                    Credits = 10
                };

                //var aaa = new List<Enrollment>();
                //    aaa.Add(new Enrollment()
                //    {
                //     Course="aaa"});


                student = new Student()
                {
                    EnrollmentDate = DateTime.Now,
                    FirstMidName = "Paulo",
                    LastName = "Manuel"
                    //Enrollments = aaa
                };

                courseRepository.Save(course);
                studentRepository.Save(student);

                uow.Commit();
            }
            catch
            {
                uow.Rollback();
            }

            return Ok(student);
        }

    }
}
