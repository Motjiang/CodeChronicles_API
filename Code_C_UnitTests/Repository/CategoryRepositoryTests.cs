using CodeChronicles_API.Data;
using CodeChronicles_API.Models.Domain;
using CodeChronicles_API.Repositories.Implementation;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_C_UnitTests.Repository
{
    public class CategoryRepositoryTests
    {
        // get the repository
        private readonly CategoryRepository _categoryRepository;

        // Constructor
        public CategoryRepositoryTests()
        {   var applicationDbContext = GetApplicationContext();
            _categoryRepository = new CategoryRepository(applicationDbContext);
        }

        // Create a private method to extract the in-memory database context
        private ApplicationDbContext GetApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CodeChroniclesDb").Options;

            var dbContext = new ApplicationDbContext(options);

            // Seed data if required
            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.Add(new Category
                {
                    Name = "Test Category 1",
                    UrlHandle = "test-category-link",
                });
                dbContext.SaveChanges();
            }

            return dbContext;
        }

        // test the create category method
        [Fact]
        public async void CategoryRepository_CreateAsync_ReturnTrue()
        {
            // Arrange
            var category = A.Fake<Category>();
            category.Name = "Test Category 2";
            category.UrlHandle = "test-category-link-2";

            // Act
            var result = await _categoryRepository.CreateAsync(category);

            // Assert
            result.Should().BeTrue();
        }


        // test the get all categories method
        [Fact]
        public async void CategoryRepository_GetAllAsync_ReturnsCategories()
        {
            // Act
            var result = await _categoryRepository.GetAllAsync();

            // Assert
            result.Should().AllBeOfType<Category>();
        }


        // test the get category by id method
        [Theory]
        [InlineData(2)]
        public async void CategoryRepository_GetByIdAsync_ReturnsCategory(int id)
        {
            // Act
            var result = await _categoryRepository.GetByIdAsync(id);

            // Assert
            result.Should().BeOfType<Category>();
        }


        // test the update category method
        [Theory]
        [InlineData(2)]
        public async void CategoryRepository_UpdateAsync_ReturnsTrue(int id)
        {
            // Arrange
            var category = await _categoryRepository.GetByIdAsync(id);
            category.Name = "Test Category updated";
            category.UrlHandle = "test-category-link-3";

            // Act
            var result = await _categoryRepository.UpdateAsync(category);

            // Assert
            result.Should().BeTrue();
        }


        // test the delete category method
        [Fact]
        public async void CategoryRepository_DeleteAsync_ReturnsTrue()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _categoryRepository.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
        }
    }
}
