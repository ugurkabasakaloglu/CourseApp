using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class EfInstructorRepsitory : IInstructorRepository
    {
        private DataContext _context;
        public EfInstructorRepsitory(DataContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Instructor Get(int id)
        {
            return _context.Instructors.Find(id);
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors
                 .Include(x => x.Contact)
                 .ThenInclude(x => x.Address);
        }


        public void Insert(Instructor entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Instructor entity)
        {
            throw new NotImplementedException();
        }
    }
}
