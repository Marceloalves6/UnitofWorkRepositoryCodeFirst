using DataAccess.EF;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork()
        {
            this.Context = new EFContext();
        }

        private DbContext Context { get; set; }

        private bool disposed = false;

        private ILancamentoRepository lancamentos;

        private IUsuarioRepository usuarios;

        private ICategoriaRepository categorias;

        public ILancamentoRepository Lancamentos
        {
            get
            {
                if (lancamentos == null)
                    lancamentos = new LancamentoRepository(this.Context);

                return lancamentos;
            }
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                if (usuarios == null)
                    usuarios = new UsuarioRepository(this.Context);

                return usuarios;
            }
        }

        public ICategoriaRepository Categorias
        {
            get
            {
                if (categorias == null)
                    categorias = new CategoriaRepository(this.Context);

                return categorias;
            }
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

    }
}
