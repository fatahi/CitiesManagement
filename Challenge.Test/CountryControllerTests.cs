using System;
using Xunit;
using FluentAssertions;
using Challenge.Domain.CountryAgg;
using Challenge.Presentation.Api.Controllers;
using Challenge.Application.Contracts.Country;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Test
{
    public  class CountryControllerTests
    {
        private readonly CountryController _controller;
        private readonly ICountryApplication _service;

        public CountryControllerTests()
        {
            _service = new CountryServiceFake();
            _controller = new CountryController(_service);
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult =await _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<CountryViewModel>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(10);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new CreateCountry()
            {
                countryName = "Iran"
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse =await _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            CreateCountry testItem = new CreateCountry()
            {
                countryName="Iran"
            };

            // Act
            var createdResponse =await _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new CreateCountry()
            {
                countryName = "Iran"
            };

            // Act
            var createdResponse = await _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Country;

            // Assert
            Assert.IsType<Country>(item);
            Assert.Equal("Guinness Original 6 Pack", item.CountryName);
        }

        [Fact]
        public async void Remove_NotExisting_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 100;

            // Act
            var badResponse =await _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async void Remove_Existing_ReturnsNoContentResult()
        {
            // Arrange
            var existingId = 100;

            // Act
            var noContentResponse =await _controller.Delete(existingId);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public async void Remove_Existing_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;

            // Act
            var okResponse =await _controller.Delete(existingId);

            // Assert
            Assert.Equal(2, _service.GetAllAsync().Result.Count());
        }
    }
}
