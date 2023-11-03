
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Data;
using Moq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P3AddNewFunctionalityDotNetCore.Tests

{
    public class ProductServiceTests
    {
        private static P3Referential _context;
        static ProductRepository productRepositoryInstance = new ProductRepository(_context);
        static OrderRepository orderRepositoryInstance = new OrderRepository(_context);

        [Fact]
        public void CheckProductModelErrors_ValidProduct_ReturnsEmptyList()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<ProductService>(factory);
            Cart cartInstance = new Cart();


            // Arrange
            var productService = new ProductService(
                    cartInstance,  // Mock or real ICart instance
                    productRepositoryInstance,  // Mock or real IProductRepository instance
                    orderRepositoryInstance,  // Mock or real IOrderRepository instance
                    localizer  // Mock or real IStringLocalizer<ProductService> instance
                );

            var product = new ProductViewModel
            {
                // Set the properties of a valid ProductViewModel
                // For example:
                Name = "i",
                Price = "10.00",
                Stock = "100",
                Description = "Valid description",
                Details = "Valid details"
            };
            var testResult=productService.CheckProductModelErrors( product );

            

           
            

         //   var validationResults = new List<ValidationResult>();
       //     var validationContext = new ValidationContext(productViewModel);
            // Mock the behavior of Validator.TryValidateObject
       //     var validatorMock = new Mock<IValidator>();
        //    validatorMock.Setup(v => v.TryValidateObject(
         //       It.IsAny<object>(),
          //      It.IsAny<ValidationContext>(),
           //     It.IsAny<ICollection<ValidationResult>>(),
            //    It.IsAny<bool>()))
           //     .Callback((object obj, ValidationContext ctx, ICollection<ValidationResult> results, bool validateAllProperties) =>
            //    {
                    // Manually add a ValidationResult for a valid object
             //       results.Add(new ValidationResult("Valid", new string[] { }));
          //      });

            // Inject the mocked IValidator
        //    productService.SetValidator(validatorMock.Object);

            // Act
          //  var errors = productService.CheckProductModelErrors(productViewModel);

            // Assert
            Assert.Empty(testResult) ;

        }








    }
}
        
 
