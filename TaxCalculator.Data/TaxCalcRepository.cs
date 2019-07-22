using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Data
{
    public class TaxCalcRepository<TEntity> : ISqlRepository<TEntity> where TEntity : class
    {
        private readonly ITaxCalcDbContextFactory _dbContextFactory;
        // protected ILogger Logger;

        public TaxCalcRepository(ITaxCalcDbContextFactory dbContextFactory/*, ILogger logger*/)
        {
            _dbContextFactory = dbContextFactory;
            // Logger = logger;
        }

        protected TaxCalcDbContext DbContext => _dbContextFactory?.DbContext;

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<TEntity> GetEntity(object id)

        {
            var entity = await DbContext.FindAsync<TEntity>(id);
            return entity;
        }

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddEntity(TEntity entity)
        {
            try
            {
                var result = await DbContext.AddAsync<TEntity>(entity);
                await DbContext.SaveChangesAsync();
                return result.Entity;
            }

            catch (Exception ex)
            {
                // Logger.Error(ex, "Unhandled Exception");
                throw;
            }
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            DbContext.Update<TEntity>(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEntity(object id)
        {
            var entity = await DbContext.FindAsync<TEntity>(id);
            if (entity != null)
            {
                DbContext.Remove<TEntity>(entity);
                await DbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}