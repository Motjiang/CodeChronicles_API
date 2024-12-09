using CodeChronicles_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeChronicles_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
    }
}
