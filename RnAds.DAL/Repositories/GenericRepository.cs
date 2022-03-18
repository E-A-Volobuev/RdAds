using Microsoft.EntityFrameworkCore;
using RnAds.InterFaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RnAdsContext _context;


        public GenericRepository(RnAdsContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var dbSet = _context.Set<T>();
            var item = await dbSet.FindAsync(id);

            if (item == null)
                return false;

            _context.Entry(item).State = EntityState.Detached;
            return true;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(List<T> item)
        {
            _context.RemoveRange(item);
            await _context.SaveChangesAsync();
        }

    }
}
