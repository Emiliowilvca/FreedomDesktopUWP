using System;
using System.Collections.Generic;

namespace Freedom.Core.BaseClass.InterfaceDatabase
{
    public interface IGet<T> : IGetAll<T>, IGetFirstOrDefault<T>, IGetById<T> where T : class
    {
    }

    public interface IGetAll<T> where T : class
    {
        List<T> GetAll();
    }

    public interface IGetFirstOrDefault<T> where T : class
    {
        T GetFirstOrDefault();
    }

    public interface IGetById<T> where T : class
    {
        T GetById(Guid id);
    }
}