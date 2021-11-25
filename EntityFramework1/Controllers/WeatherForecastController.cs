
using EntityFramework1.Data;
using EntityFramework1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SchoolContext _schoolDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SchoolContext context)
        {
            _logger = logger;
            _schoolDbContext = context;
        }

        #region students

        [HttpPost]
        [Route("students")]
        public ActionResult StudentsAdd([FromBody] Student student)
        {
            student.StudentID = 0;
            student.Enrollments = null;
            student.EnrollmentDate = DateTime.Now;

            _schoolDbContext.Students.Add(student);
            _schoolDbContext.SaveChanges();

            return Ok(student);
        }

        [HttpPut]
        [Route("students")]
        public ActionResult StudentsUpdate([FromBody] Student student)
        {
            Student result1 = _schoolDbContext.Students.Find(student.StudentID); // search by primary key
            //Student result = _schoolDbContext.Students.SingleOrDefault(p => p.StudentID == student.StudentID); //search and return on record
            if (result1 == null)
                return NotFound();

            result1.FirstMidName = student.FirstMidName;
            result1.LastName = student.LastName;

            _schoolDbContext.Update<Student>(result1);
            _schoolDbContext.SaveChanges();

            return Ok(result1);
        }

        [HttpDelete]
        [Route("students")]
        public ActionResult StudentsDelete([FromBody] Student student)
        {
            Student result1 = _schoolDbContext.Students.Find(student.StudentID); // search by primary key
            if (result1 == null)
                return NotFound();

            _schoolDbContext.Remove<Student>(result1);
            _schoolDbContext.SaveChanges();

            return Ok(result1);
        }

        [HttpGet]
        [Route("students")]
        public ActionResult StudentsGet()
        {
            IEnumerable<Student> list = _schoolDbContext.Students;

            return Ok(list);
        }

        [HttpGet] //FromQuery
        [Route("studentsPage")]
        public ActionResult StudentsGetByKeyQ([FromQuery(Name = "skip")] int skip = 0, [FromQuery(Name = "take")] int take = 0)
        {
            IEnumerable<Student> studentList;
            if (take == 0)
                studentList = _schoolDbContext.Students;
            else
                studentList = _schoolDbContext.Students.Skip(skip).Take(take);

            return Ok(studentList);
        }

        [HttpGet] //FromRoute
        [Route("students/{idX:int}")]
        public ActionResult<Student> StudentsGetByKeyR([FromRoute(Name = "idX")] int id)
        {
            Student student = _schoolDbContext.Students.Find(id);
            return Ok(student);
        }

        [HttpGet]  //FromQuery
        [Route("studentsById")]
        public ActionResult StudentsGetByKeyQ([FromQuery(Name = "idX")] int id = 0)
        {
            Student student = _schoolDbContext.Students.Find(id);
            return Ok(student);
        }


        [HttpGet]
        [Route("students99")]
        public ActionResult GetStudents99()
        {
            //pagination v1 via linq
            //IQueryable<Student> query = from x in _schoolDbContext.Students.Skip(2).Take(10)
            //                            select x;
            //IEnumerable<Student> xx = query.ToArray(); //execute query

            //pagination v2 via entity
            //IEnumerable<Student> xx = _schoolDbContext.Students.Skip(2).Take(10);


            //query via linq
            //IQueryable<Student> query = from x in _context.Students
            //                            where x.StudentID == 1
            //                            select x;
            //IEnumerable<Student> xx = query.ToArray();  //execute query

            //query via entity
            //IEnumerable<Student> xx = _schoolDbContext.Students.Where(p => p.StudentID == 2);
            //IEnumerable<Student> xx = _schoolDbContext.Students.Where(p => p.StudentID > 12 && p.StudentID < 14); //.FirstOrDefault();



            var xx = _schoolDbContext.Students.Include(b => b.Enrollments.Select(c => c.StudentID)); //left join       AsNoTracking ?!?!?!??
            //var xx = _schoolDbContext.Students.Include(b => b.Enrollments).AsNoTracking(); //left join       AsNoTracking ?!?!?!??
            //var xx = _schoolDbContext.Students.Include("Enrollment"); //left join

            return Ok(xx);
        }

        #endregion

        #region View

        [HttpGet]
        [Route("getView1")]
        public ActionResult GetView1()
        {
            //Note: quando não existe primary Key tem de se usar ".HasNoKey" ou attributo [Keyless]
            //procurar no source por .HasNoKey ou [Keyless]

            var all = _schoolDbContext.GetView1.ToList();

            return this.Ok(all);
        }

        #endregion

        #region enrollments

        [HttpGet]
        [Route("enrollments")]
        public ActionResult<dynamic> GetEnrollments()
        {
            return _schoolDbContext.Enrollments.ToList();
        }

        #endregion
    }
}


//[FromBody]
//[FromQuery]
//public IEnumerable<string> Get([FromQuery] QueryParameters parameters)
//{
//    return new[] { parameters.A.ToString(), parameters.B.ToString() };
//}
//[FromRoute]
//[FromHeader]
//[FromFrom]
//https://www.michalbialecki.com/en/2020/01/10/net-core-pass-parameters-to-actions/
//https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-5.0
//https://www.youtube.com/watch?v=BDi0A2HRNRc
//https://entityframeworkcore.com/knowledge-base/59639245/load-child-entity-on-the-fetch-of-the-parent-entity-efcore

//var users = from u in context.Users.Include("WhoHasBlockedMe").Include("Friends")
//            from act in u.WantsToDo
//            where act.ActivityId == activityId &&
//                u.Gender == genderId &&
//                u.City == city &&
//                u.Country == countryIsoCode
//            select u;

//    var job = db.Jobs
//.Include(x => x.Quotes.Select(q => q.QuoteItems))
//.Where(x => x.JobID == id)
//.SingleOrDefault();
