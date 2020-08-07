using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public class UnitRepository : IUnitRepository
    {
        public readonly DataContext _context;
        public UnitRepository(DataContext context)
        {
            _context = context;

        }
        public void Add(Unit entity)
        {
            _context.Units.Add(entity);
        }

        public void Delete(Unit entity)
        {
            _context.Units.Remove(entity);
        }

        public async Task<IEnumerable<Unit>> GetAll()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit> GetByCode(string code)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.Code == code);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}