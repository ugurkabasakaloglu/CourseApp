using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public static class SeedDatabase
    {
        public static void Seed(DbContext context)
        {
            //context.Database.GetPendingMigrations().Count()==0
            if (!context.Database.GetPendingMigrations().Any())  //Bekleyen migrations işlemi yoksa
            {
                if (context is DataContext)
                {
                    DataContext _context = context as DataContext;
                    if (_context.Courses.Count() == 0)
                    {
                        _context.Courses.AddRange(Courses); //Addrange birden fazla bilgiyi ekler 
                        //Add tek bir veriyi ekler

                    }
                    if (_context.Instructors.Count() == 0)
                    {
                        _context.Instructors.AddRange(Instructors);
                    }

                }

                if (context is UserContext)
                {
                    UserContext _context = context as UserContext;
                    if (_context.Users.Count() == 0)
                    {
                        _context.Users.AddRange(Users);
                    }
                }
                context.SaveChanges();
            }

        }

        private static User[] Users =
        {
            new User(){UserName="Ugur", Email="ugur@ugur.com", City="İstanbul", Password="1234" },
            new User(){UserName="Ugur2", Email="ugur2@ugur.com", City="Duzce", Password="1234" }

        };

        private static Course[] Courses
        {
            get
            {
                Course[] course = new Course[]
                {
                    new Course(){Name="Html",Price=10,Description="about html",IsActive=true,Instructor=Instructors[0]},
                    new Course(){Name="Css",Price=10,Description="about css",IsActive=true,Instructor=Instructors[1]},
                    new Course(){Name="JavaScript",Price=20,Description="about javascript",IsActive=true,Instructor=Instructors[2]},
                    new Course(){Name="NodeJs",Price=10,Description="about nodejs",IsActive=true,Instructor=Instructors[3]},
                    new Course(){Name="Angular",Price=30,Description="about angular",IsActive=true,Instructor=Instructors[4]},
                    new Course(){Name="React",Price=20,Description="about react",IsActive=true,Instructor=Instructors[0]},
                    new Course(){Name="Mvc",Price=10,Description="about mvc",IsActive=true,Instructor=Instructors[3]}
                };
                return course;
            }

        }

        private static Instructor[] Instructors =
        {
            new Instructor(){Name="Onur",Contact=new Contact(){ Email="ugur@ugur.com", Phone="554965",Address=new Address(){ City="çorum",Country="papua", Text="vfcdwxsa" }  }},
            new Instructor(){Name="Selin",Contact=new Contact(){ Email="ugur2@ugur.com", Phone="236373",Address=new Address(){ City="wfrgt",Country="freg", Text="cdvcfre" }  }},
            new Instructor(){Name="Serpil",Contact=new Contact(){ Email="ugur3@ugur.com", Phone="65656",Address=new Address(){ City="frg",Country="rtr", Text="hyhyh" }  }},
            new Instructor(){Name="Nurcihan",Contact=new Contact(){ Email="ugur4@ugur.com", Phone="54546",Address=new Address(){ City="hyhy",Country="ıkı", Text="ftrgt" }  }},
            new Instructor(){Name="Orkun",Contact=new Contact(){ Email="ugur5@ugur.com", Phone="8776",Address=new Address(){ City="vf",Country="hyhuj", Text="wwer" }  }}
        };


    }
}
