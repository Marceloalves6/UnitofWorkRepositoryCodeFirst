using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [MaxLength(75)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(75)]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Senha { get; set; }
    }
}
