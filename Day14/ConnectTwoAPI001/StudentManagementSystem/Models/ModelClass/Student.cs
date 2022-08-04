using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ModelClass
{
    public class Student
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public int StudentAge { set; get; }

        public string StudentAddress { set; get; }

        [Range(3,13, ErrorMessage = "Oop it just accept from 3 to 13")]
        public long StudentPhone { set; get; }
    }
}
