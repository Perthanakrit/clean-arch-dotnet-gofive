using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories {
    public class CategoryRepository : ICategoryRepository {
        protected readonly ApplicationDbContext dbContext;

        public async Task<List<Category>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(Guid id) {
            return await dbContext.Categories.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
