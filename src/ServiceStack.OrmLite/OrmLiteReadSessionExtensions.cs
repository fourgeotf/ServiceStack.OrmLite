using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace ServiceStack.OrmLite
{
    public static class OrmLiteReadSessionExtensions
    {
        public static List<T> Select<T>(this IOrmLiteSession session) 
        {
            return session.Exec(dbCmd => dbCmd.Select<T>(session));
        }

        public static List<T> Select<T>(this IOrmLiteSession session, string sqlFilter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.Select<T>(session, sqlFilter, filterParams));
        }

        public static List<TModel> Select<TModel>(this IOrmLiteSession session, Type fromTableType)
        {
            return session.Exec(dbCmd => dbCmd.Select<TModel>(session, fromTableType));
        }

        public static List<TModel> Select<TModel>(this IOrmLiteSession session, Type fromTableType, string sqlFilter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.Select<TModel>(session, fromTableType, sqlFilter, filterParams));
        }

        public static IEnumerable<T> Each<T>(this IOrmLiteSession session)
        {
            return session.ExecLazy(dbCmd => dbCmd.Each<T>(session));
        }

        public static IEnumerable<T> Each<T>(this IOrmLiteSession session, string filter, params object[] filterParams)
        {
            return session.ExecLazy(dbCmd => dbCmd.Each<T>(session, filter, filterParams));
        }
        
        public static T First<T>(this IOrmLiteSession session, string filter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.First<T>(filter, filterParams));
        }

        /// <summary>
        /// Alias for First
        /// </summary>
        public static T Single<T>(this IOrmLiteSession session, string filter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.First<T>(filter, filterParams));
        }

        public static T First<T>(this IOrmLiteSession session, string filter)
        {
            return session.Exec(dbCmd => dbCmd.First<T>(filter));
        }

        /// <summary>
        /// Alias for First
        /// </summary>
        public static T Single<T>(this IOrmLiteSession session, string filter)
        {
            return session.Exec(dbCmd => dbCmd.First<T>(filter));
        }

        public static T FirstOrDefault<T>(this IOrmLiteSession session, string filter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.FirstOrDefault<T>(filter, filterParams));
        }

        /// <summary>
        /// Alias for FirstOrDefault
        /// </summary>
        public static T SingleOrDefault<T>(this IOrmLiteSession session, string filter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.FirstOrDefault<T>(filter, filterParams));
        }

        public static T FirstOrDefault<T>(this IOrmLiteSession session, string filter)
        {
            return session.Exec(dbCmd => dbCmd.FirstOrDefault<T>(filter));
        }

        /// <summary>
        /// Alias for FirstOrDefault
        /// </summary>
        public static T SingleOrDefault<T>(this IOrmLiteSession session, string filter)
        {
            return session.Exec(dbCmd => dbCmd.FirstOrDefault<T>(filter));
        }

        public static T GetById<T>(this IOrmLiteSession session, object idValue)
        {
            return session.Exec(dbCmd => dbCmd.GetById<T>(session, idValue));
        }

        /// <summary>
        /// Performs an GetById() except argument is passed as a parameter to the generated SQL
        /// </summary>
        public static T GetByIdParam<T>(this IOrmLiteSession session, object idValue)
        {
            return session.Exec(dbCmd => dbCmd.GetByIdParam<T>(session, idValue));
        }

        /// <summary>
        /// Alias for GetById
        /// </summary>
        public static T Id<T>(this IOrmLiteSession session, object idValue)
        {
            return session.Exec(dbCmd => dbCmd.GetById<T>(session, idValue));
        }
        
        public static T QueryById<T>(this IOrmLiteSession session, object value) 
        {
            return session.Exec(dbCmd => dbCmd.QueryById<T>(session, value));
        }

        public static T SingleWhere<T>(this IOrmLiteSession session, string name, object value)
        {
            return session.Exec(dbCmd => dbCmd.SingleWhere<T>(session, name, value));
        }

        public static T QuerySingle<T>(this IOrmLiteSession session, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.QuerySingle<T>(session, anonType));
        }

        public static T QuerySingle<T>(this IOrmLiteSession session, string sql, object anonType = null)
        {
            return session.Exec(dbCmd => dbCmd.QuerySingle<T>(session, sql, anonType));
        }

        public static List<T> Where<T>(this IOrmLiteSession session, string name, object value)
        {
            return session.Exec(dbCmd => dbCmd.Where<T>(session, name, value));
        }

        public static List<T> Where<T>(this IOrmLiteSession session, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.Where<T>(session, anonType));
        }

        public static List<T> Where<T>(this IOrmLiteSession session, System.Linq.Expressions.Expression<Func<T,bool>> Predicate)
        {
            return (List<T>) session.Select<T>(Predicate);
        }

        public static List<T> Query<T>(this IOrmLiteSession session, string sql)
        {
            return session.Exec(dbCmd => dbCmd.Query<T>(session, sql));
        }

        public static List<T> Query<T>(this IOrmLiteSession session, string sql, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.Query<T>(session, sql, anonType));
        }

        public static List<T> Query<T>(this IOrmLiteSession session, string sql, Dictionary<string, object> dict)
        {
            return session.Exec(dbCmd => dbCmd.Query<T>(session, sql, dict));
        }

        public static int ExecuteNonQuery(this IOrmLiteSession session, string sql)
        {
            return session.Exec(dbCmd => dbCmd.ExecuteNonQuery(sql));
        }

        public static int ExecuteNonQuery(this IOrmLiteSession session, string sql, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.ExecuteNonQuery(sql, anonType));
        }

        public static int ExecuteNonQuery(this IOrmLiteSession session, string sql, Dictionary<string, object> dict)
        {
            return session.Exec(dbCmd => dbCmd.ExecuteNonQuery(sql, dict));
        }

        public static T QueryScalar<T>(this IOrmLiteSession session, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.QueryScalar<T>(session, anonType));
        }

        public static T QueryScalar<T>(this IOrmLiteSession session, string sql, object anonType = null)
        {
            return session.Exec(dbCmd => dbCmd.QueryScalar<T>(session, sql, anonType));
        }
        
        public static List<T> SqlList<T>(this IOrmLiteSession session, string sql)
        {
            return session.Exec(dbCmd => dbCmd.SqlList<T>(session, sql));
        }

        public static List<T> SqlList<T>(this IOrmLiteSession session, string sql, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.SqlList<T>(session, sql, anonType));
        }

        public static List<T> SqlList<T>(this IOrmLiteSession session, string sql, Dictionary<string, object> dict)
        {
            return session.Exec(dbCmd => dbCmd.SqlList<T>(session, sql, dict));
        }

        public static T SqlScalar<T>(this IOrmLiteSession session, string sql, object anonType = null)
        {
            return session.Exec(dbCmd => dbCmd.SqlScalar<T>(session, sql, anonType));
        }

        public static T SqlScalar<T>(this IOrmLiteSession session, string sql, Dictionary<string, object> dict)
        {
            return session.Exec(dbCmd => dbCmd.SqlScalar<T>(sql, dict));
        }
        
        public static List<T> ByExampleWhere<T>(this IOrmLiteSession session, object anonType)
        {
            return session.Exec(dbCmd => dbCmd.ByExampleWhere<T>(session, anonType));
        }

        public static List<T> ByExampleWhere<T>(this IOrmLiteSession session, object anonType, bool excludeNulls)
        {
            return session.Exec(dbCmd => dbCmd.ByExampleWhere<T>(session, anonType, excludeNulls));
        }

        public static List<T> QueryByExample<T>(this IOrmLiteSession session, string sql, object anonType = null)
        {
            return session.Exec(dbCmd => dbCmd.QueryByExample<T>(session, sql, anonType));
        }

        public static IEnumerable<T> QueryEach<T>(this IOrmLiteSession session, string sql, object anonType = null)
        {
            return session.ExecLazy(dbCmd => dbCmd.QueryEach<T>(session, sql, anonType));
        }

        public static IEnumerable<T> EachWhere<T>(this IOrmLiteSession session, object anonType)
        {
            return session.ExecLazy(dbCmd => dbCmd.EachWhere<T>(session, anonType));
        }

        public static T GetByIdOrDefault<T>(this IOrmLiteSession session, object idValue)
        {
            return session.Exec(dbCmd => dbCmd.GetByIdOrDefault<T>(session, idValue));
        }

        /// <summary>
        /// Alias for GetByIds
        /// </summary>
        public static T IdOrDefault<T>(this IOrmLiteSession session, object idValue)
        {
            return session.Exec(dbCmd => dbCmd.GetByIdOrDefault<T>(session, idValue));
        }

        public static List<T> GetByIds<T>(this IOrmLiteSession session, IEnumerable idValues)
        {
            return session.Exec(dbCmd => dbCmd.GetByIds<T>(session, idValues));
        }

        /// <summary>
        /// Alias for GetByIds
        /// </summary>
        public static List<T> Ids<T>(this IOrmLiteSession session, IEnumerable idValues)
        {
            return session.Exec(dbCmd => dbCmd.GetByIds<T>(session, idValues));
        }

        public static T GetScalar<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetScalar<T>(sql, sqlParams));
        }

        /// <summary>
        /// Alias for Scalar
        /// </summary>
        public static T Scalar<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetScalar<T>(sql, sqlParams));
        }

        public static long GetLastInsertId(this IOrmLiteSession session)
        {
            return session.Exec(dbCmd => dbCmd.GetLastInsertId());
        }

        public static List<T> GetFirstColumn<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetFirstColumn<T>(sql, sqlParams));
        }

        public static List<T> GetList<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetList<T>(sql, sqlParams));
        }

        /// <summary>
        /// Alias for GetList
        /// </summary>
        public static List<T> List<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetList<T>(sql, sqlParams));
        }

        public static HashSet<T> GetFirstColumnDistinct<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetFirstColumnDistinct<T>(sql, sqlParams));
        }

        public static HashSet<T> GetHashSet<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetHashSet<T>(sql, sqlParams));
        }

        /// <summary>
        /// Alias for GetHashSet
        /// </summary>
        public static HashSet<T> HashSet<T>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetHashSet<T>(sql, sqlParams));
        }

        public static Dictionary<K, List<V>> GetLookup<K, V>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetLookup<K, V>(sql, sqlParams));
        }

        /// <summary>
        /// Alias for GetLookup
        /// </summary>
        public static Dictionary<K, List<V>> Lookup<K, V>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetLookup<K, V>(sql, sqlParams));
        }

        public static Dictionary<K, V> GetDictionary<K, V>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetDictionary<K, V>(sql, sqlParams));
        }

        /// <summary>
        /// Alias for GetDictionary
        /// </summary>
        public static Dictionary<K, V> Dictionary<K, V>(this IOrmLiteSession session, string sql, params object[] sqlParams)
        {
            return session.Exec(dbCmd => dbCmd.GetDictionary<K, V>(sql, sqlParams));
        }

        // somo aditional methods

        public static bool HasChildren<T>(this IOrmLiteSession session, object record)
        {
            return session.Exec(dbCmd => dbCmd.HasChildren<T>(record));
        }

        public static bool Exists<T>(this IOrmLiteSession session, string sqlFilter, params object[] filterParams)
        {
            return session.Exec(dbCmd => dbCmd.Exists<T>(sqlFilter, filterParams));
        }

        public static bool Exists<T>(this IOrmLiteSession session, object record)
        {
            return session.Exec(dbCmd => dbCmd.Exists<T>(record));
        }

        // procedures ...		
        public static List<TOutputModel> SelectFromProcedure<TOutputModel>(this IOrmLiteSession session,
            object fromObjWithProperties)
        {
            return session.Exec(dbCmd => dbCmd.SelectFromProcedure<TOutputModel>(session, fromObjWithProperties));
        }

        public static List<TOutputModel> SelectFromProcedure<TOutputModel>(this IOrmLiteSession session,
            object fromObjWithProperties,
            string sqlFilter,
            params object[] filterParams)
            where TOutputModel : new()
        {
            return session.Exec(dbCmd => dbCmd.SelectFromProcedure<TOutputModel>(session,
                fromObjWithProperties, sqlFilter, filterParams));
        }

        public static long GetLongScalar(this IOrmLiteSession session)
        {
            return session.Exec(dbCmd => dbCmd.GetLongScalar());
        }			
    }
}
