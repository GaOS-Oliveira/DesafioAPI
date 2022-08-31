using System.ComponentModel.DataAnnotations;


namespace DesafioAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid Email.")]
        public string UserEmail { get; set; }
        [Required]
        [MinLength(8)]
        public string UserPassword { get; set; }
        public string ProfilePic { get; set; }
    }
}
