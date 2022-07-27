using System;
using System.Collections.Generic;

namespace test_ef.Models
{
    public partial class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? StandardId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
