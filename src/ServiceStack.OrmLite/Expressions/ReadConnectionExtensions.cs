using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace ServiceStack.OrmLite
{
    public static class ReadConnectionExtensions
    {
        [ThreadStatic]
        internal static string LastCommandText;

        public static SqlExpressionVisitor<T> CreateExpression<T>(this IOrmLiteSession session) 
        {
            return session.GetDialectProvider().ExpressionVisitor<T>();
        }

        public static T Exec<T>(this IOrmLiteSession session, Func<IDbCommand, T> filter)
        {
            var holdProvider = OrmLiteConfig.TSDialectProvider;
            try
            {
                var ormLiteDbConn = session.Connection as OrmLiteConnection;
                if (ormLiteDbConn != null)
                    OrmLiteConfig.TSDialectProvider = ormLiteDbConn.Factory.DialectProvider;

                using (var dbCmd = session.Connection.CreateCommand())
                {
                    dbCmd.Transaction = (ormLiteDbConn != null) ? ormLiteDbConn.Transaction : OrmLiteConfig.TSTransaction;
                    dbCmd.CommandTimeout = OrmLiteConfig.CommandTimeout;
                    var ret = filter(dbCmd);
                    LastCommandText = dbCmd.CommandText;
                    return ret;
                }
            }
            finally
            {
                OrmLiteConfig.TSDialectProvider = holdProvider;
            }
        }

        public static void Exec(this IOrmLiteSession session, Action<IDbCommand> filter)
        {
            var dialectProvider = OrmLiteConfig.DialectProvider;
            try
            {
                var ormLiteDbConn = session.Connection as OrmLiteConnection;
                if (ormLiteDbConn != null)
                    OrmLiteConfig.DialectProvider = ormLiteDbConn.Factory.DialectProvider;

                using (var dbCmd = session.Connection.CreateCommand())
                {
                    dbCmd.Transaction = (ormLiteDbConn != null) ? ormLiteDbConn.Transaction : OrmLiteConfig.TSTransaction;
                    dbCmd.CommandTimeout = OrmLiteConfig.CommandTimeout;

                    filter(dbCmd);
                    LastCommandText = dbCmd.CommandText;
                }
            }
            finally
            {
                OrmLiteConfig.DialectProvider = dialectProvider;
            }
        }

        public static IEnumerable<T> ExecLazy<T>(this IOrmLiteSession session, Func<IDbCommand, IEnumerable<T>> filter)
        {
            var dialectProvider = OrmLiteConfig.DialectProvider;
            try
            {
                var ormLiteDbConn = session.Connection as OrmLiteConnection;
                if (ormLiteDbConn != null)
                    OrmLiteConfig.DialectProvider = ormLiteDbConn.Factory.DialectProvider;

                using (var dbCmd = session.Connection.CreateCommand())
                {
                    dbCmd.Transaction = (ormLiteDbConn != null) ? ormLiteDbConn.Transaction : OrmLiteConfig.TSTransaction;
                    dbCmd.CommandTimeout = OrmLiteConfig.CommandTimeout;

                    var results = filter(dbCmd);
                    LastCommandText = dbCmd.CommandText;
                    foreach (var item in results)
                    {
                        yield return item;
                    }
                }
            }
            finally
            {
                OrmLiteConfig.DialectProvider = dialectProvider;
            }
        }

        public static IDbTransaction OpenTransaction(this IOrmLiteSession session)
        {
            return new OrmLiteTransaction(session.Connection, session.Connection.BeginTransaction());
        }

        public static IDbTransaction OpenTransaction(this IOrmLiteSession session, IsolationLevel isolationLevel)
        {
            return new OrmLiteTransaction(session.Connection, session.Connection.BeginTransaction(isolationLevel));
        }

        public static IOrmLiteDialectProvider GetDialectProvider(this IOrmLiteSession session)
        {
            var ormLiteDbConn = session.Connection as OrmLiteConnection;
            return ormLiteDbConn != null 
                ? ormLiteDbConn.Factory.DialectProvider 
                : OrmLiteConfig.DialectProvider;
        }

        public static SqlExpressionVisitor<T> CreateExpression<T>()
        {
            return OrmLiteConfig.DialectProvider.ExpressionVisitor<T>();
        }

        public static List<T> Select<T>(this IOrmLiteSession session, Expression<Func<T, bool>> predicate)
        {
            return session.Exec(dbCmd => dbCmd.Select(session, predicate));
        }

        public static List<T> Select<T>(this IOrmLiteSession session, Func<SqlExpressionVisitor<T>, SqlExpressionVisitor<T>> expression)
        {
            return session.Exec(dbCmd => dbCmd.Select(session, expression));
        }

        public static List<T> Select<T>(this IOrmLiteSession session, SqlExpressionVisitor<T> expression)
        {
            return session.Exec(dbCmd => dbCmd.Select(session, expression));
        }

        /// <summary>
        /// Performs the same function as Select() except arguments are passed as parameters to the generated SQL.
        /// Currently does not support complex SQL.## ,  .StartsWith(), EndsWith() and Contains() operators
        /// </summary>
        public static List<T> SelectParam<T>(this IOrmLiteSession session, Expression<Func<T, bool>> predicate)
        {
            return session.Exec(dbCmd => dbCmd.SelectParam(session, predicate));
        }

        public static T First<T>(this IOrmLiteSession session, Expression<Func<T, bool>> predicate)
        {
            return session.Exec(dbCmd => dbCmd.First(session, predicate));
        }

        public static T First<T>(this IOrmLiteSession session, SqlExpressionVisitor<T> expression)
        {
            return session.Exec(dbCmd => dbCmd.First(session, expression));
        }

        public static T FirstOrDefault<T>(this IOrmLiteSession session, Expression<Func<T, bool>> predicate)
        {
            return session.Exec(dbCmd => dbCmd.FirstOrDefault(session, predicate));
        }

        public static T FirstOrDefault<T>(this IOrmLiteSession session, SqlExpressionVisitor<T> expression)
        {
            return session.Exec(dbCmd => dbCmd.FirstOrDefault(session, expression));
        }

        public static TKey GetScalar<T, TKey>(this IOrmLiteSession session, Expression<Func<T, TKey>> field)
        {
            return session.Exec(dbCmd => dbCmd.GetScalar(field));
        }

        public static TKey GetScalar<T, TKey>(this IOrmLiteSession session, Expression<Func<T, TKey>> field,
                                             Expression<Func<T, bool>> predicate)
        {
            return session.Exec(dbCmd => dbCmd.GetScalar(field, predicate));
        }

        public static long Count<T>(this IOrmLiteSession session, SqlExpressionVisitor<T> expression)
        {
            return session.Exec(dbCmd => dbCmd.Count(expression));
        }

        public static long Count<T>(this IOrmLiteSession session, Expression<Func<T, bool>> expression)
        {
            return session.Exec(dbCmd => dbCmd.Count(expression));
        }

        public static long Count<T>(this IOrmLiteSession session)
        {
            SqlExpressionVisitor<T> expression = OrmLiteConfig.DialectProvider.ExpressionVisitor<T>();
            return session.Exec(dbCmd => dbCmd.Count(expression));
        }
    }
}
