using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI0001.Models.Class
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required,Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "nvarchar(20)")]
        public string Job { set; get; }
    }
}
