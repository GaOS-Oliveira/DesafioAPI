using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class UsuarioGrupo
    {
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int GrupoId { get; set; }
    }
}
