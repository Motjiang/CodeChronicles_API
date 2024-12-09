using CodeChronicles_API.Models.Domain;

namespace CodeChronicles_API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        //5 basic CRUD operations
        Task<bool> CreateAsync(Category category);      
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        
    }
}
