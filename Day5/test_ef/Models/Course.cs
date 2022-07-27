using System;
using System.Collections.Generic;

namespace test_ef.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? StudentId { get; set; }

        public virtual Student? Student { get; set; }
    }
}
