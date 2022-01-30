using CoreLayer.IServices;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ShopContext context;
		private DbSet<T> entities;
		string errorMessage = string.Empty;

		public GenericRepository(ShopContext context)
		{
			this.context = context;
			entities = context.Set<T>();
		}
		public IQueryable<T> GetAll()
		{
			return entities.AsQueryable();
		}

		public T Get(int id)
		{
			return entities.Find(id);
		}
		public IQueryable<T> GetQueryable(int id)
		{
			return entities.Where(x => x.Id == id).AsQueryable();
		}
		public void Insert(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.AddAsync(entity);
			SaveChange();
		}

		public void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Update(entity);
			SaveChange();
		}

		public bool Delete(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			try
			{
				entities.Remove(entity);
				SaveChange();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		private void SaveChange()
		{
			context.SaveChanges();
		}
	}
}
