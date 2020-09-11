using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnection
{
    public abstract class RepositoryEntity : IRepositoryEntity
    {
        private readonly DbContext _context;

        public RepositoryEntity()
        {
            _context = new CoreDbContext();
        }

        public T Get<T>(int id) where T : Base
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : Base
        {
            return _context.Set<T>();
        }

        public void Save<T>(T entidade) where T : Base
        {
            var exists = Get<T>(entidade.Id);
            if (exists == null)
                _context.Set<T>().Add(entidade);
            else
                _context.Set<T>().Update(entidade);
        } 

        public void SaveAll<T>(IEnumerable<T> entidade) where T : Base
        {
            foreach (var e in entidade)
            {
                this.Save(e);
            }
        }

        public void Delete<T>(T entidade) where T : Base
        {
            _context.Set<T>().Attach(entidade);
            _context.Set<T>().Remove(entidade);
        }

        public void Delete<T>(int id) where T : Base
        {
            var entidade = Get<T>(id);
            if (entidade == null)
                throw new System.Exception($"Registro não localizada pelo id {id}");

            _context.Entry(entidade).State = EntityState.Deleted;
            _context.Set<T>().Remove(entidade);
        }

        public void DeleteAll<T>(IEnumerable<T> entidades) where T : Base
        {
            foreach (var e in entidades)
            {
                this.Delete(e);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
