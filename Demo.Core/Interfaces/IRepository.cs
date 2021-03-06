﻿namespace Demo.Core.Interfaces
{
    using Infrastructure;

    public interface IRepository<T> where T : class
    {
        T Get(int id);

        PagedListResult<T> GetPagedList(PageableListQueryCommand<T> command);

        T Save(T obj);

        void Update(T obj);

        void Delete(int id);

        T Clone(int id);
    }
}
