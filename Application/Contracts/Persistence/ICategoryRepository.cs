using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ICategoryRepository {
        Task<Category> GetByIdAsync(Guid id);
        Task<List<Category>> GetAllCategories();
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<Category> DeleteAsync(Guid id);
    }
}
