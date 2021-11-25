using Candal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }

         [HttpGet]
        [Route("get-all-users")]
        public ActionResult GetAllUsers()
        {
            var all = _applicationDbContext.GetUsers.ToList();

            return this.Ok(all);
        }

        [HttpPost]
        [Route("insert-user")]
        public ActionResult Insert()
        {
            var rec = new Models.MyIdentityUser()
            {
                IdX = "2",
                UserName = "xpto2",
                Email = "xpto2@gmail.com",
                LockoutEnabled = false

        // public DateTimeOffset? LockoutEnd { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public string PhoneNumber { get; set; }
        //public string ConcurrencyStamp { get; set; }
        //public string SecurityStamp { get; set; }
        //public string PasswordHash { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string NormalizedEmail { get; set; }
        //public string Email { get; set; }
        //public string NormalizedUserName { get; set; }
        //public string UserName { get; set; }
        //public string Id { get; set; }
        //public int AccessFailedCount { get; set; }
    };

            _applicationDbContext.GetUsers.Add(rec);
            _applicationDbContext.SaveChanges();

            return this.Ok(rec);
        }

        [HttpPut]
        [Route("update-user")]
        public ActionResult Update()
        {
            MyIdentityUser rec1 = _applicationDbContext.GetUsers.Find("1"); //find by primary key
            rec1.Email = "zulu@example.com";
            _applicationDbContext.SaveChanges();

            return this.Ok(rec1);
        }

        [HttpDelete]
        [Route("delete-user")]
        public ActionResult Delete()
        {
            MyIdentityUser rec1 = _applicationDbContext.GetUsers.Find("1");
            rec1.Email = "zulu@example.com";
            _applicationDbContext.Remove(rec1);
            _applicationDbContext.SaveChanges();

            return this.Ok(rec1);
        }

        [HttpGet]
        [Route("find-user-by-id")]
        public ActionResult FindById()
        {
            //MyIdentityUser rec1 =_applicationDbContext.GetUsers.Find("1");
            MyIdentityUser rec1 = _applicationDbContext.GetUsers.Where(cols => cols.IdX.Equals("1")).FirstOrDefault(); //generate a sql statement more simple

            return this.Ok(rec1);
        }

        [HttpGet]
        [Route("find-user-by-name-like")]
        public ActionResult FindWhereLike()
        {
            var rec1 = _applicationDbContext.GetUsers.Where(cols => cols.UserName.Contains("xpto")); //linq

            return this.Ok(rec1);
        }

        [HttpGet]
        [Route("find-user-where-parameter")]
        public ActionResult FindWhereParameter()
        {
            string id = "1";
            string name = "xpto1";

            var sql = "SELECT * from AspNetUsers WHERE id=(@id) AND UserName=(@name)";
            var parms = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@id", id),
                new Microsoft.Data.SqlClient.SqlParameter("@name", name)
            };

            //errado
            var rec1 = _applicationDbContext.Database.ExecuteSqlRaw(sql, parms); // não dá, retorna um inteiro


            return this.Ok(rec1);
        }

        [HttpGet]
        [Route("sqlStatement")]
        public ActionResult SqlStatement()
        {
            string name = "xpto";
            var sql = "SELECT * from AspNetUsers WHERE UserName LIKE '%' + @name + '%'";
            //var rec2 = _applicationDbContext.Database.ExecuteSqlInterpolated(xx1);
            //var rec3 = _applicationDbContext.Database.ExecuteSqlRaw(xx0);
            //var rec4 = _applicationDbContext.GetAll.FromSqlRaw(xx0);

            var entities = new List<Dictionary<string, object>>();

            using (var command = _applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                DbParameter parm = command.CreateParameter();
                parm.ParameterName = "@name";
                parm.DbType = DbType.String;
                parm.Value = name;
                command.Parameters.Add(parm);


                _applicationDbContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        var rowColl = new Dictionary<string, object>();
                        for (int i = 0; i < result.FieldCount; i++)
                        {
                            rowColl.Add(result.GetName(i), result.GetValue(i).ToString());
                        }
                        entities.Add(rowColl);
                    }
                }
            }

            return this.Ok(new { data = entities });
        }


        [HttpGet]
        [Route("sqlStatement-no-entity-framework")]
        public ActionResult SqlStatement2()
        {
            var entities = new List<Dictionary<string, object>>();

            string name = "xpto";

            var sql = "SELECT * from AspNetUsers WHERE UserName LIKE '%' + @name + '%'";

            string connString = _configuration.GetConnectionString("DefaultConnection");
            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connString))
            {
                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                command.Parameters.Add("@name", SqlDbType.NVarChar);
                command.Parameters["@name"].Value = name;

                conn.Open();
                using (Microsoft.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rowColl = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rowColl.Add(reader.GetName(i), reader.GetValue(i).ToString());
                        }
                        entities.Add(rowColl);
                    }
                }
            }

            return this.Ok(new { data = entities });
        }


        [HttpGet]
        [Route("getView1")]
        public ActionResult GetView1()
        {
            //Note: quando não existe primary Key tem de se usar ".HasNoKey" ou attributo [Keyless]
            //procurar no source por .HasNoKey ou [Keyless]

            var all = _applicationDbContext.GetView1.ToList();

            return this.Ok(all);
        }

   }
}
