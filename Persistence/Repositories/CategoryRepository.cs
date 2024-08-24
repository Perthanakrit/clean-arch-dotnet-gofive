using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;


namespace Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository {
        protected readonly ApplicationDbContext dbContext;
        
        public CategoryRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            Category categoryDb = dbContext.Categories.Where(w => w.Id == id).FirstOrDefault();

            if (categoryDb == null) {
                return null;
            }

            dbContext.Categories.Remove(categoryDb);
            await dbContext.SaveChangesAsync();

            return categoryDb;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await dbContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id) {
            return await dbContext.Categories.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            Category categoryDb = await dbContext.Categories.Where(w => w.Id == category.Id).FirstOrDefaultAsync();

            if (categoryDb == null) {
                return null;
            }

            categoryDb.Name = category.Name;
            categoryDb.UrlHandle = category.UrlHandle;

            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
