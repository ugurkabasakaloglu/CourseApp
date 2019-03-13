﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public interface ICourseRepository
    {
        IQueryable<Course> Courses { get; }

        Course GetById(int courseid);
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCourseByActive(bool isactive);
        IEnumerable<Course> GetCoursesByFilters(string name=null,decimal? price=null,string IsActive=null);
        int CreateCourse(Course newCourse);
        void UpdateCourse(Course updateCourse,Course originalCourse=null);
        void DeleteCourse(int courseid);
    }
}
