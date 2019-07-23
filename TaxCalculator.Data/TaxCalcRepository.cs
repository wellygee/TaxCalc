using System;
using System.Threading.Tasks;

namespace TaxCalculator.Data
{
    public class TaxCalcRepository<TEntity> : ISqlRepository<TEntity> where TEntity : class
    {
        private readonly ITaxCalcDbContextFactory _dbContextFactory;

        public TaxCalcRepository(ITaxCalcDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected TaxCalcDbContext DbContext => _dbContextFactory?.DbContext;

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id">The id</param>

        public async Task<TEntity> GetEntity(object id)

        {
            var entity = await DbContext.FindAsync<TEntity>(id);
            return entity;
        }

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public async Task<TEntity> AddEntity(TEntity entity)
        {
            try
            {
                var result = await DbContext.AddAsync(entity);
                await DbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">The entity</param>
        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            DbContext.Update<TEntity>(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id">The id</param>
        public async Task<bool> DeleteEntity(object id)
        {
            var entity = await DbContext.FindAsync<TEntity>(id);
            if (entity != null)
            {
                DbContext.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}