using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF
{
    public class EFContext: DbContext
    {
        public EFContext():base(){}
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
         
    }
}
