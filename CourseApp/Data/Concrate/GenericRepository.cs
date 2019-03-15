using CourseApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Models;

namespace CourseApp.Data.Concrate
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Remove<TEntity>(Get(id));
            _context.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            _context.Add<TEntity>(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
            _context.SaveChanges();
        }
    }
}
