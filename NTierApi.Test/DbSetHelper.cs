using System.Collections.Generic;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace BenefitsEstimator.Test
{
    // Static class to initialize DbSets
    public static class DbSetHelper
    {
        public static Mock<DbSet<T>> GetDbSetMock<T>(List<T> data, Func<object[], T> findPredicate = null) where T : class
        {
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());
            dbSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
            dbSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(x => { data.Remove(x); });
            if (findPredicate != null)
            {
                dbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(findPredicate);
            }
            return dbSet;
        }
    }
}