using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        //public int InstructorId { get; set; }
        //Navigation property
        public virtual Instructor Instructor { get; set; }
        //virtual eklenirse Lazy Loading
    }
}
