using System;
using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Postagem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(280)]
        public string PostText { get; set; }
        public string PostImage { get; set; }
        // PostDate é adicionado automaticamente, sempre leva o valor da data e horário atual.
        public DateTime PostDate { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public int GrupoId { get; set; }
    }
}
