using EntityFramework1.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run(); //original
            
            ///////////////////////
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        //////////////////////
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SchoolContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}


//CREATE TABLE Course (
//        CourseID int IDENTITY(1,1) PRIMARY KEY,
//         Title varchar(50),
//        Credits int
//)

//CREATE TABLE Enrollment (
//        EnrollmentID  int IDENTITY(1,1) PRIMARY KEY,
//        CourseID  int,
//        StudentID  int,
//        Grade varchar(1)
//)

//CREATE TABLE Student (
//        StudentID  int IDENTITY(1,1) PRIMARY KEY,
//        LastName varchar(255),
//		FirstMidName varchar(255),
//		EnrollmentDate DateTime
//)


//CREATE TABLE Course (
//        CourseID int PRIMARY KEY,
//         Title varchar(50),
//        Credits int
//)

//CREATE TABLE Enrollment (
//        EnrollmentID  int PRIMARY KEY,
//        CourseID  int,
//        StudentID  int,
//        Grade varchar(1)
//)

//CREATE TABLE Student (
//        StudentID  int PRIMARY KEY,
//        LastName varchar(255),
//		FirstMidName varchar(255),
//		EnrollmentDate DateTime
//)

//    https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-5.0