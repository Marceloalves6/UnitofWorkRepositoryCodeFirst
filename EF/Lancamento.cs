using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF
{
    public class Lancamento
    {
        [Key]
        public int IdLancamento { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        [MaxLength(1)]
        public string TipoLancamento { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime DataAlteracao { get; set; }
    }
}
