using DLA.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service
{
    public class GenericClass<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericClass(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<T> Add(T value)
        {
            Entity().Add(value);
           await SaveChanges();
            return value;
        }

        public async Task<T>GetById(int id)
        {
            var value =  Entity().Find(id);
            
            return value;
        }
        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>(); 
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);  //_context.Books.Include(x=>x.Students).ToList();
            }
            return query.ToList();
        }
        public async Task<List<T>> GetAll()
        {
            var value = Entity().Include(x=>x).ToList();
            return value;
        }
        public void Update(T value)
        {
            Entity().Update(value);
            SaveChanges();
        }

        public async Task Delete(T value)
        {
            //Entity().Remove(value);
            await Task.Run(() => Entity().Remove(value));
          await SaveChanges();
        }

        private DbSet<T> Entity() => _context.Set<T>();
        private async Task SaveChanges()=>  _context.SaveChanges();
        
        

        

    }
   

}
