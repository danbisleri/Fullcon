using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnection
{
    public interface IRepositoryEntity : IDisposable
    {
        T Get<T>(int id) where T : Base;
        IQueryable<T> Query<T>() where T : Base;
        void Save<T>(T entidade) where T : Base;
        void SaveAll<T>(IEnumerable<T> entidade) where T : Base;
        void Delete<T>(T entidade) where T : Base;
        void Delete<T>(int id) where T : Base;
        void DeleteAll<T>(IEnumerable<T> entidades) where T : Base;
        void Commit();
    }
}
