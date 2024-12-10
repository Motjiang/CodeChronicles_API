using CodeChronicles_API.Controllers;
using CodeChronicles_API.Models.Domain;
using CodeChronicles_API.Repositories.Interface;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_C_UnitTests.Controller
{
    public class CategoryControllerTests
    {
        //get repository
        private readonly ICategoryRepository CategoryRepository;
        //get controller
        private readonly CategoryController CategoryController;

        //constructor
        public CategoryControllerTests()
        {
            this.CategoryRepository = A.Fake<ICategoryRepository>();
            this.CategoryController = new CategoryController(CategoryRepository);
        }

        private static Category CreateFakeCategory() => A.Fake<Category>();

        //test create category
        // this method is testing the create category method in the category controller
        //It wil test if the action returns a 201 status code
        [Fact]
        public async void CategoryController_Create_ReturnCreated()
        {
            // Arrange
            var category = CreateFakeCategory();

            // Act
            A.CallTo(() => CategoryRepository.CreateAsync(category)).Returns(true);
            var result = (CreatedAtActionResult)await CategoryController.Create(category);

            // Assert
            result.StatusCode.Should().Be(201);
            result.Should().NotBeNull();
        }

        //test get all categories
        // this method is testing the get all categories method in the category controller
        //It wil test if the action returns a 200 status code
        [Fact]
        public async void CategoryController_GetAll_ReturnOk()
        {
            // Arrange
            var categories = A.Fake<List<Category>>();
            categories.Add(new Category() { Name = "Category 1", UrlHandle = "Category 1- link"});

            // Act
            A.CallTo(() => CategoryRepository.GetAllAsync()).Returns(categories);
            var result = (OkObjectResult)await CategoryController.GetAll();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        //test get category by id
        // this method is testing the get category by id method in the category controller
        //It wil test if the action returns a 200 status code
        [Theory]
        [InlineData(1)]
        public async void CategoryController_GetById_ReturnOk(int id)
        {
            // Arrange
            var category = CreateFakeCategory();
            category.Id = id;
            category.Name = "Category 1";
            category.UrlHandle = "Category 1- link";

            // Act
            A.CallTo(() => CategoryRepository.GetByIdAsync(id)).Returns(category);
            var result = (OkObjectResult)await CategoryController.GetById(id);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }


        //test update category
        // this method is testing the update category method in the category controller
        //It wil test if the action returns a 200 status code
        [Fact]
        public async void CategoryController_Update_ReturnOk()
        {
            // Arrange
            var category = CreateFakeCategory();

            // Act
            A.CallTo(() => CategoryRepository.UpdateAsync(category)).Returns(true);
            var result = (OkResult)await CategoryController.Update(category);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        //test delete category
        // this method is testing the delete category method in the category controller
        //It wil test if the action returns a 204 status code
        [Fact]
        public async void CategoryController_Delete_ReturnNoContent()
        {
            // Arrange
            var id = 1;

            // Act
            A.CallTo(() => CategoryRepository.DeleteAsync(id)).Returns(true);
            var result = (NoContentResult)await CategoryController.Delete(id);

            // Assert
            result.StatusCode.Should().Be(204);
            result.Should().NotBeNull();
        }
    }
}
