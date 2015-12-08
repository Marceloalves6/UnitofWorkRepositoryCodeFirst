using DataAccess.EF;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<TObject> : IRepository<TObject> where TObject : class
    {

        protected DbContext Context { get; set; }
        private bool shareContext = false;

        public Repository(DbContext Context)
        {
            this.Context = Context;
        }

        protected DbSet<TObject> DbSet
        {
            get
            {
                return this.Context.Set<TObject>();
            }
        }

        public IQueryable<TObject> All()
        {
            return this.DbSet.AsQueryable();
        }

        public IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return this.DbSet.Where(predicate).AsQueryable<TObject>();
        }

        public IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, dynamic>>[] Includes)
        {
            return Includes.Aggregate(this.DbSet.AsQueryable(), (current, include) => current.Include(include)).Where(predicate).AsQueryable<TObject>();
        }

        public IQueryable<TObject> Filter<Key>(Expression<Func<TObject, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();

            return resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public TObject Find(params object[] keys)
        {
            return this.DbSet.Find(keys);
        }

        public TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return this.DbSet.FirstOrDefault(predicate);
        }

        public TObject Create(TObject t)
        {
            var newEntry = DbSet.Add(t);

            if (!shareContext)
                Context.SaveChanges();

            return newEntry;
        }

        public int  Delete(TObject t)
        {
            DbSet.Remove(t);

            if (!shareContext)
                return Context.SaveChanges();

            return 0;
        }

        public int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = this.Filter(predicate);

            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }

            if (!shareContext)
                return Context.SaveChanges();

            return 0;
        }

        public int Update(TObject t)
        {
            var entry = Context.Entry(t);
            DbSet.Attach(t);
            entry.State = EntityState.Modified;
            if (!shareContext)
                return Context.SaveChanges();
            return 0;
        }

        public int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public void Dispose()
        {
            if (shareContext && (Context != null))
                Context.Dispose();
        }


       
    }
}
