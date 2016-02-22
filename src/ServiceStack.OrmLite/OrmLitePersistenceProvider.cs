//
// ServiceStack.OrmLite: Light-weight POCO ORM for .NET and Mono
//
// Authors:
//   Demis Bellot (demis.bellot@gmail.com)
//
// Copyright 2010 Liquidbit Ltd.
//
// Licensed under the same terms of ServiceStack: new BSD license.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DataAccess;

namespace ServiceStack.OrmLite
{
	/// <summary>
	/// Allow for code-sharing between OrmLite, IPersistenceProvider and ICacheClient
	/// </summary>
	public class OrmLitePersistenceProvider
		: IBasicPersistenceProvider
	{
		protected string ConnectionString { get; set; }
		protected bool DisposeConnection = true;

		protected IDbConnection connection;
	    protected IOrmLiteSession session;
		public IDbConnection Connection
		{
			get
			{
				if (connection == null)
				{
				    var connStr = this.ConnectionString;
                    connection = connStr.OpenDbConnection();
				}
				return connection;
			}
		}

		public OrmLitePersistenceProvider(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public OrmLitePersistenceProvider(IOrmLiteSession session)
		{
			this.connection = session.Connection;
		    this.session = session;
			this.DisposeConnection = false;
		}

		private IDbCommand CreateCommand()
		{
			var cmd = this.Connection.CreateCommand();
			cmd.CommandTimeout = OrmLiteConfig.CommandTimeout;
			return cmd;
		}

		public T GetById<T>(object id)
			where T : class, new()
		{
			using (var dbCmd = CreateCommand())
			{
				return dbCmd.GetByIdOrDefault<T>(session, id);
			}
		}

		public IList<T> GetByIds<T>(ICollection ids)
			where T : class, new()
		{
			using (var dbCmd = CreateCommand())
			{
				return dbCmd.GetByIds<T>(session, ids);
			}
		}

		public T Store<T>(T entity)
			where T : class, new()
		{
			using (var dbCmd = CreateCommand())
			{
				return InsertOrUpdate(dbCmd, session, entity);
			}
		}

		private static T InsertOrUpdate<T>(IDbCommand dbCmd, IOrmLiteSession session, T entity)
			where T : class, new()
		{
			var id = IdUtils.GetId(entity);
			var existingEntity = dbCmd.GetByIdOrDefault<T>(session, id);
			if (existingEntity != null)
			{
				existingEntity.PopulateWith(entity);
				dbCmd.Update(entity);

				return existingEntity;
			}

			dbCmd.Insert(entity);
			return entity;
		}

		public void StoreAll<TEntity>(IEnumerable<TEntity> entities) 
			where TEntity : class, new()
		{
			using (var dbCmd = CreateCommand())
			using (var dbTrans = this.Connection.BeginTransaction())
			{
				foreach (var entity in entities)
				{
					InsertOrUpdate(dbCmd, session, entity);
				}
				dbTrans.Commit();
			}
		}

		public void Delete<T>(T entity)
			where T : class, new()
		{
			using (var dbCmd = CreateCommand())
			{
				dbCmd.Delete(entity);
			}
		}

		public void DeleteById<T>(object id) where T : class, new()
		{
			using (var dbCmd = CreateCommand())
			{
				dbCmd.DeleteById<T>(session, id);
			}
		}

		public void DeleteByIds<T>(ICollection ids) where T : class, new()
		{
			using (var dbCmd = this.CreateCommand())
			{
				dbCmd.DeleteByIds<T>(session, ids);
			}
		}

		public void DeleteAll<TEntity>() where TEntity : class, new()
		{
			using (var dbCmd = CreateCommand())
			{
				dbCmd.DeleteAll<TEntity>();
			}
		}

		public void Dispose()
		{
			if (!DisposeConnection) return;
			if (this.connection == null) return;
			
			this.connection.Dispose();
			this.connection = null;
		}
	}
}
