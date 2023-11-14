
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
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Tests

{
    public class ProductServiceTests
    {
        
        private static P3Referential _context;
        static ProductRepository productRepositoryInstance = new ProductRepository(_context);
        static OrderRepository orderRepositoryInstance = new OrderRepository(_context);        

        static List<string> validityResult(ProductViewModel product)
        {
            var mockCart = Mock.Of<ICart>();
            var mockProductRepository = Mock.Of<IProductRepository>();
            var mockOrderRepository = Mock.Of<IOrderRepository>();
            var mockLocalizer = Mock.Of<IStringLocalizer<ProductService>>();

            var productService = new ProductService(
                mockCart,
                mockProductRepository,
                mockOrderRepository,
                mockLocalizer
            );
          return  productService.CheckProductModelErrors(product);
        }


        //  CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            

        [Fact]
        public void CheckProductModelErrors_ValidProduct()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Empty(validityResult(product));
        }
        [Fact]
        public void CheckProductModelErrors_ValidProduct_BlankSpaces()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "  Name  ",
                Price = "  100  ",
                Stock = "  5  ",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Empty(validityResult(product));
        }
        [Fact]
        public void CheckProductModelErrors_ValidProduct_PriceWithDotSeparator()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100.5",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Empty(validityResult(product));
        }
        [Fact]
        public void CheckProductModelErrors_ValidProduct_PriceWithCommaSeparator()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100,5",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Empty(validityResult(product));
        }
        [Fact]
        public void CheckProductModelErrors_AllErrorMessagesInFrench()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            var product = new ProductViewModel
            {
                Name = "",
                Price = "-100.5",
                Stock = "-5",
                Description = "",
                Details = ""
            };
            //Assert
            Assert.Equal(3, validityResult(product).Count);
            Assert.Contains("Veuillez saisir un nom", validityResult(product));
            Assert.Contains("Le stock doit être un entier positif", validityResult(product));
            Assert.Contains("Le prix doit être un nombre positif", validityResult(product));
        }

        [Fact]
        public void CheckProductModelErrors_MissingStock()
        {
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100.5",
                Stock = "",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Contains("Please enter a stock value", validityResult(product));          
           
        }
        [Fact]
        public void CheckProductModelErrors_MissingName()
        {
            var product = new ProductViewModel
            {
                Name = "",
                Price = "100.5",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Contains("Please enter a name", validityResult(product));

        }


        [Fact]
        public void CheckProductModelErrors_PriceNotGreaterThanZero()
        {
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "-100.5",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Contains("The price must be a positive number", validityResult(product));
        }
        [Fact]
        public void CheckProductModelErrors_QuantityNotGreaterThanZero()
        {  // Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100.5",
                Stock = "-1",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Single(validityResult(product));
            Assert.Contains("The stock must be a positive integer", validityResult(product));

        }
        [Fact]
        public void CheckProductModelErrors_MissingName_NegativePrice_InvalidStock()
        {  // Arrange
            var product = new ProductViewModel
            {
                Name = "",
                Price = "-100.5",
                Stock = "-kjh",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Equal(3,validityResult(product).Count);
            Assert.Contains("Please enter a name", validityResult(product));
            Assert.Contains("The price must be a positive number", validityResult(product));
            Assert.Contains("The stock must be a positive integer", validityResult(product));

        }
       
        [Fact]
        public void CheckProductModelErrors_PriceNotGreaterThanZero_DotSeparator()
        {
            var product = new ProductViewModel
            {            
                Name = "Name",
                Price = "100.5",
                Stock ="5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Assert
            Assert.Empty(validityResult(product));
         ///   Assert.Single(validityResult(product));
          //  Assert.Contains("The price must be a positive number", validityResult(product));
        }    
    }
}
        
 
