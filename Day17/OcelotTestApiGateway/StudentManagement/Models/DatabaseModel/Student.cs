using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models.DatabaseModel
{
    public class Student
    {
        [Key]
        public int Id { set; get; }

        public string StudentName { set; get; } = string.Empty;

        public string StudentAddress { set; get; } = string.Empty;

        public int StudentPhone { set; get; } 
    }
}
