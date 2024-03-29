﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Data
{

    public interface ISqlRepository<T> where T : class
    {
        Task<T> GetEntity(object id);
        Task<T> AddEntity(T entity);
        Task<T> UpdateEntity(T entity);
        Task<bool> DeleteEntity(object id);
    }
}
