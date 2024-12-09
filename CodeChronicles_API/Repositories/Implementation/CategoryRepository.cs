using CodeChronicles_API.Data;
using CodeChronicles_API.Models.Domain;
using CodeChronicles_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeChronicles_API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        //get the db context
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();


        public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);


        public async Task<bool> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Name = category.Name;
            existingCategory.UrlHandle = category.UrlHandle;

            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }

}
