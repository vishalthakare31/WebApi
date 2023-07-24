using System.ComponentModel.DataAnnotations;

namespace WebApi.dto
{
    public  class StudentDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        public string City { get; set; }


    }
}
