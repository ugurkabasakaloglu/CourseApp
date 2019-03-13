using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class EfCourseRepository :ICourseRepository
    {
        private DataContext context;

        public EfCourseRepository(DataContext _context)
        {
            context = _context;
        }

        public IQueryable<Course> Courses => context.Courses;

        public int CreateCourse(Course newCourse)
        {
            context.Courses.Add(newCourse);
            context.SaveChanges();
            return newCourse.Id;
        }

        public void DeleteCourse(int courseid)
        {
            var entity = GetById(courseid);
            context.Courses.Remove(entity);
            if (entity.Instructor != null)
            {
                context.Remove(entity.Instructor);
            }

            context.SaveChanges();
        }

        public Course GetById(int courseid)
        {
            return context.Courses
                .Include(x=>x.Instructor)
                .ThenInclude(x=>x.Contact)
                .ThenInclude(x=>x.Address)
                .FirstOrDefault(x=>x.Id==courseid);
        }

        public IEnumerable<Course> GetCourseByActive(bool isactive)
        {
            return context.Courses.Where(x => x.IsActive == true);
        }

        public IEnumerable<Course> GetCourses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesByFilters(string name = null, decimal? price = null, string IsActive = null)
        {
            IQueryable<Course> query=context.Courses;
            if (name!=null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (price!=null)
            {
                query = query.Where(x => x.Price >= price);
            }

            if (IsActive=="on")
            {
                query = query.Where(x => x.IsActive == true);
            }

            return query.Include(x=>x.Instructor).ToList();
        }

        public void UpdateCourse(Course updateCourse,Course originalCourse=null)
        {
            if (originalCourse==null)
            {
                originalCourse = context.Courses.Find(updateCourse.Id);
            }
            else
            {
                context.Courses.Attach(originalCourse);
            }
            originalCourse.Name = updateCourse.Name;
            originalCourse.Description = updateCourse.Description;
            originalCourse.Price = updateCourse.Price;
            originalCourse.IsActive = updateCourse.IsActive;

            originalCourse.Instructor.Name = updateCourse.Instructor.Name;

            EntityEntry entry = context.Entry(originalCourse);
            //Modified,Unchanged,Added,Deleted,Detached
            Console.WriteLine($"Entity State :{entry.State}");
            foreach (var property in new string[] { "Name","Description","Price","IsActive"})
            {
                Console.WriteLine($"{property}-old:{entry.OriginalValues[property]} new:{entry.CurrentValues[property]}");
            }

            context.SaveChanges();
        }
    }
}
