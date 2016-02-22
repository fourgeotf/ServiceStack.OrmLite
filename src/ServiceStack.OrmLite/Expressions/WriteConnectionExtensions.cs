using System;
using System.Data;
using System.Linq.Expressions;

namespace ServiceStack.OrmLite
{
    public static class WriteConnectionExtensions
    {
        public static int UpdateOnly<T>(this IOrmLiteSession session, T model, Func<SqlExpressionVisitor<T>, SqlExpressionVisitor<T>> onlyFields)
        {
            return session.Exec(dbCmd => dbCmd.UpdateOnly(model, onlyFields));
        }

        public static int UpdateOnly<T>(this IOrmLiteSession session, T model, SqlExpressionVisitor<T> onlyFields)
        {
            return session.Exec(dbCmd => dbCmd.UpdateOnly(model, onlyFields));
        }

        public static int UpdateOnly<T, TKey>(this IOrmLiteSession session, T obj,
            Expression<Func<T, TKey>> onlyFields = null,
            Expression<Func<T, bool>> where = null)
        {
            return session.Exec(dbCmd => dbCmd.UpdateOnly(obj, onlyFields, where));
        }

        public static int UpdateNonDefaults<T>(this IOrmLiteSession session, T item, Expression<Func<T, bool>> where)
        {
            return session.Exec(dbCmd => dbCmd.UpdateNonDefaults(item, where));
        }

        public static int Update<T>(this IOrmLiteSession session, T item, Expression<Func<T, bool>> where)
        {
            return session.Exec(dbCmd => dbCmd.Update(item, where));
        }

        public static int Update<T>(this IOrmLiteSession session, object updateOnly, Expression<Func<T, bool>> where = null)
        {
            return session.Exec(dbCmd => dbCmd.Update(updateOnly, where));
        }

        public static int Update<T>(this IOrmLiteSession session, string set = null, string where = null)
        {
            return session.Exec(dbCmd => dbCmd.Update<T>(set, where));
        }

        public static int Update(this IOrmLiteSession session, string table = null, string set = null, string where = null)
        {
            return session.Exec(dbCmd => dbCmd.Update(table, set, where));
        }

        public static void InsertOnly<T>(this IOrmLiteSession session, T obj, Func<SqlExpressionVisitor<T>, SqlExpressionVisitor<T>> onlyFields)
        {
            session.Exec(dbCmd => dbCmd.InsertOnly(obj, onlyFields));
        }

        public static void InsertOnly<T>(this IOrmLiteSession session, T obj, SqlExpressionVisitor<T> onlyFields)
        {
            session.Exec(dbCmd => dbCmd.InsertOnly(obj, onlyFields));
        }

        public static int Delete<T>(this IOrmLiteSession session, Expression<Func<T, bool>> where)
        {
            return session.Exec(dbCmd => dbCmd.Delete(where));
        }

        public static int Delete<T>(this IOrmLiteSession session, Func<SqlExpressionVisitor<T>, SqlExpressionVisitor<T>> where)
        {
            return session.Exec(dbCmd => dbCmd.Delete(where));
        }

        public static int Delete<T>(this IOrmLiteSession session, SqlExpressionVisitor<T> where)
        {
            return session.Exec(dbCmd => dbCmd.Delete(where));
        }

        public static int Delete<T>(this IOrmLiteSession session, string where = null)
        {
            return session.Exec(dbCmd => dbCmd.Delete<T>(where));
        }

        public static int Delete(this IOrmLiteSession session, string table = null, string where = null)
        {
            return session.Exec(dbCmd => dbCmd.Delete(table, where));
        }         
    }
}
