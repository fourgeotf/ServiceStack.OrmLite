using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServiceStack.OrmLite
{
    public static class OrmLiteWriteSessionExtensions
    {
        public static bool TableExists(this IOrmLiteSession session, string tableName)
        {
            return session.GetDialectProvider().DoesTableExist(session, tableName);
        }

        public static void CreateTables(this IOrmLiteSession session, bool overwrite, params Type[] tableTypes)
        {
            session.Exec(dbCmd => dbCmd.CreateTables(overwrite, tableTypes));
        }

        public static void CreateTableIfNotExists(this IOrmLiteSession session, params Type[] tableTypes)
        {
            session.Exec(dbCmd => dbCmd.CreateTables(false, tableTypes));
        }

        public static void DropAndCreateTables(this IOrmLiteSession session, params Type[] tableTypes)
        {
            session.Exec(dbCmd => dbCmd.CreateTables(true, tableTypes));
        }

        /// <summary>
        /// Alias for CreateTableIfNotExists
        /// </summary>
        public static void CreateTable<T>(this IOrmLiteSession session)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.CreateTable<T>());
        }

        public static void CreateTable<T>(this IOrmLiteSession session, bool overwrite)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.CreateTable<T>(overwrite));
        }

        public static void CreateTableIfNotExists<T>(this IOrmLiteSession session)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.CreateTable<T>(false));
        }

        public static void DropAndCreateTable<T>(this IOrmLiteSession session)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.CreateTable<T>(true));
        }

        public static void CreateTable(this IOrmLiteSession session, bool overwrite, Type modelType)
        {
            session.Exec(dbCmd => dbCmd.CreateTable(overwrite, modelType));
        }

        public static void CreateTableIfNotExists(this IOrmLiteSession session, Type modelType)
        {
            session.Exec(dbCmd => dbCmd.CreateTable(false, modelType));
        }

        public static void DropAndCreateTable(this IOrmLiteSession session, Type modelType)
        {
            session.Exec(dbCmd => dbCmd.CreateTable(true, modelType));
        }

        public static void DropTables(this IOrmLiteSession session, params Type[] tableTypes)
        {
            session.Exec(dbCmd => dbCmd.DropTables(tableTypes));
        }

        public static void DropTable(this IOrmLiteSession session, Type modelType)
        {
            session.Exec(dbCmd => dbCmd.DropTable(modelType));
        }

        public static void DropTable<T>(this IOrmLiteSession session)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.DropTable<T>(session));
        }

        public static string GetLastSql(this IOrmLiteSession session)
        {
            return ReadConnectionExtensions.LastCommandText;
        }

        public static int ExecuteSql(this IOrmLiteSession session, string sql)
        {
            return session.Exec(dbCmd => dbCmd.ExecuteSql(sql));
        }

        public static void Update<T>(this IOrmLiteSession session, params T[] objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.Update(objs));
        }

        public static void UpdateAll<T>(this IOrmLiteSession session, IEnumerable<T> objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.UpdateAll(objs));
        }

        /// <summary>
        /// Performs an Update<T>() except arguments are passed as parameters to the generated SQL
        /// </summary>
        public static void UpdateParam<T>(this IOrmLiteSession session, T obj) where T : new()
        {
            session.Exec(dbCmd =>
            {
                using (var updateStmt = session.Connection.CreateUpdateStatement(obj))
                {
                    dbCmd.CopyParameterizedStatementTo(updateStmt);
                }
                dbCmd.ExecuteNonQuery();
            });
        }

        public static void Delete<T>(this IOrmLiteSession session, params T[] objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.Delete(objs));
        }

        public static void DeleteAll<T>(this IOrmLiteSession session, IEnumerable<T> objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.DeleteAll(objs));
        }

        public static void DeleteById<T>(this IOrmLiteSession session, object id)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.DeleteById<T>(session, id));
        }

        /// <summary>
        /// Performs a DeleteById() except argument is passed as a parameter to the generated SQL
        /// </summary>
        public static void DeleteByIdParam<T>(this IOrmLiteSession session, object id) where T : new()
        {
            session.Exec(dbCmd => dbCmd.DeleteByIdParam<T>(session, id));
        }

        public static void DeleteByIds<T>(this IOrmLiteSession session, IEnumerable idValues)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.DeleteByIds<T>(session, idValues));
        }

        public static void DeleteAll<T>(this IOrmLiteSession session)
        {
            session.Exec(dbCmd => dbCmd.DeleteAll<T>());
        }

        public static void DeleteAll(this IOrmLiteSession session, Type tableType)
        {
            session.Exec(dbCmd => dbCmd.DeleteAll(tableType));
        }

        public static void Delete<T>(this IOrmLiteSession session, string sqlFilter, params object[] filterParams)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.Delete<T>(sqlFilter, filterParams));
        }

        public static void Delete(this IOrmLiteSession session, Type tableType, string sqlFilter, params object[] filterParams)
        {
            session.Exec(dbCmd => dbCmd.Delete(tableType, sqlFilter, filterParams));
        }

        public static void Save<T>(this IOrmLiteSession session, T obj)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.Save(session, obj));
        }

        public static void Insert<T>(this IOrmLiteSession session, params T[] objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.Insert(objs));
        }

        public static void InsertAll<T>(this IOrmLiteSession session, IEnumerable<T> objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.InsertAll(objs));
        }

        /// <summary>
        /// Performs an Insert() except arguments are passed as parameters to the generated SQL
        /// </summary>
        public static long InsertParam<T>(this IOrmLiteSession session, T obj, bool selectIdentity = false)
            where T : new()
        {
            return session.Exec(dbCmd =>
            {
                using (var insertStmt = session.Connection.CreateInsertStatement(obj))
                {
                    dbCmd.CopyParameterizedStatementTo(insertStmt);
                }

                if (selectIdentity)
                    return OrmLiteConfig.DialectProvider.InsertAndGetLastInsertId<T>(dbCmd);

                dbCmd.ExecuteNonQuery();
                return -1;
            });
        }

        private static void CopyParameterizedStatementTo(this IDbCommand dbCmd, IDbCommand tmpStmt)
        {
            dbCmd.CommandText = tmpStmt.CommandText;

            //Instead of creating new generic DbParameters, copy them from the "dummy" IDbCommand, 
            //to keep provider specific information. E.g: SqlServer "datetime2" dbtype
            //We must first create a temp list, as DbParam can't belong to two DbCommands
            var tmpParams = new List<IDbDataParameter>(tmpStmt.Parameters.Count);
            tmpParams.AddRange(tmpStmt.Parameters.Cast<IDbDataParameter>());

            tmpStmt.Parameters.Clear();

            tmpParams.ForEach(x => dbCmd.Parameters.Add(x));
        }

        public static void Save<T>(this IOrmLiteSession session, params T[] objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.Save(session, objs));
        }

        public static void SaveAll<T>(this IOrmLiteSession session, IEnumerable<T> objs)
            where T : new()
        {
            session.Exec(dbCmd => dbCmd.SaveAll(session, objs));
        }

        public static IDbTransaction BeginTransaction(this IOrmLiteSession session)
        {
            return session.Exec(dbCmd => dbCmd.BeginTransaction());
        }

        public static IDbTransaction BeginTransaction(this IOrmLiteSession session, IsolationLevel isolationLevel)
        {
            return session.Exec(dbCmd => dbCmd.BeginTransaction(isolationLevel));
        }

        // Procedures
        public static void ExecuteProcedure<T>(this IOrmLiteSession session, T obj)
        {
            session.Exec(dbCmd => dbCmd.ExecuteProcedure(obj));
        }
    }
}
