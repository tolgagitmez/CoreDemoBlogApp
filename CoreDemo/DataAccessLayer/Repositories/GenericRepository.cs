using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new Context();
            c.Remove(t);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            using var context = new Context();
            var entity = context.Set<T>().Find(id);
            if (entity == null)
            {
                // Varlık bulunamazsa hata fırlatılabilir veya farklı bir işlem yapılabilir.
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            return entity;
        }

        public List<T> GetListAll()
        {
            using var context = new Context();
            var entity = context.Set<T>().ToList();
            return entity;
        }

        public void Insert(T t)
        {
            using var c = new Context();
            c.Add(t);
            c.SaveChanges();
        }

		public List<T> GetListAll(Expression<Func<T, bool>> filter)
		{
			using var c = new Context();
            return c.Set<T>().Where(filter).ToList();
		}

		public void Update(T t)
        {
            using var c = new Context();
            c.Update(t);
            c.SaveChanges();
        }
    }
}
