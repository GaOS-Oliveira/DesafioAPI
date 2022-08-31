using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        [Required]
        public string GroupName { get; set; }
    }
}
