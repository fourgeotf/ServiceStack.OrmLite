using System.Data;

namespace ServiceStack.OrmLite
{
    public interface IOrmLiteSession
    {
        IDbConnection Connection { get; set; }

        ModelDefinition<T> GetModelDefinition<T>();

        T CreateInstance<T>();
    }
}
